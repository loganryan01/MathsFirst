using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using rl = Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();

        static Vector3 max1 = new Vector3(640, 480, 1);
        static Vector3 min1 = new Vector3(0, 0, 1);
        static Vector3 min2 = new Vector3();
        static Vector3 max2 = new Vector3();

        AABB tank = new AABB(min2, max2);
        AABB box = new AABB(min1, max1);

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        public Game()
        {
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            tankSprite.Load(@"D:\\Windows\\PNG\\Tanks\\tankBlue_outline.png");
            // sprite is facing the wrong way... fix that here
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);
            turretSprite.Load(@"D:\\Windows\\PNG\\Tanks\\barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            // set the turret offset from the tank base
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            // set up the scene object hierarchy - parent the turret to the base,
            // then the base to the tank sceneObject
            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);

            // having an empty object for the tank parent means we can set the
            // position/rotation of the tank without
            // affecting the offset of the base sprite
            tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
            //42, 42

            //min2.x = tankSprite.LocalTransform.m7 - 42;
            //min2.y = tankSprite.LocalTransform.m8 - 42;
            //max2.x = tankSprite.LocalTransform.m7 + 42;
            //max2.y = tankSprite.LocalTransform.m8 + 42;
        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            if (IsKeyDown(rl.KeyboardKey.KEY_A))
            {
                tankObject.Rotate(-deltaTime);
            }
            if (IsKeyDown(rl.KeyboardKey.KEY_D))
            {
                tankObject.Rotate(deltaTime);
            }
            if (IsKeyDown(rl.KeyboardKey.KEY_W))
            {
                Vector3 facing = new Vector3(
                           tankObject.LocalTransform.m1,
                           tankObject.LocalTransform.m2, 1) * deltaTime * 100;
                tankObject.Translate(facing.x, facing.y);
            }
            if (IsKeyDown(rl.KeyboardKey.KEY_S))
            {
                Vector3 facing = new Vector3(
                           tankObject.LocalTransform.m1,
                           tankObject.LocalTransform.m2, 1) * deltaTime * -100;
                tankObject.Translate(facing.x, facing.y);
            }
            if (IsKeyDown(rl.KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime);
            }
            if (IsKeyDown(rl.KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime);
            }
            tankObject.Update(deltaTime);

            // Make sure tank does not go out of bounds
            if (tankObject.LocalTransform.m7 >= 594)
            {
                tankObject.SetPosition(594, tankObject.LocalTransform.m8);
            }
            if (tankObject.LocalTransform.m7 <= 50)
            {
                tankObject.SetPosition(50, tankObject.LocalTransform.m8);
            }
            if (tankObject.LocalTransform.m8 >= 430)
            {
                tankObject.SetPosition(tankObject.LocalTransform.m7, 430);
            }
            if (tankObject.LocalTransform.m8 <= 50)
            {
                tankObject.SetPosition(tankObject.LocalTransform.m7, 50);
            }

            lastTime = currentTime;
        }

        public void Draw()
        {
            min2.x = tankObject.LocalTransform.m7 - 52;
            min2.y = tankObject.LocalTransform.m8 - 50;
            min2.z = 1;
            max2.x = tankObject.LocalTransform.m7 + 52;
            max2.y = tankObject.LocalTransform.m8 + 50;
            max2.z = 1;

            BeginDrawing();

            ClearBackground(rl.Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 14, rl.Color.RED);
            
            DrawRectangleLines((int)(tankObject.LocalTransform.m7 - 52), (int)(tankObject.LocalTransform.m8 - 50), 100, 100, rl.Color.BLACK);
            DrawRectangleLines(0, 0, GetScreenWidth(), GetScreenHeight(), rl.Color.BLUE);

            //if (tank.Overlaps(box))
            //{
            //    DrawText("Tank overlaps box", 10, 70, 14, rl.Color.RED);
            //}
            //m1 = 1, when the front of the tank is facing left
            //m2 = 1, when the front of the tank is facing down
            //m3 is constantly 0.
            //m4 = 1, when the front of the tank is facing up.
            //m5 = 1, when the front of the tank is facing right
            //m6 is contantly 0.
            //m7 is x position of the tank.
            //m8 is y position of the tank.
            //m9 is constantly 1.
            // turretObject.LocalTransform.m5, turretObject.LocalTransform.m2
            //The width of the tank is 83.
            //The height of the tank is 78.

            tankObject.Draw();

            EndDrawing();
        }

    }
}
