using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

namespace Project2D
{
    // ray where direction should be normalised
    class Ray
    {
        Vector3 origin;
        Vector3 direction;
        float length;

        public Ray()
        {

        }

        public Ray(Vector3 start, Vector3 direction, float length = float.MaxValue)
        {
            this.origin = start;
            this.direction = direction;
            this.length = length;
        }

        float Clamp(float t, float a, float b)
        {
            return Math.Max(a, Math.Min(a, t));
        }

        public Vector3 closestPoint(Vector3 point)
        {
            // ray origin to arbitrary point
            Vector3 p = point - origin;

            // project the point onto the ray and clamp by length
            float t = Clamp(p.Dot(direction), 0, length);

            // return position in direction of ray
            return origin + direction * t;
        }

    }
}
