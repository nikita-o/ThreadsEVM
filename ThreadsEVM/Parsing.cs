﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{
    class Parsing
    {
        public int[,] matrix_out;
        public List<int> inputs = new List<int>(), outputs = new List<int>();
        int id,
            out_count,
            inp_count;
        public List<Top> tops;

        //проверяем максимальное колличество входов и выходов у вершины
        (int, int, Top) check(String str, string name)
        {
            Top top;
            switch (str.ToLower())
            {
                case "input":
                    out_count = 1;
                    inp_count = 0;
                    top = new InputTop();
                    break;
                case "oper1":
                    out_count = 1;
                    inp_count = 1;
                    top = new OperTop();
                    ((OperTop)top).func = check_func(name);
                    break;
                case "oper2":
                    out_count = 1;
                    inp_count = 2;
                    top = new OperTop();
                    ((OperTop)top).func = check_func(name);
                    break;
                case "branch":
                    out_count = 2;
                    inp_count = 1;
                    top = new BranchTop();
                    break;
                case "merge":
                    out_count = 1;
                    inp_count = 2;
                    top = new MergeTop();
                    break;
                case "tf":
                    out_count = 2;
                    inp_count = 2;
                    top = new TFTop();
                    break;
                case "valve":
                    out_count = 1;
                    inp_count = 2;
                    top = new ValveTop();
                    break;
                case "output":
                    out_count = 0;
                    inp_count = 1;
                    top = new OutputTop();
                    break;
                default:
                    throw new Exception("Wrong top name");
                    break;
            }
            return (inp_count, out_count, top);
        }

        OperTop.Func check_func(String str)
        {
            switch (str.ToLower())
            {
                case "sum":
                    return Operations.Sum;
                case "mul":
                    return Operations.Mul;
                case "div":
                    return Operations.Div;
                case "mod":
                    return Operations.Mod;
                case "inc":
                    return Operations.Inc;
                case "dec":
                    return Operations.Dec;
                case "more":
                    return Operations.More;
                case "small":
                    return Operations.Small;
                case "equals":
                    return Operations.Equals;
                case "true":
                    return Operations.constTrue;
                case "false":
                    return Operations.constFalse;
                default:
                    throw new Exception("Wrong func name");
                    break;
            }
        }
        public void parsing(String[] text)
        {
            string[] str;
            List<string[]> save_str = new List<string[]>();


            //matrix_inp = new int[text.Length, text.Length];
            matrix_out = new int[text.Length, text.Length];
            tops = new List<Top>();
            for (int i = 0; i < text.Length; i++)
            {
                str = text[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                save_str.Add(str);
                if (str.Length != 5)
                {
                    throw new Exception("Wrong Count params");
                }

                try
                {
                    id = Int32.Parse(str[0]);
                }
                catch (FormatException)
                {

                    throw new Exception($"Wrong Format, {i} str ");
                }


                (int inp, int outp, Top top) = check(str[1], str[2]);
                tops.Add(top);

                for (int j = 0; j < outp; j++)
                {
                    string[] string_out = str[3 + j].Split('-');
                    int out1 = Int32.Parse(string_out[0]);
                    matrix_out[id, out1] = 1;
                }

            }
            ////PROVERKA
            int[] line_sum = new int[text.Length], column_sum = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {

                for (int j = 0; j < text.Length; j++)
                {
                    line_sum[i] += matrix_out[i, j];
                    column_sum[j] += matrix_out[i, j];
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                id = Int32.Parse(save_str[i][0]);
                (int inp, int outp, Top top) = check(save_str[i][1], save_str[i][2]);

                if (outp == 1 && inp == 0)
                {
                    inputs.Add(id);
                }

                if (outp == 0 && inp == 1)
                {
                    outputs.Add(id);
                }

                if (outp < line_sum[i] || inp < column_sum[i])
                {
                    throw new Exception($"Wrong Count, {i} str ");
                }
            }
            ////////////////////////////////////////////////////////////////
            ///
            for (int i = 0; i < text.Length; i++)
            {
                //save_str[i][3];
                (int inp, int outp, Top top) = check(save_str[i][1], save_str[i][2]);
                id = Int32.Parse(save_str[i][0]);
                tops[id].output = new List<(int id, int i, int data)>();

                for (int j = 0; j < outp; j++)
                {
                    string[] string_out = save_str[i][3 + j].Split('-');
                    int out1 = Int32.Parse(string_out[0]);
                    int out2 = Int32.Parse(string_out[1]);
                    tops[id].output.Add((out1, out2, 0));

                }
            }





        }

    }
}
