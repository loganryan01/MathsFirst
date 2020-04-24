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
        static Colour c = new Colour(0x12, 0x34, 0x56, 0x78);

        static void Main(string[] args)
        {
            Console.WriteLine(c.colour); // Prints decimal numbers
            Console.WriteLine(c.ToHex()); // Prints hex numbers
            Console.WriteLine(c.ToBinary()); // Prints binary numbers
            c.MoveRed();
            Console.WriteLine("After Red value has been moved into the green value");
            Console.WriteLine(c.colour); // Prints decimal numbers
            Console.WriteLine(c.ToHex()); // Prints hex numbers
            Console.WriteLine(c.ToBinary()); // Prints binary numbers
            Console.ReadKey();
        }
    }
}
