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
        Vector3 N;
        float d;

        public Plane()
        {

        }

        public Plane(float x, float y, float d)
        {
            this.N = new Vector3(x, y, 0);
            this.d = d;
        }

        public Plane(Vector3 n, float d)
        {
            this.N = n;
            this.d = d;
        }
    }
}
