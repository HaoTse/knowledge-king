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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private int cnt;
        int[] index = new int[5];
        string dbHost = "127.0.0.1";//資料庫位址
        string dbUser = "root";//資料庫使用者帳號
        string dbPass = "";//資料庫使用者密碼
        string dbName = "project";//資料庫名稱

        int[] ValueString = new int[5];

        string ans;
        int num = 0;

        int score = 0;

        public Form3()
        {
            InitializeComponent();
            this.Height = 510;
            cnt = 8;
            timer1.Interval = 1000;

            getRand();
            timer1.Enabled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void getRand()
        {
            Random rnd = new Random();
            int max = 20;

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
            if (index > 4)
            {
                timer1.Enabled = false;

                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();

                command.CommandText = "INSERT INTO ranking(score) VALUES(" + score + ")";
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
                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                string sql = "SELECT problem FROM quiz WHERE id=" + ValueString[index].ToString();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string prob = (string)cmd.ExecuteScalar();
                label2.Text = prob;

                sql = "SELECT A FROM quiz WHERE id=" + ValueString[index].ToString();
                cmd = new MySqlCommand(sql, conn);
                string opt1 = (string)cmd.ExecuteScalar();
                button1.Text = opt1;

                sql = "SELECT B FROM quiz WHERE id=" + ValueString[index].ToString();
                cmd = new MySqlCommand(sql, conn);
                string opt2 = (string)cmd.ExecuteScalar();
                button2.Text = opt2;

                sql = "SELECT C FROM quiz WHERE id=" + ValueString[index].ToString();
                cmd = new MySqlCommand(sql, conn);
                string opt3 = (string)cmd.ExecuteScalar();
                button3.Text = opt3;

                sql = "SELECT D FROM quiz WHERE id=" + ValueString[index].ToString();
                cmd = new MySqlCommand(sql, conn);
                string opt4 = (string)cmd.ExecuteScalar();
                button4.Text = opt4;

                sql = "SELECT ans FROM quiz WHERE id=" + ValueString[index].ToString();
                cmd = new MySqlCommand(sql, conn);
                ans = (string)cmd.ExecuteScalar();

                label5.Text = ans;

                conn.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cnt <= 0)
            {
                timer1.Enabled = false;
                cnt = 8;
                label1.Text = cnt.ToString();

                getProblem(++num);
                timer1.Enabled = true;

            }
            else
            {
                cnt = cnt - 1;
                label1.Text = cnt.ToString();
            }
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
            timer1.Enabled = false;
            
            if (button1.Text == ans)
            {
                score = score + cnt;
                label4.Text = score.ToString();
            }
            cnt = 8;
            label1.Text = cnt.ToString();

            getProblem(++num);
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            if (button2.Text == ans)
            {
                score = score + cnt;
                label4.Text = score.ToString();
            }
            cnt = 8;
            label1.Text = cnt.ToString();
            getProblem(++num);
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (button3.Text == ans)
            {
                score = score + cnt;
                label4.Text = score.ToString();
            }
            cnt = 8;
            label1.Text = cnt.ToString();
            getProblem(++num);
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            if (button4.Text == ans)
            {
                score = score + cnt;
                label4.Text = score.ToString();
            }
            cnt = 8;
            label1.Text = cnt.ToString();
            getProblem(++num);
            timer1.Enabled = true;
        }
    }
}
