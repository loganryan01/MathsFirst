using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector3
    {
        public float x, y, z;

        // Default Vector3 Constructor
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        // Creates a 2-D Homogeneous Vector
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // Overloads the + operator
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        // Overloads the - operator
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        // Overloads the * operator
        public static Vector3 operator *(Vector3 v1, float s2)
        {
            return new Vector3(
                v1.x * s2,
                v1.y * s2,
                v1.z * s2);
        }

        // Overloads the * operator
        public static Vector3 operator *(float s1, Vector3 v2)
        {
            return v2 * s1;
        }

        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            return new Vector3(
                lhs.x / rhs,
                lhs.y / rhs,
                lhs.z / rhs);
        }

        // Vector Dot Product
        public float Dot(Vector3 rhs)
        {
            return x * rhs.x + y * rhs.y + z * rhs.z;
        }

        // Cross Product - 3-D Perpendicular Vectors
        public Vector3 Cross(Vector3 rhs)
        {
            return new Vector3(
                y * rhs.z - z * rhs.y,
                z * rhs.x - x * rhs.z,
                x * rhs.y - y * rhs.x);
        }

        // Calculating Magnitude
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        // Calculating Magnitude
        public float MagnitudeSqr() 
        { 
            return (x * x + y * y + z * z); 
        }

        // Vector Normalisation
        public void Normalize()
        {
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
        }

        public Vector3 GetNormalised() 
        { 
            return (this / Magnitude()); 
        }

        // Returns a vector whose componenets are created from the minimum components of the two passed in parameter Vectors
        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        // Returns a vector whose componenets are created from the maximum components of the two passed in parameter Vectors
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        // Clamps a specified value within a range specified by minimum and maximum values.
        public static Vector3 Clamp(Vector3 t, Vector3 a, Vector3 b)
        {
            return Max(a, Min(b, t));
        }
    }
}
