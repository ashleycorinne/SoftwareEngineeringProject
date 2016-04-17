using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var nums = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                nums.Add(random.Next(10000));
            }

            var test = nums.Where(i => i > 5000).OrderBy(i => i);

            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }
    }
}
