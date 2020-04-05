using MathClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2D
{
    class AABB
    {
        Vector3 min = new Vector3(float.NegativeInfinity,
                                  float.NegativeInfinity,
                                  float.NegativeInfinity);
        Vector3 max = new Vector3(float.PositiveInfinity,
                                  float.PositiveInfinity,
                                  float.PositiveInfinity);

        public AABB()
        {

        }

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public bool IsEmpty()
        {
            if (float.IsNegativeInfinity(min.x) &&
                float.IsNegativeInfinity(min.y) &&
                float.IsNegativeInfinity(min.z) &&
                float.IsInfinity(max.x) &&
                float.IsInfinity(max.y) &&
                float.IsInfinity(max.z))
                return true;
            return false;
        }

        public void Empty()
        {
            min = new Vector3(float.NegativeInfinity,
                              float.NegativeInfinity,
                              float.NegativeInfinity);
            max = new Vector3(float.PositiveInfinity,
                              float.PositiveInfinity,
                              float.PositiveInfinity);
        }

        void SetToTransformedBox(AABB box, Matrix3 m)
        {
            // If we're empty, then exit (an empty box defined as having the min/max
            // set to infinity)
            if (box.IsEmpty())
            {
                Empty();
                return;
            }

            // Examine each of the nine matrix elements
            // and compute the new AABB
            if (m.m1 > 0.0f) // m1 = m11 in the formula above
            {
                min.x += m.m1 * box.min.x; max.x += m.m1 * box.max.x;
            }
            else
            {
                min.x += m.m1 * box.max.x; max.x += m.m1 * box.min.x;
            }
            if (m.m2 > 0.0f) // m2 = m12 in the formula above
            {
                min.y += m.m2 * box.min.x; max.y += m.m2 * box.max.x;
            }
            else
            {
                min.y += m.m2 * box.max.x; max.y += m.m2 * box.min.x;
            }
            if (m.m3 > 0.0f) // m3 = m13 in the formula above
            {
                min.z += m.m3 * box.min.x; max.z += m.m3 * box.max.x;
            }
            else
            {
                min.z += m.m3 * box.max.x; max.z += m.m3 * box.min.x;
            }
            if (m.m4 > 0.0f) // m4 = m14 in the formula above
            {
                min.x += m.m4 * box.min.x; max.x += m.m4 * box.max.x;
            }
            else
            {
                min.x += m.m4 * box.max.x; max.x += m.m4 * box.min.x;
            }
            if (m.m5 > 0.0f) // m5 = m15 in the formula above
            {
                min.y += m.m5 * box.min.x; max.y += m.m5 * box.max.x;
            }
            else
            {
                min.y += m.m5 * box.max.x; max.y += m.m5 * box.min.x;
            }
            if (m.m6 > 0.0f) // m6 = m16 in the formula above
            {
                min.z += m.m6 * box.min.x; max.z += m.m6 * box.max.x;
            }
            else
            {
                min.z += m.m6 * box.max.x; max.z += m.m6 * box.min.x;
            }
            if (m.m7 > 0.0f) // m7 = m17 in the formula above
            {
                min.x += m.m7 * box.min.x; max.x += m.m7 * box.max.x;
            }
            else
            {
                min.x += m.m7 * box.max.x; max.x += m.m7 * box.min.x;
            }
            if (m.m8 > 0.0f) // m8 = m18 in the formula above
            {
                min.y += m.m8 * box.min.x; max.y += m.m8 * box.max.x;
            }
            else
            {
                min.y += m.m8 * box.max.x; max.y += m.m8 * box.min.x;
            }
            if (m.m9 > 0.0f) // m9 = m19 in the formula above
            {
                min.z += m.m9 * box.min.x; max.z += m.m9 * box.max.x;
            }
            else
            {
                min.z += m.m9 * box.max.x; max.z += m.m9 * box.min.x;
            }

            public Vector3 Center()
            {
                return (min + max) * 0.5f;
            }
        }
    }
}
