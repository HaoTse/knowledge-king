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

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2 PlayForm = new Form2();
            Hide();
            PlayForm.ShowDialog();
            Close();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form3 SelfPlayForm = new Form3();
            Hide();
            SelfPlayForm.ShowDialog();
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form4 rankingForm = new Form4();
            rankingForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form5 logForm = new Form5();
            logForm.Show();
        }
    }
}
