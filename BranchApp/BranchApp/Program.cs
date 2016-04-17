using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            var items = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                items.Add(rand.Next(10000));
            }

            var things = items.Where(i => i > 5000).OrderBy(i => i);

            foreach (var item in things)
            {
                Console.WriteLine(item);
            }
        }
    }
}
