using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {
        public UInt32 colour; // 4-byte colour value

        // ---- CONSTRUCTORS ----

        public Colour()
        {
            colour = 0; //Sets colour to zero
        }

        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            colour |= (UInt32)red << 24; // move the red component into the correct position in a 4-byte value
            colour |= (UInt32)green << 16; // move the green component into the correct position in a 4-byte value
            colour |= (UInt32)blue << 8; // move the blue component into the correct position in a 4-byte value
            colour |= (UInt32)alpha << 0; // move the alpha component into the correct position in a 4-byte value
        }

        // ---- RED VALUES ----

        public byte GetRed() 
        {
            return (byte)((colour & 0xff000000) >> 24); // retrieves the 8-bit red component from a 4-byte colour value
        }
        public void SetRed(byte red) 
        {
            colour = colour & 0x00ffffff; // clear the red component
            colour |= (UInt32)red << 24; // move the red component into the correct position in a 4-byte value
        }

        // ---- GREEN VALUES ----

        public byte GetGreen() 
        {
            return (byte)((colour & 0x00ff0000) >> 16); // retrieves the 8-bit green component from a 4-byte colour value
        }
        public void SetGreen(byte green) 
        {
            colour = colour & 0xff00ffff; // clear the green component
            colour |= (UInt32)green << 16; // move the green component in to the correct position in a 4-byte value
        }

        // ---- BLUE VALUES ----

        public byte GetBlue() 
        {
            return (byte)((colour & 0x0000ff00) >> 8); // retrieves the 8-bit blue component from a 4-byte colour value
        }
        public void SetBlue(byte blue) 
        {
            colour = colour & 0xffff00ff; // clear the blue component
            colour |= (UInt32)blue << 8; // move the blue in to the correct position in a 4-byte value
        }

        // ---- ALPHA VALUES ----

        public byte GetAlpha() 
        {
            return (byte)((colour & 0x000000ff) >> 0); // retrieves the 8-bit alpha component from a 4-byte colour value
        }
        public void SetAlpha(byte alpha) 
        {
            colour = colour & 0xffffff00; // clear the alpha component
            colour |= (UInt32)alpha << 0; // move the alpha component in to the correct position from a 4-byte colour value
        }

        // ---- QUESTION 6 ----

        public void MoveRed()
        {
            SetGreen(GetRed()); // Set the green value equal to the red value
            SetRed(0x00); // Set red to 00
        }

        // ---- CONVERT TO HEXADECIMAL ----

        public string ToHex()
        {
            string hexString = colour.ToString("X");
            return hexString;
        }

        public string ToBinary()
        {
            string hexString = colour.ToString("X");
            string binaryString = String.Join(String.Empty,
                hexString.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );
            return binaryString;
        }
    }
}
