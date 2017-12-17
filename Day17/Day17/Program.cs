using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var stepSize = 348;

            var list = new List<int> {0};
            var index = 0;

            for (int value = 1; value < 2018; value++)
            {
                index = (index + stepSize) % list.Count + 1;
                list.Insert(index, value);
            }

            Console.WriteLine(list[index + 1]);

            list = new List<int> { 0 };
            index = 0;
            var listlegth = 1;
            var valueAfter = 0;

            for (int value = 1; value < 50000001; value++)
            {
                index = (index + stepSize) % listlegth + 1;
                listlegth++;

                if (index == 1)
                    valueAfter = value;
            }

            Console.WriteLine(valueAfter);
            Console.ReadKey();
        }
    }
}
