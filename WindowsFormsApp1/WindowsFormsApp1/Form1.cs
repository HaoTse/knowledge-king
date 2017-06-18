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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Height = 590;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (CurrentPlayer.isLogin)
            {
                pictureBox2.MouseEnter -= No_MouseEnter; pictureBox3.MouseEnter -= No_MouseEnter; pictureBox4.MouseEnter -= No_MouseEnter;
                label2.MouseEnter -= No_MouseEnter; label3.MouseEnter -= No_MouseEnter; label1.MouseEnter -= No_MouseEnter;
                pictureBox2.MouseEnter += pictureBox3_MouseEnter; pictureBox3.MouseEnter += pictureBox3_MouseEnter; pictureBox4.MouseEnter += pictureBox3_MouseEnter;
                label2.MouseEnter += pictureBox3_MouseEnter; label3.MouseEnter += pictureBox3_MouseEnter; label1.MouseEnter += pictureBox3_MouseEnter;

                label4.Text = "登出";
                label5.Text = CurrentPlayer.getPlayer();
            }
            else
            {
                pictureBox2.MouseEnter -= pictureBox3_MouseEnter; pictureBox3.MouseEnter -= pictureBox3_MouseEnter; pictureBox4.MouseEnter -= pictureBox3_MouseEnter;
                label2.MouseEnter -= pictureBox3_MouseEnter; label3.MouseEnter -= pictureBox3_MouseEnter; label1.MouseEnter -= pictureBox3_MouseEnter;
                pictureBox2.MouseEnter += No_MouseEnter; pictureBox3.MouseEnter += No_MouseEnter; pictureBox4.MouseEnter += No_MouseEnter;
                label2.MouseEnter += No_MouseEnter;label3.MouseEnter += No_MouseEnter;label1.MouseEnter += No_MouseEnter;

                label4.Text = "登入";
                label5.Text = "";
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer.isLogin)
            {
                Form2 PlayForm = new Form2();
                Hide();
                try
                {
                    PlayForm.ShowDialog();
                }
                catch
                {

                }
                Close();
            }
            else
            {
                MessageBox.Show("請先登入", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void No_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.No;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer.isLogin)
            {
                Form3 SelfPlayForm = new Form3();
                Hide();
                SelfPlayForm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("請先登入", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer.isLogin)
            {
                Form4 rankingForm = new Form4();
                rankingForm.Show();
            }
            else
            {
                MessageBox.Show("請先登入", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer.isLogin)
            {
                CurrentPlayer.Logout();
                Form1_Load(sender, e);
                MessageBox.Show("登出成功", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                Form5 logForm = new Form5();
                Hide();
                logForm.ShowDialog();
                Close();
            }
        }
    }
}
