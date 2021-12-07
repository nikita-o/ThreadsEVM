﻿using System;
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

            parsing.parsing(textBox1.Lines);

            evm = new Evm(ref parsing.tops, ref parsing.matrix_out, ref parsing.inputs, ref parsing.outputs, ref textBox3);


        }

        // запуск
        private void button2_Click(object sender, EventArgs e)
        {
            String[] text = textBox2.Lines;
            Queue<int>[] data = new Queue<int>[text.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Queue<int>();
                String[] s = text[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var ps in s)
                {
                    data[i].Enqueue(int.Parse(ps));
                }
            }

            evm.start(data);
        }
    }
}
