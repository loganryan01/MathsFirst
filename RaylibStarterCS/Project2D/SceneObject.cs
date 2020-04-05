using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void AddChild(SceneObject child)
        {
            // make sure it doesn't have a parent already
            Debug.Assert(child.parent == null);
            // assign "this as parent
            child.parent = this;
            // add new child to collection
            children.Add(child);
        }

        public void RemoveChild(SceneObject child)
        {
            if ( children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        ~SceneObject()
        {
            if(parent != null)
            {
                parent.RemoveChild(this);
            }

            foreach(SceneObject so in children)
            {
                so.parent = null;
            }
        }
    }
}
