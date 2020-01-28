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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            new Settings();
            textBox1.Text = Settings.Width.ToString();
            textBox2.Text = Settings.Height.ToString();
            textBox3.Text = Settings.Speed.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Width"] = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default["Height"] = Convert.ToInt32(textBox2.Text);
            Properties.Settings.Default["SpeedP"] = Convert.ToInt32(textBox3.Text);
            Properties.Settings.Default.Save();
            this.Close();
            Form1 NewGame = new Form1();
            NewGame.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
