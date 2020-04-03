using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {
        public UInt32 colour;

        public Colour()
        {
            colour = 0;
        }

        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            colour = 0x12345678;
        }

        public byte GetRed() 
        {
            return (byte)((colour & 0xff000000));
        }
        public void SetRed(byte red) 
        {
            colour = colour & 0x00ffffff;
            colour |= (UInt32)red << 24;
        }

        public byte GetGreen() {}
        public void SetGreen(byte green) {}

        public byte GetBlue() {}
        public void SetBlue(byte blue) {}

        public byte GetAlpha() {}
        public void SetAlpha(byte alpha) {}
    }
}
