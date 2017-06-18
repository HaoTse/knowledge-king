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
    public partial class Form3 : Form
    {
        string dbHost = DBConfig.dbHost;
        string dbUser = DBConfig.dbUser;
        string dbPass = DBConfig.dbPass;
        string dbName = DBConfig.dbName;
        MySqlConnection conn;

        private int cnt;
        int[] index = new int[5];
        int[] ValueString = new int[5];

        Button[] btnArr;

        string ans;
        int opt;
        int num = 0;

        int score = 0;

        public Form3()
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
            conn = new MySqlConnection(connStr);

            InitializeComponent();
            this.Height = 510;
            cnt = 8;
            timer1.Interval = 1000;
            timer2.Interval = 300;

            getRand();
            timer1.Enabled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            btnArr = new Button[4] { button1, button2, button3, button4 };

        }

        private void getRand()
        {
            Random rnd = new Random();
            // get max
            conn.Open();
            string sql = "SELECT COUNT(*) FROM quiz";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int max = (int)(long)cmd.ExecuteScalar();
            conn.Close();

            //  亂數產生
            for (int i = 0; i < 5; i++)
            {
                ValueString[i] = rnd.Next(1, max + 1);

                //  檢查是否存在重複
                while (Array.IndexOf(ValueString, ValueString[i], 0, i) > -1)
                {
                    ValueString[i] = rnd.Next(1, max + 1);
                }
            }

            label5.Text = ValueString[0].ToString() + ValueString[1].ToString() + ValueString[2].ToString() + ValueString[3].ToString() + ValueString[4].ToString();
            getProblem(0);
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
                timer2.Enabled = false;

                MySqlCommand command = conn.CreateCommand();
                conn.Open();

                command.CommandText = "INSERT INTO ranking(name, score) VALUES('" + CurrentPlayer.getPlayer() + "', " + score + ")";
                command.ExecuteNonQuery();

                conn.Close();


                MessageBox.Show("您的成績是" + score, "遊戲結束", MessageBoxButtons.OK);
                Form1 mainForm = new Form1();
                Hide();
                mainForm.ShowDialog();
                Close();
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
                    label5.Text = ans = data["ans"].ToString();
                    opt = int.Parse(data["opt"].ToString());


                }

                conn.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cnt <= 0)
            {
                btnArr[opt-1].BackColor = Color.FromArgb(159, 223, 201);
                timer1.Enabled = false;

                timer2.Enabled = true;

            }
            else
            {
                cnt = cnt - 1;
                label1.Text = cnt.ToString();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            getProblem(++num);
            cnt = 8;
            label1.Text = cnt.ToString();
            timer1.Enabled = true;
            timer2.Enabled = false;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            timer1.Enabled = false;

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
            
            timer2.Enabled = true;
        }
    }
}
