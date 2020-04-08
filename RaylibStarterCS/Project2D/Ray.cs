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
    }
}
