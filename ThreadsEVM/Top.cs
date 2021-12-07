using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{
    abstract class Top
    {
        public struct Output
        {
            public int idTop;
            public int idIn;
            public int data;
        }

        // Comand
        public int[] data; // (opr) - входные данные
        public Output[] outputs; // (des) - дуги по которым идет результат

        // Control
        public bool[] checkData; // (pres) - флаги получения входных данных

        public Top() {}

        public void reload()
        {
            Array.Fill(checkData, false);
        }

        virtual public bool isReady()
        {
            foreach (var flag in checkData)
                if (!flag)
                    return false;
            return true;
        }

        abstract public Output[] work();
    }

    class OperTop1: Top // 1-2 (n) - входа, 1 (n) выход
    {
        public Operations.Func func;

        public OperTop1(): base()
        {
            data = new int[1];
            checkData = new bool[1];
        }

        override public Output[] work()
        {
            int outData = func(data);

            outputs[0].data = outData;

            Array.Fill(checkData, false);

            return outputs;
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

        override public Output[] work()
        {
            int outData = func(data);

            outputs[0].data = outData;

            Array.Fill(checkData, false);

            return outputs;
        }
    }

    class BranchTop: Top // 1 - вход, 2 (n) выхода
    {
        public BranchTop() : base()
        {
            data = new int[1];
            checkData = new bool[1];
        }

        override public Output[] work()
        {
            outputs[0].data = data[0];
            outputs[1].data = data[0];

            Array.Fill(checkData, false);

            return outputs;
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

        override public Output[] work()
        {
            for (int i = 0; i < checkData.Length; i++)
                if (checkData[i])
                    outputs[0].data = data[i];

            Array.Fill(checkData, false);

            return outputs;
        }
    }

    class TFTop: Top // 1 - вход, 2 выхода, 1 управляющий (data[0])
    {
        public TFTop() : base()
        {
            data = new int[2];
            checkData = new bool[2];
        }

        override public Output[] work()
        {
            Output[] res = new Output[1];

            if (data[0] != 0)
                res[0] = outputs[0];            
            else
                res[0] = outputs[1];

            res[0].data = data[1];

            Array.Fill(checkData, false);

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
        override public Output[] work()
        {
            Array.Fill(checkData, false);

            if (data[0] == 0)
                return new Output[0];

            outputs[0].data = data[1];

            return outputs;
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

        override public bool isReady()
        {
            return input.Count > 0;
        }

        override public Output[] work()
        {
            outputs[0].data = input.Dequeue();
            
            return outputs;
        }
    }

    class OutputTop: Top // 1 вход
    {
        public OutputTop() : base()
        {
            data = new int[1];
            checkData = new bool[1];
        }

        override public Output[] work()
        {
            return new Output[0];
        }
    }
}
