using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector4
    {
        public float x, y, z, w;

        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        public static Vector4 operator *(Vector4 v1, float s2)
        {
            return new Vector4(
                v1.x * s2,
                v1.y * s2,
                v1.z * s2,
                v1.w * s2);
        }

        public static Vector4 operator *(float s1, Vector4 v2)
        {
            return v2 * s1;
        }

        public float Dot(Vector4 rhs)
        {
            return x * rhs.x + y * rhs.y + z * rhs.z + w * rhs.w;
        }
    }
}
