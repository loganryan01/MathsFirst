using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rl = Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    public class SpriteObject : SceneObject
    {
        // Member variables
        rl.Texture2D texture = new rl.Texture2D();
        rl.Image image = new rl.Image();

        // Gets width of sprite
        public float Width
        {
            get { return texture.width; }
        }

        // Gets height of sprite
        public float Height
        {
            get { return texture.height; }
        }

        // Constructor
        public SpriteObject()
        {

        }

        // Loads the texture
        public void Load(string filename)
        {
            rl.Image img = LoadImage(filename);
            texture = LoadTextureFromImage(img);
        }

        // Custom draw method
        public override void OnDraw()
        {
            float rotation = (float)Math.Atan2(
                                         globalTransform.m2, globalTransform.m1);
            Raylib.Raylib.DrawTextureEx(
                texture,
                new rl.Vector2(globalTransform.m7, globalTransform.m8),
                rotation * (float)(180.0f / Math.PI),
                1, rl.Color.WHITE);
        }
    }
}
