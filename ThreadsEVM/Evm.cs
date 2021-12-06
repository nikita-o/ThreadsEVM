using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsEVM
{

    class Evm
    {
        Queue<Top> mem;

        void start()
        {
            while (mem.Count > 0)
            {
                foreach (var top in mem.Dequeue().work())
                {
                    if (top.isReady())
                        mem.Enqueue(top);
                }
            }
        }
    }
}
