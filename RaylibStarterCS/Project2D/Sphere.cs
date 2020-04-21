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
        public Vector3 center; // Center of the sphere
        public float radius; // Radius of the sphere

        // Default Constructor
        public Sphere()
        {

        }

        // Creates a simple sphere
        public Sphere(Vector3 p, float r)
        {
            this.center = p;
            this.radius = r;
        }

        // Test if two spheres overlap
        public bool Overlaps(Sphere other)
        {
            Vector3 diff = other.center - center;
            float r = radius + other.radius;
            return diff.MagnitudeSqr() <= (r * r);
        }

        // Tests if a sphere overlaps an Axis-Aligned Bounding box (AABB)
        public bool Overlaps(AABB aabb)
        {
            Vector3 diff = aabb.ClosestPoint(center) - center;
            return diff.Dot(diff) <= (radius * radius);
        }
    }
}
