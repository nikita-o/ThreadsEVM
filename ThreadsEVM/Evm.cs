using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{

    class Evm
    {
        Queue<int> mem;

        Top[] tops;
        int[,] matrix;

        int[] input;
        int[] output;

        public Evm(int a)
        {

        }

        public void start()
        {
            while (mem.Count > 0)
            {
                List<int> callback = new List<int>();

                // выбор из очереди некст команды
                int id = mem.Dequeue();
                Top top = tops[id];

                // исполнение команды
                List<(int i, int j, int data)> output = top.work();

                // обновление данных (вносим данные, помечаем флаги)
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
