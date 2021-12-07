using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{
    abstract class Top
    {
        // Comand
        public int[] data; // (opr) - входные данные
        public List<(int id, int i, int data)> output; // (des) - дуги по которым идет результат

        // Control
        public bool[] checkData; // (pres) - флаги получения входных данных

        public Top()
        {
        }

        virtual public bool isReady()
        {
            foreach (var flag in checkData)
                if (!flag)
                    return false;
            return true;
        }

        abstract public List<(int id, int i, int data)> work();
    }

    class OperTop1: Top // 1-2 (n) - входа, 1 (n) выход
    {
        public Operations.Func func;

        public OperTop1(): base()
        {
            data = new int[1];
            checkData = new bool[1];
        }

        override public List<(int id, int i, int data)> work()
        {
            int outData = func(data);

            for (int i = 0; i < output.Count; i++)
            {
                (int id, int i, int data) lol = output[i];
                lol.data = outData;
                output[i] = lol;
            }

            for (int i = 0; i < checkData.Length; i++)
                checkData[i] = false;

            return output;
        }
    }

    class OperTop2 : Top // 1-2 (n) - входа, 1 (n) выход
    {

        public Operations.Func func;

        public OperTop2() : base()
        {
            data = new int[2];
            checkData = new bool[2];
        }

        override public List<(int id, int i, int data)> work()
        {
            int outData = func(data);

            for (int i = 0; i < output.Count; i++)
            {
                (int id, int i, int data) lol = output[i];
                lol.data = outData;
                output[i] = lol;
            }

            for (int i = 0; i < checkData.Length; i++)
                checkData[i] = false;

            return output;
        }
    }

    class BranchTop: Top // 1 - вход, 2 (n) выхода
    {
        public BranchTop() : base()
        {
            data = new int[1];
            checkData = new bool[1];
        }

        override public List<(int id, int i, int data)> work()
        {
            for (int i = 0; i < output.Count; i++)
            {
                (int id, int i, int data) lol = output[i];
                lol.data = data[0];
                output[i] = lol;
            }

            for (int i = 0; i < checkData.Length; i++)
                checkData[i] = false;

            return output;
        }
    }

    class MergeTop: Top // 2 (n) - входа, 1 выход
    {
        public MergeTop() : base()
        {
            data = new int[2];
            checkData = new bool[2];
        }
        override public bool isReady()
        {
            foreach (bool flag in checkData)
                if (flag)
                    return true;
            return false;
        }

        override public List<(int id, int i, int data)> work()
        {
            for (int i = 0; i < checkData.Length; i++)
            {
                if (checkData[i])
                {
                    (int id, int i, int data) lol = output[0];
                    lol.data = data[i];
                    output[0] = lol;
                }
            }
            

            for (int i = 0; i < checkData.Length; i++)
                checkData[i] = false;

            return output;
        }
    }

    class TFTop: Top // 1 - вход, 2 выхода, 1 управляющий (data[0])
    {
        public TFTop() : base()
        {
            data = new int[2];
            checkData = new bool[2];
        }

        override public List<(int id, int i, int data)> work()
        {
            List<(int id, int i, int data)> res = new List<(int id, int i, int data)>();

            if (data[0] != 0)
            {
                (int id, int i, int data) lol = output[0];
                lol.data = data[1];
                res.Add(lol);
            }
            else
            {
                (int id, int i, int data) lol = output[1];
                lol.data = data[1];
                res.Add(lol);
            }

            for (int i = 0; i < checkData.Length; i++)
                checkData[i] = false;
            return res;
        }
    }

    class ValveTop: Top // 1 - вход, 1 выход, 1 управляющий (data[0])
    {
        public ValveTop() : base()
        {
            data = new int[2];
            checkData = new bool[2];
        }
        override public List<(int id, int i, int data)> work()
        {
            if (data[0] != 0)
            {
                (int id, int i, int data) lol = output[0];
                lol.data = data[1];
                output[0] = lol;
            }

            for (int i = 0; i < checkData.Length; i++)
                checkData[i] = false;
            
            return output;
        }
    }

    class InputTop: Top // 1 выход
    {
        public Queue<int> input;

        public InputTop() : base()
        {
            checkData = new bool[1];
            checkData[0] = true;
        }

        override public List<(int id, int i, int data)> work()
        {
            (int id, int i, int data) lol = output[0];
            lol.data = input.Dequeue();
            output[0] = lol;

            if (input.Count == 0)
            {
                checkData[0] = false;
            }

            
            return output;
        }
    }

    class OutputTop: Top // 1 вход
    {
        public OutputTop() : base()
        {
            data = new int[1];
            checkData = new bool[1];
        }

        override public List<(int id, int i, int data)> work()
        {
            // вывод data[0]...
            return new List<(int id, int i, int data)>();
        }
    }
}
