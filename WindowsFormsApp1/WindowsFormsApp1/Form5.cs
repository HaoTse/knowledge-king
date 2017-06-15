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
        string dbHost = "127.0.0.1";//資料庫位址
        string dbUser = "root";//資料庫使用者帳號
        string dbPass = "";//資料庫使用者密碼
        string dbName = "project";//資料庫名稱

        DataRow[] rows;

        public Form5()
        {
            InitializeComponent();

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "SELECT * FROM member";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            var dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            rows = dt.AsEnumerable().ToArray();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
