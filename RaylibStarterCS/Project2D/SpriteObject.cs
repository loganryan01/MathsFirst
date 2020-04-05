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
        rl.Texture2D texture = new rl.Texture2D();
        rl.Image image = new rl.Image();

        public float Width
        {
            get { return texture.width; }
        }

        public float Height
        {
            get { return texture.height; }
        }

        public SpriteObject()
        {

        }

        public void Load(string filename)
        {
            rl.Image img = LoadImage(filename);
            texture = LoadTextureFromImage(img);
        }

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
