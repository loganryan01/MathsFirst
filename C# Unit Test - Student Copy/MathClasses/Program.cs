using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    class Program
    {
        // Enter the values in hex number
        static Colour c = new Colour(94, 0, 0, 0);

        static void Main(string[] args)
        {
            c.MoveRed(); // Moves values from red to green
            Console.WriteLine(c.colour); // Prints decimal numbers
            Console.WriteLine(c.ToHex()); // Prints hex numbers
            Console.WriteLine(c.ToBinary()); // Prints binary numbers
            Console.ReadKey();
        }
    }
}
