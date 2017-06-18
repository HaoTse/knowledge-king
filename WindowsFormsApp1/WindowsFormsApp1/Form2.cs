using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        GameSocket client;
        StrHandler msgHandler;
        int gameID;

        string dbHost = DBConfig.dbHost;
        string dbUser = DBConfig.dbUser;
        string dbPass = DBConfig.dbPass;
        string dbName = DBConfig.dbName;
        MySqlConnection conn;

        int[] ValueString = new int[5];
        Button[] btnArr;

        int cnt = 8;

        string ans;
        int opt;
        int num = 0;

        int score = 0;

        public Form2()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer2.Interval = 300;

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
            conn = new MySqlConnection(connStr);

            msgHandler = this.getMsg;

            try
            {
                client = GameSocket.connect(GameSetting.serverIp);
                client.newListener(processMsgComeIn);
                client.send(CurrentPlayer.getPlayer() + ":-2:join");
            }
            catch
            {
                gameID = -2;
                timer1.Enabled = timer2.Enabled = false;
                MessageBox.Show("遊戲終止", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btnArr = new Button[4] { button1, button2, button3, button4 };
        }

        public string processMsgComeIn(String msg)
        {
            this.Invoke(msgHandler, new Object[] { msg });
            return "OK";
        }

        public String getMsg(String msg)
        {
            if (!msg.Split(':')[0].Contains(CurrentPlayer.getPlayer()))
                return "nothing";

            Console.WriteLine(msg);
            string playerList = msg.Split(':')[0];
            gameID = int.Parse(msg.Split(':')[1]);
            string content = msg.Split(':')[2];

            if (content.Contains("wait"))
            {
                /*
                 * implement form
                 */
            }
            else if (content.Contains("start"))
            {
                label3.Text = CurrentPlayer.getPlayer();
                if (playerList.Split(',')[0] != CurrentPlayer.getPlayer())
                    label6.Text = playerList.Split(',')[0];
                else
                    label6.Text = playerList.Split(',')[1];
            }
            else if (content.Contains("ShutDown"))
            {
                /*
                 * implement form
                 */
                timer1.Enabled = timer2.Enabled = false;
                MessageBox.Show("遊戲終止", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else if (content.Contains("quiz"))
            {
                string[] quiz = content.Split(' ')[1].Split(',');

                for(int i = 0; i < 5; i++)
                {
                    ValueString[i] = int.Parse(quiz[i]);
                }
                cnt = 8;
                getProblem(num++);
                timer1.Enabled = true;
            }
            else if (content.Contains("score"))
            {
                if(CurrentPlayer.getPlayer() != content.Split(' ')[1].Split(',')[0])
                    label5.Text = content.Split(' ')[1].Split(',')[1];
                else
                    label5.Text = content.Split(' ')[1].Split(',')[3];
            }
            else if (content.Contains("next"))
            {
                timer2.Enabled = true;
            }
            else if (content.Contains("gameover"))
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                gameID = -2;
                string result = content.Split(' ')[1];

                if(result == "draw")
                    MessageBox.Show("平手", "遊戲結束", MessageBoxButtons.OK);
                else if(result == CurrentPlayer.getPlayer())
                    MessageBox.Show("You win!", "遊戲結束", MessageBoxButtons.OK);
                else
                    MessageBox.Show("You lose!", "遊戲結束", MessageBoxButtons.OK);

                Form1 mainForm = new Form1();
                Hide();
                mainForm.ShowDialog();
                Close();
            }
            return "OK";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            if (btn.Text == ans)
            {
                score = score + cnt;
                label4.Text = score.ToString();
                btn.BackColor = Color.FromArgb(159, 223, 201);
            }
            else
            {
                btn.BackColor = Color.LightCoral;
                btnArr[opt - 1].BackColor = Color.FromArgb(159, 223, 201);
            }
            client.send(CurrentPlayer.getPlayer() + ":" + gameID.ToString() + ":score:" + score.ToString());
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(gameID > -2)
                client.send(CurrentPlayer.getPlayer() + ":" + gameID.ToString() + ":leave");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cnt <= 0)
            {
                Console.WriteLine("timeout!!!!!!!");
                timer1.Enabled = false;
                btnArr[opt-1].BackColor = Color.FromArgb(159, 223, 201);
                client.send(CurrentPlayer.getPlayer() + ":" + gameID.ToString() + ":timeout");
            }
            else
            {
                cnt = cnt - 1;
                label1.Text = cnt.ToString();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            cnt = 8;
            getProblem(num++);
            timer1.Enabled = true;
            timer2.Enabled = false;
        }

        private void getProblem(int index)
        {
            button1.BackColor = SystemColors.InactiveCaption;
            button2.BackColor = SystemColors.InactiveCaption;
            button3.BackColor = SystemColors.InactiveCaption;
            button4.BackColor = SystemColors.InactiveCaption;

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            if (index > 4)
            {
                timer1.Enabled = false;
                client.send(CurrentPlayer.getPlayer() + ":" + gameID + ":gameover");
            }
            else
            {
                conn.Open();

                string sql = "SELECT * FROM quiz WHERE id=" + ValueString[index].ToString();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    label2.Text = data["problem"].ToString();
                    button1.Text = data["A"].ToString();
                    button2.Text = data["B"].ToString();
                    button3.Text = data["C"].ToString();
                    button4.Text = data["D"].ToString();
                    ans = data["ans"].ToString();
                    opt = int.Parse(data["opt"].ToString());
                }

                conn.Close();
            }
        }


    }
}
