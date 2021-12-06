using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ThreadsEVM
{

    class Evm
    {
        Queue<int> mem;

        Top[] tops;
        int[,] matrix;

        int[] input;
        int[] output;

        TextBox textBox;

        public Evm(Top[] tops, int[,] matrix, int[] input, int[] output, TextBox textBox)
        {
            this.tops = tops;
            this.matrix = matrix;
            this.input = input;
            this.output = output;
            this.textBox = textBox;

            mem = new Queue<int>();
            foreach (var item in input)
                mem.Enqueue(item);
        }

        public void start(Queue<int>[] data)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int id = input[i];
                InputTop top = (InputTop)tops[id];
                top.input = data[i];
            }

            while (mem.Count > 0)
            {
                List<int> callback = new List<int>();

                // выбор из очереди некст команды
                int id = mem.Dequeue();
                Top top = tops[id];

                // исполнение команды
                List<(int i, int j, int data)> output = top.work();

                // обновление данных (вносим данные, помечаем флаги)
                if (output.Count == 0)
                {
                    // вывод data[0]
                    textBox.AppendText(top.data[0].ToString() + '\n');
                }

                foreach (var item in output)
                {
                    (int i, int j, int data) = item;
                    tops[i].data[j] = data;
                    tops[i].checkData[j] = true;

                    if (tops[i].isReady())
                    {
                        mem.Enqueue(i);
                    }
                }

                // формируем некст вызов в очередь (по матрице)
                for (int i = 0; i < tops.Length; i++)
                {
                    if (matrix[i, id] != 0)
                    {
                        mem.Enqueue(i);
                    }
                }

            }
        }
    }
}
