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
    public partial class Form5 : Form
    {
        string dbHost = DBConfig.dbHost;
        string dbUser = DBConfig.dbUser;
        string dbPass = DBConfig.dbPass;
        string dbName = DBConfig.dbName;
        MySqlConnection conn;

        public Form5()
        {
            InitializeComponent();

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
            conn = new MySqlConnection(connStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = @"SELECT * FROM member WHERE name=@name AND pwd=MD5(@pwd)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@pwd", textBox2.Text);
            MySqlDataReader data = cmd.ExecuteReader();

            if (data.HasRows)
            {
                while (data.Read())
                    CurrentPlayer.Login(data["name"].ToString());
                Form1 mainForm = new Form1();
                Hide();
                mainForm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("帳號或密碼錯誤", "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegForm regform = new RegForm();
            Hide();
            regform.ShowDialog();
            Close();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }
    }
}
