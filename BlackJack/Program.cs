using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            //for correct view of suites
            Console.OutputEncoding = Encoding.Unicode;

            Game game = new Game();

            GC.Collect();
        }
    }
}
