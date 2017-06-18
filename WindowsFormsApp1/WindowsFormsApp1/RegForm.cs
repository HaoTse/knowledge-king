using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class RegForm : Form
    {
        string dbHost = DBConfig.dbHost;
        string dbUser = DBConfig.dbUser;
        string dbPass = DBConfig.dbPass;
        string dbName = DBConfig.dbName;
        MySqlConnection conn;

        public RegForm()
        {
            InitializeComponent();

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
            conn = new MySqlConnection(connStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
                MessageBox.Show("帳號為必填", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(textBox2.Text == "")
                MessageBox.Show("密碼為必填", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox2.Text != textBox3.Text)
                MessageBox.Show("確認密碼錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                conn.Open();
                string sql = "INSERT INTO member (name, pwd) VALUES (@name, MD5(@pwd))";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@pwd", textBox2.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                Form5 logForm = new Form5();
                Hide();
                logForm.ShowDialog();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form1 mainForm = new Form1();
            Hide();
            mainForm.ShowDialog();
            Close();
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }
    }
}
