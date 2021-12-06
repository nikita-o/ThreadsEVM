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
        Evm evm;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // компиляция
        private void button1_Click(object sender, EventArgs e)
        {
            Parsing parsing = new Parsing();

            Evm evm = new Evm();
        }

        // запуск
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
