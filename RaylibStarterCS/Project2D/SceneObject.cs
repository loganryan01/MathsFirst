using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2D
{
    class SceneObject
    {
        protected SceneObject parent = null;
        protected List<SceneObject> children = new List<SceneObject>();

        public SceneObject Parent
        {
            get { return parent; }
        }
        
        public SceneObject()
        {

        }

        public int GetChildCount()
        {
            return children.Count;
        }

        public SceneObject GetChild(int index)
        {
            return children[index];
        }
    }
}
