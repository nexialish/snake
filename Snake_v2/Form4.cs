using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Snake.Properties;

namespace Snake
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.Name1;
            textBox3.Text = Properties.Settings.Default.Name2;
            textBox5.Text = Properties.Settings.Default.Name3;
            textBox2.Text = Properties.Settings.Default.Score1.ToString();
            textBox4.Text = Properties.Settings.Default.Score2.ToString();
            textBox6.Text = Properties.Settings.Default.Score3.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Name1 = textBox1.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
