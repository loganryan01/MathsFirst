using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

namespace Project2D
{
    class Sphere
    {
        public Vector3 center;
        public float radius;

        public Sphere()
        {

        }

        public Sphere(Vector3 p, float r)
        {
            this.center = p;
            this.radius = r;
        }

        public bool Overlaps(Sphere other)
        {
            Vector3 diff = other.center - center;
            float r = radius + other.radius;
            return diff.MagnitudeSqr() <= (r * r);
        }

        public bool Overlaps(AABB aabb)
        {
            Vector3 diff = aabb.ClosestPoint(center) - center;
            return diff.Dot(diff) <= (radius * radius);
        }
    }
}
