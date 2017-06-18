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
    public partial class Form4 : Form
    {
        string dbHost = DBConfig.dbHost;
        string dbUser = DBConfig.dbUser;
        string dbPass = DBConfig.dbPass;
        string dbName = DBConfig.dbName;

        public Form4()
        {
            InitializeComponent();
            this.Height = 590;

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "SELECT * FROM ranking ORDER BY score DESC LIMIT 5";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            var dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            var rows = dt.AsEnumerable().ToArray();
            conn.Close();

            if (rows.Length > 0)
            {
                label7.Text = rows[0]["name"].ToString();
                label12.Text = rows[0]["score"].ToString();
            }
            if (rows.Length > 1)
            {
                label8.Text = rows[1]["name"].ToString();
                label13.Text = rows[1]["score"].ToString();
            }
            if (rows.Length > 2)
            {
                label9.Text = rows[2]["name"].ToString();
                label14.Text = rows[2]["score"].ToString();
            }
            if (rows.Length > 3)
            {
                label10.Text = rows[3]["name"].ToString();
                label15.Text = rows[3]["score"].ToString();
            }
            if (rows.Length > 4)
            {
                label1.Text = rows[4]["name"].ToString();
                label16.Text = rows[4]["score"].ToString();
            }

        }
    }
}
