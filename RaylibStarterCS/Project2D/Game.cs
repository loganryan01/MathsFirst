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
        SceneObject bulletObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();
        SpriteObject bulletSprite = new SpriteObject();

        //static Vector3 max1 = new Vector3(640, 480, 1);
        //static Vector3 min1 = new Vector3(0, 0, 1);
        //static Vector3 min2 = new Vector3();
        //static Vector3 max2 = new Vector3();
        Vector3 tankPosition = new Vector3(GetScreenWidth() / 2, GetScreenHeight() / 2, 1);

        //AABB tank = new AABB(min2, max2);
        //AABB border = new AABB(min1, max1);

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
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f)); // converts degrees to radians
            // sets an offset for the base, so it rotates around the centre
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);

            turretSprite.Load(@"D:\\Windows\\PNG\\Tanks\\barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            // set the turret offset from the tank base
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            bulletSprite.Load(@"D:\\Windows\\PNG\\Bullets\\bulletBlue.png");
            bulletSprite.SetRotate(-270 * (float)(Math.PI / 180.0f));
            bulletSprite.SetPosition(bulletSprite.Width / 2.0f, bulletSprite.Height / 2.0f);

            bulletObject.AddChild(bulletSprite);
            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);

            tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
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
            if (IsKeyDown(rl.KeyboardKey.KEY_SPACE))
            {
                bulletObject.CopyTransform(turretObject.GlobalTransform);
                
                //bulletObject.SetPosition(tankObject.LocalTransform.m7 + 15f, tankObject.LocalTransform.m8 - 7.5f);
                //bulletObject.Rotate(deltaTime);
            }
            tankObject.Update(deltaTime);
            bulletObject.Update(deltaTime);

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
            BeginDrawing();

            ClearBackground(rl.Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 14, rl.Color.RED);
            
            DrawRectangleLines((int)(tankObject.LocalTransform.m7 - 52), (int)(tankObject.LocalTransform.m8 - 50), 100, 100, rl.Color.BLACK);
            DrawRectangleLines(0, 0, GetScreenWidth(), GetScreenHeight(), rl.Color.BLUE);
            //DrawCircleLines((int)(bulletObject.LocalTransform.m7), (int)(bulletObject.LocalTransform.m8), 15, rl.Color.BLACK);

            //DrawText((bulletObject.LocalTransform.m7).ToString(), 10, 30, 14, rl.Color.RED);
            //DrawText((turretObject.LocalTransform.m2).ToString(), 10, 50, 14, rl.Color.RED);
            //DrawText((direction1.y).ToString(), 10, 70, 14, rl.Color.RED);
            //DrawText((turretObject.LocalTransform.m5).ToString(), 10, 90, 14, rl.Color.RED);
            //m1 = 1, when the front of the tank is facing right
            //m2 = 1, when the front of the tank is facing down
            //m3 is constantly 0.
            //m4 = 1, when the front of the tank is facing up.
            //m5 = 1, when the front of the tank is facing right
            //m6 is contantly 0.
            //m7 is x position of the tank.
            //m8 is y position of the tank.
            //m9 is constantly 1.

            tankObject.Draw();
            bulletObject.Draw();

            EndDrawing();
        }

    }
}
