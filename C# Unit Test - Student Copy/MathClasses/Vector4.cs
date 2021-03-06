﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector4
    {
        public float x, y, z, w;

        // ---- CONSTRUCTORS ----

        // Default Vector4 constructor
        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        // Creates a 3-D Homogeneous Vector
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        // ---- IMPLEMENTING MATHS OPERATORS ----

        // Overloads the + operator
        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }

        // Overloads the - operator
        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        // Overloads the * operator
        public static Vector4 operator *(Vector4 v1, float s2)
        {
            return new Vector4(
                v1.x * s2,
                v1.y * s2,
                v1.z * s2,
                v1.w * s2);
        }

        // Overloads the * operator
        public static Vector4 operator *(float s1, Vector4 v2)
        {
            return v2 * s1;
        }

        // ---- DOT AND CROSS PRODUCT ----

        // Vector Dot Product 
        public float Dot(Vector4 rhs)
        {
            return x * rhs.x + y * rhs.y + z * rhs.z + w * rhs.w;
        }

        //  Cross Product - 4-D Perpendicular Vectors
        public Vector4 Cross(Vector4 rhs)
        {
            return new Vector4(
                y * rhs.z - z * rhs.y,
                z * rhs.x - x * rhs.z,
                x * rhs.y - y * rhs.x,
                0);
        }

        // ---- MAGNITUDE AND NORMALISATION ----

        // Calculating Magnitude
        public float Magnitude()
        {
            return (float)Math.Sqrt((double)(x * x + y * y + z * z + w * w));
        }

        // Vector Normalisation
        public void Normalize()
        {
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
            this.w /= m;
        }
    }
}
