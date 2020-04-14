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

        public bool Intersects(AABB aabb, Vector3 I = null, Vector3 R = null)
        {
            // get distances to each axis of the box
            float xmin, xmax, ymin, ymax;

            // get min and max in the x-axis
            if (direction.x < 0)
            {
                xmin = (aabb.max.x - origin.x) / direction.x;
                xmax = (aabb.min.x - origin.x) / direction.x;
            }
            else
            {
                xmin = (aabb.min.x - origin.x) / direction.x;
                xmax = (aabb.max.x - origin.x) / direction.x;
            }

            // get min and max in the y-axis
            if (direction.y < 0)
            {
                ymin = (aabb.max.y - origin.y) / direction.y;
                ymax = (aabb.min.y - origin.y) / direction.y;
            }
            else
            {
                ymin = (aabb.min.y - origin.y) / direction.y;
                ymax = (aabb.max.y - origin.y) / direction.y;
            }

            // ensure within box
            if (xmin > ymax || ymin > xmax)
            {
                return false;
            }

            // the first contact is the largest of the two min
            float t = Math.Max(xmin, ymin);

            // intersects if within range
            if (t >= 0 && t <= length)
            {
                // store intersection point if requested
                if (I != null)
                {
                    I = origin + direction * t;
                }

                if (R != null)
                {
                    // need to determine box side hit
                    Vector3 N;
                    if (t == xmin)
                    {
                        // horizontal normal
                        if (direction.x < 0)
                        {
                            // right side
                            N = new Vector3(1, 0, 1);
                        }
                        else
                        {
                            // left side
                            N = new Vector3(-1, 0, 1);
                        }
                    }
                    else
                    {
                        // vertical normal
                        if (direction.y < 0)
                        {
                            // top
                            N = new Vector3(0, 1, 1);
                        }
                        else
                        {
                            // bottom
                            N = new Vector3(0, -1, 1);
                        }
                    }

                    // get penetration vector
                    Vector3 P = direction * (length - t);

                    // get penetration amount
                    float p = P.Dot(N);

                    // get reflected vector
                    R = N * -2 * p + P;
                }

                return true;
            }

            // not within Ray's range
            return false;
        }

        public bool Intersects(Plane plane, Vector3 I = null, Vector3 R = null)
        {
            float t = direction.Dot(plane.N);
            if (t > 0)
            {
                return false;
            }

            float d = origin.Dot(plane.N) + plane.d;

            if (t == 0 && d != 0)
            {
                return false;
            }

            t = d == 0 ? 0 : -(d / t);

            if (t >= 0 &&
                t <= length)
            {
                if (I != null)
                {
                    I = origin + direction * t;
                }

                if (R != null)
                {
                    Vector3 P = direction * (length - t);
                    float p = P.Dot(plane.N);
                    R = plane.N * -2 * p + P;
                }

                return true;
            }

            return false;
        }
    }
}
