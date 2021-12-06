using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{
    class Top
    {
        //public delegate List<int> Func(List<int> opr);

        //// Comand
        //public Func opc;
        public int[] data; // (opr) - входные данные
        public (Top t, int id)[] output; // (des) - дуги по которым идет результат

        // Control
        public bool[] checkData; // (pres) - флаги получения входных данных
        //public Top[] parents;

        virtual public bool isReady()
        {
            foreach (var flag in checkData)
            {
                if (!flag)
                {
                    return false;
                }
            }
            return true;
        }

        virtual public List<Top> work()
        {
            //return new Top[]{ };
            // Исполнить функцию
            // заполнить выходные данные для следующих вершин (+ установки их флагов)
            // обнулить свои флаги данных
            // вызвать следующие вершины + родительские (вроде не важен порядок) (return кого вызвать)
        }
    }

    class OperTop: Top // 1-2 (n) - входа, 1 (n) выход
    {
        delegate int[] Func(int[] data);

        Func func;

        override public List<Top> work()
        {
            List<Top> res = new List<Top>();

            int[] outData = func(data);
            for (int i = 0; i < output.Length; i++)
            {
                (Top t, int id) = output[i];
                t.data[id] = outData[i];
                t.checkData[id] = true;
                res.Add(t);
            }

            for (int i = 0; i < checkData.Length; i++)
            {
                checkData[i] = false;
            }
            return res;
        }
    }

    class BranchTop: Top // 1 - вход, 2 (n) выхода
    {
        override public List<Top> work()
        {
            List<Top> res = new List<Top>();
            foreach (var item in output)
            {
                (Top t, int id) = item;
                t.data[id] = data[0];
                t.checkData[id] = true;
                res.Add(t);
            }

            for (int i = 0; i < checkData.Length; i++)
            {
                checkData[i] = false;
            }
            
            return res;
        }
    }

    class MergeTop: Top // 2 (n) - входа, 1 выход
    {
        override public bool isReady()
        {
            foreach (bool flag in checkData)
            {
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }

        override public List<Top> work()
        {
            List<Top> res = new List<Top>();

            for (int i = 0; i < checkData.Length; i++)
            {
                if (checkData[i])
                {
                    (Top t, int id) = output[0];
                    t.data[id] = data[i];
                    t.checkData[id] = true;
                    res.Add(t);
                }
            }
            

            for (int i = 0; i < checkData.Length; i++)
            {
                checkData[i] = false;
            }

            return res;
        }
    }

    class TFTop: Top // 1 - вход, 2 выхода, 1 управляющий (data[0])
    {
        override public List<Top> work()
        {
            List<Top> res = new List<Top>();

            if (data[0] != 0)
            {
                (Top t, int id) = output[0];
                t.data[id] = data[1];
                t.checkData[id] = true;
                res.Add(t);
            }
            else
            {
                (Top t, int id) = output[1];
                t.data[id] = data[1];
                t.checkData[id] = true;
                res.Add(t);
            }

            for (int i = 0; i < checkData.Length; i++)
            {
                checkData[i] = false;
            }
            return res;
        }
    }
    class ValveTop: Top // 1 - вход, 1 выход, 1 управляющий (data[0])
    {
        override public List<Top> work()
        {
            List<Top> res = new List<Top>();

            if (data[0] != 0)
            {
                (Top t, int id) = output[0];
                t.data[id] = data[1];
                t.checkData[id] = true;
                res.Add(t);
            }

            for (int i = 0; i < checkData.Length; i++)
            {
                checkData[i] = false;
            }
            return res;
        }
    }

    class ThreadTop: Top // 1 выход
    {

        override public List<Top> work()
        {
            List<Top> res = new List<Top>();



            
            return res;
        }
    }

    class OutTop: Top // 1 вход
    {
        override public List<Top> work()
        {
            List<Top> res = new List<Top>();




            return res;
        }
    }
}
