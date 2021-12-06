using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadsEVM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            string x,z="";
            string[] y;
            x= textBox1.Text;
            label1.Text = textBox1.Text;
            y = x.Split('\n');
            label1.Text = y.Length.ToString();
            //for (int i = 0; i < 6; i++)
            //{
            //    z += y[i]+"zz";
            //}
            textBox2.Text = z;
        }
    }
}
