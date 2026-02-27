using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Barco b1 = new Barco("pito", 3, 'v', new Coordenada(3, 4));
            Barco b2 = new Barco("caca", 1, 'h', new Coordenada(0, 0));

            Console.WriteLine(b1.ToString());
            Console.WriteLine(b2.ToString());
        }
    }
}
