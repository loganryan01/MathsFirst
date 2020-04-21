using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

namespace Project2D
{
    public class SceneObject
    {
        // Member variables
        protected SceneObject parent = null;
        protected List<SceneObject> children = new List<SceneObject>();

        // Transforms
        protected Matrix3 localTransform = new Matrix3();
        protected Matrix3 globalTransform = new Matrix3();

        // Access local transform
        public Matrix3 LocalTransform
        {
            get { return localTransform; }
        }

        // Access global transform
        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
        }

        // Returns parent of the child
        public SceneObject Parent
        {
            get { return parent; }
        }
        
        // Constructor
        public SceneObject()
        {

        }

        // How many children an object has
        public int GetChildCount()
        {
            return children.Count;
        }

        // Access a certain child by an index
        public SceneObject GetChild(int index)
        {
            return children[index];
        }

        // Add a child scene object to another scene object
        public void AddChild(SceneObject child)
        {
            // make sure it doesn't have a parent already
            Debug.Assert(child.parent == null);
            // assign "this as parent
            child.parent = this;
            // add new child to collection
            children.Add(child);
        }

        // Removes a child scene object to another scene object
        public void RemoveChild(SceneObject child)
        {
            if ( children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        // Implement specific derived behaviours
        public virtual void OnUpdate(float deltaTime)
        {

        }

        // Implement specific derived drawing behaviours
        public virtual void OnDraw()
        {

        }

        // Updates all children
        public void Update(float deltaTime)
        {
            // run OnUpdate behaviour
            OnUpdate(deltaTime);

            // update children
            foreach (SceneObject child in children)
            {
                child.Update(deltaTime);
            }
        }

        // Draws all children
        public void Draw()
        {
            // run OnDraw behaviour
            OnDraw();

            // draw children
            foreach (SceneObject child in children)
            {
                child.Draw();
            }
        }

        // A transform copies another transform
        public void CopyTransform(Matrix3 newTransform)
        {
            localTransform = newTransform;
            UpdateTransform();
        }

        // Updates the global transform
        public void UpdateTransform()
        {
            if (parent != null)
                globalTransform = parent.globalTransform * localTransform;
            else
                globalTransform = localTransform;

            foreach (SceneObject child in children)
                child.UpdateTransform();
        }

        // Sets an object to a specific position
        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);
            UpdateTransform();
        }

        // Sets an object to a specific rotation
        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }

        // Sets an object to a specific size
        public void SetScale(float width, float height)
        {
            localTransform.SetScaled(width, height, 1);
            UpdateTransform();
        }

        // Modifys an objects position
        public void Translate(float x, float y)
        {
            localTransform.Translate(x, y);
            UpdateTransform();
        }

        // Modifys an objects rotation
        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }

        // Modifys an objects size
        public void Scale(float width, float height)
        {
            localTransform.Scale(width, height, 1);
            UpdateTransform();
        }

        // Destructor
        ~SceneObject()
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }

            foreach (SceneObject so in children)
            {
                so.parent = null;
            }
        }
    }
}
