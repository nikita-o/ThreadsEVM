using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ThreadsEVM
{

    class Evm
    {
        Queue<int> mem;

        List<Top> tops;
        int[,] matrix;

        List<int> input;
        List<int> output; // not used

        TextBox textBox;

        public Evm(ref List<Top> tops, ref int[,] matrix, ref List<int> input, ref List<int> output, ref TextBox textBox)
        {
            this.tops = tops;
            this.matrix = matrix;
            this.input = input;
            this.output = output;
            this.textBox = textBox;
        }

        public void start(Queue<int>[] data)
        {
            // обновление состояния вершин
            foreach (var top in tops)
                top.reload();

            mem = new Queue<int>();

            // заполняем очереди "входных" вершин
            for (int i = 0; i < input.Count; i++)
            {
                int id = input[i];
                InputTop top = (InputTop)tops[id];
                top.input = data[i];

                // в "очередь выполнения" отправляем "входные"
                mem.Enqueue(id);
            }

            while (mem.Count > 0)
            {
                List<int> callback = new List<int>();

                // выбор из очереди некст команды
                int id = mem.Dequeue();
                Top top = tops[id];

                // исполнение команды
                Top.Output[] outputs = top.work();

                // обновление данных (вносим данные, помечаем флаги)
                if (outputs.Length == 0) // только вершины вывода, могут быть без вывода... 
                    textBox.AppendText(top.data[0].ToString() + "\r\n");

                foreach (var item in outputs)
                {
                    int idTop = item.idTop;
                    int idIn = item.idIn;
                    int outData = item.data;

                    tops[idTop].data[idIn] = outData;
                    tops[idTop].checkData[idIn] = true;

                    // вызов следующих вершин
                    if (tops[idTop].isReady())
                        mem.Enqueue(idTop);
                }

                // формируем некст вызов в очередь (по матрице ищем родителя, сообщаем ему, что мы готовы принять его)
                for (int i = 0; i < tops.Count; i++)
                    if (matrix[i, id] != 0 && tops[i].isReady())
                            mem.Enqueue(i);
            }
        }
    }
}
