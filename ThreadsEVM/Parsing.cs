using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{
    class Parsing
    {
        int[,] matrix_inp, matrix_out;

        int id,
            out_count,
            inp_count;
        Top[] tops;
        
        //проверяем максимальное колличество входов и выходов у вершины
        (int, int, Top) check(String str)
        {
            Top top;
            switch (str.ToLower())
            {
                case "input":
                    out_count = 1;
                    inp_count = 0;
                    top = new OperTop();
                    break;
                case "oper1":
                    out_count = 1;
                    inp_count = 1;
                    top = new OperTop();
                    break;
                case "oper2":
                    out_count = 1;
                    inp_count = 2;
                    top = new OperTop();
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
                    top = new OutTop();
                    break;
                default:
                    throw new Exception("Wrong top name");
                    break;
            }
            return (inp_count, out_count, top);
        }
        public void parsing(String[] text)
        {
            string[] str;
            List<string[]> save_str = new List<string[]>();


            //matrix_inp = new int[text.Length, text.Length];
            matrix_out = new int[text.Length, text.Length];
            tops = new Top[text.Length];
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


                (int inp, int outp, Top top) = check(str[1]);

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
                (int inp, int outp, Top top) = check(save_str[i][1]);
                if (outp < line_sum[i] || inp < column_sum[i])
                {
                    throw new Exception($"Wrong Count, {i} str ");
                }
            }

            //int[] line_sum = new int[text.Length], column_sum = new int[text.Length];
            //for (int i = 0; i < text.Length; i++)
            //{

            //    for (int j = 0; j < text.Length; j++)
            //    {
            //        line_sum[i] += matrix_out[i, j];
            //        column_sum[j] += matrix_out[j, i];
            //    }
            //    id = Int32.Parse(save_str[i][0]);
            //    (int inp, int outp, Top top) = check(save_str[i][1]);
            //    if (outp < line_sum[i] || inp < column_sum[i])
            //    {
            //        throw new Exception($"Wrong Count, {i} str ");
            //    }
            //}



        }

    }
}
