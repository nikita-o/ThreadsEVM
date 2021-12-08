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
        public int[] Data { get; set; } // (opr) - входные данные
        public Output[] Outputs { get; set; } // (des) - дуги по которым идет результат

        // Control
        public bool[] CheckData { get; set; } // (pres) - флаги получения входных данных

        public Top() {}

        public void reload()
        {
            Array.Fill(CheckData, false);
        }

        virtual public bool isReady()
        {
            foreach (var flag in CheckData)
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
            Data = new int[1];
            CheckData = new bool[1];
        }

        override public Output[] work()
        {
            int outData = func(Data);

            Outputs[0].data = outData;

            Array.Fill(CheckData, false);

            return Outputs;
        }
    }

    class OperTop2 : Top // 1-2 (n) - входа, 1 (n) выход
    {

        public Operations.Func func;

        public OperTop2() : base()
        {
            Data = new int[2];
            CheckData = new bool[2];
        }

        override public Output[] work()
        {
            int outData = func(Data);

            Outputs[0].data = outData;

            Array.Fill(CheckData, false);

            return Outputs;
        }
    }

    class BranchTop: Top // 1 - вход, 2 (n) выхода
    {
        public BranchTop() : base()
        {
            Data = new int[1];
            CheckData = new bool[1];
        }

        override public Output[] work()
        {
            Outputs[0].data = Data[0];
            Outputs[1].data = Data[0];

            Array.Fill(CheckData, false);

            return Outputs;
        }
    }

    class MergeTop: Top // 2 (n) - входа, 1 выход
    {
        public MergeTop() : base()
        {
            Data = new int[2];
            CheckData = new bool[2];
        }

        override public bool isReady()
        {
            foreach (bool flag in CheckData)
                if (flag)
                    return true;
            return false;
        }

        override public Output[] work()
        {
            for (int i = 0; i < CheckData.Length; i++)
                if (CheckData[i])
                    Outputs[0].data = Data[i];

            Array.Fill(CheckData, false);

            return Outputs;
        }
    }

    class TFTop: Top // 1 - вход, 2 выхода, 1 управляющий (data[0])
    {
        public TFTop() : base()
        {
            Data = new int[2];
            CheckData = new bool[2];
        }

        override public Output[] work()
        {
            Output[] res = new Output[1];

            if (Data[0] != 0)
                res[0] = Outputs[0];            
            else
                res[0] = Outputs[1];

            res[0].data = Data[1];

            Array.Fill(CheckData, false);

            return res;
        }
    }

    class ValveTop: Top // 1 - вход, 1 выход, 1 управляющий (data[0])
    {
        public ValveTop() : base()
        {
            Data = new int[2];
            CheckData = new bool[2];
        }
        override public Output[] work()
        {
            Array.Fill(CheckData, false);

            if (Data[0] == 0)
                return new Output[0];

            Outputs[0].data = Data[1];

            return Outputs;
        }
    }

    class InputTop: Top // 1 выход
    {
        public Queue<int> input;

        public InputTop() : base()
        {
            CheckData = new bool[1];
            CheckData[0] = true;
        }

        override public bool isReady()
        {
            return input.Count > 0;
        }

        override public Output[] work()
        {
            Outputs[0].data = input.Dequeue();
            
            return Outputs;
        }
    }

    class OutputTop: Top // 1 вход
    {
        public OutputTop() : base()
        {
            Data = new int[1];
            CheckData = new bool[1];
        }

        override public Output[] work()
        {
            return new Output[0];
        }
    }
}
