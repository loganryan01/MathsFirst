using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

namespace Project2D
{
    // 2D mathematical plane using Ax + By + d = 0
    class Plane
    {
        // Member properties
        public Vector3 N = new Vector3(0, 0, 0);
        public float d;

        // Enumeration to easily define the sides of the plane
        public enum ePlaneResult : int 
        {
            FRONT = 1,
            BACK = -1,
            INTERSECTS = 0
        }

        // Constructor where we set these values directly
        public Plane()
        {

        }

        // Constructor where we set these values by passing in3 floats.
        public Plane(float x, float y, float d)
        {
            this.N = new Vector3(x, y, 0);
            this.d = d;
        }

        // Constructor where we set these values by passing in a vector and a float
        public Plane(Vector3 n, float d)
        {
            this.N = n;
            this.d = d;
        }

        // Constructor that takes in two position
        public Plane(Vector3 p1, Vector3 p2)
        {
            // calculate normalised vector p0 to p1
            Vector3 v = p2 - p1;
            v.Normalize();

            // set normal perpendicular to the vector
            N.x = -v.y;
            N.y = v.x;

            // calculate d
            d = -p1.Dot(N);
        }

        // Distance to Plane
        public float DistanceTo(Vector3 p)
        {
            return p.Dot(N) + d;
        }

        // Closest point to a Plane
        public Vector3 ClosestPoint(Vector3 p)
        {
            return p - N * DistanceTo(p);
        }

        // Determine which side of a plane a point is
        public ePlaneResult TestSide(Vector3 p)
        {
            float t = p.Dot(N) + d;

            if (t < 0)
            {
                return ePlaneResult.BACK;
            }
            else if (t > 0)
            {
                return ePlaneResult.FRONT;
            }
            return ePlaneResult.INTERSECTS;
        }

        // Determine which side of a plane a sphere is on
        public ePlaneResult TestSide(Sphere sphere)
        {
            float t = DistanceTo(sphere.center);

            if (t > sphere.radius)
            {
                return ePlaneResult.FRONT;
            }
            else if (t < -sphere.radius)
            {
                return ePlaneResult.BACK;
            }

            return ePlaneResult.INTERSECTS;
        }
        
        // Determine which side of a plane a box is on
        public ePlaneResult TestSide(AABB aabb)
        {
            // tag if we find a corner on each side
            bool[] side = new bool[2] { false, false };

            // compare each corner
            foreach (Vector3 c in aabb.Corners())
            {
                ePlaneResult result = TestSide(c);
                if (result == ePlaneResult.FRONT)
                {
                    side[0] = true;
                }
                else if (result == ePlaneResult.BACK)
                {
                    side[1] = true;
                }
            }

            // if front but not back
            if (side[0] && !side[1])
            {
                return ePlaneResult.FRONT;
            }
            // if back but not front
            else if (!side[0] && side[1])
            {
                return ePlaneResult.BACK;
            }
            // else overlapping
            return ePlaneResult.INTERSECTS;
        }
    }
}
