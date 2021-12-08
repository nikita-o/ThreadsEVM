using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{
    static class Operations
    {
        public delegate int Func(int[] data);

        static public int Sum(int[] data)
        {
            return data[0] + data[1];
        }

        static public int Mul(int[] data)
        {
            return data[0] * data[1];
        }

        static public int Div(int[] data)
        {
            return data[0] / data[1];
        }

        static public int Mod(int[] data)
        {
            return data[0] % data[1];
        }

        static public int Inc(int[] data)
        {
            return ++data[0];
        }

        static public int Dec(int[] data)
        {
            return --data[0];
        }

        static public int  Max(int[] data)
        {
            return data[0] > data[1] ? data[0] : data[1];
        }

        static public int Min(int[] data)
        {
            return data[0] < data[1] ? data[0] : data[1];
        }

        static public int Equals(int[] data)
        {
            return data[0] == data[1] ? 1 : 0;
        }

        static public int constTrue(int[] data)
        {
            return 1;
        }

        static public int constFalse(int[] data)
        {
            return 0;
        }
    }
}
