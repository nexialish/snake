using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Settings();
            Form1 NewGame = new Form1();
            NewGame.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 settingsButton = new Form2();
            settingsButton.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 ScoreTable = new Form4();
            ScoreTable.Show();
            
        }
    }
}
