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

        static Vector3 max1 = new Vector3(640, 480, 1);
        static Vector3 min1 = new Vector3(0, 0, 1);
        static Vector3 min2 = new Vector3();
        static Vector3 max2 = new Vector3();
        static Vector3 min3 = new Vector3();
        static Vector3 max3 = new Vector3();
        static Vector3 bulletOrigin = new Vector3();
        //static Vector3 bulletStart = new Vector3();
        static Vector3 turretOrigin = new Vector3();
        static Vector3 edge1 = new Vector3(640, 0, 1);
        static Vector3 edge2 = new Vector3(640, 480, 1);
        static Vector3 edge3 = new Vector3(0, 480, 1);
        static Vector3 edge4 = new Vector3(0, 0, 1);

        AABB tank = new AABB(min2, max2);
        AABB border = new AABB(min1, max1);
        //AABB turret = new AABB(min3, max3)

        Sphere bullet = new Sphere(bulletOrigin, bulletRadius);

        Plane plane1 = new Plane(edge1, edge2);
        Plane plane2 = new Plane(edge2, edge3);
        Plane plane3 = new Plane(edge3, edge4);
        Plane plane4 = new Plane(edge4, edge1);

        Ray bulletLine = new Ray(turretOrigin, bulletOrigin);

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private static float bulletRadius;

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

            Bullet();

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
            turretOrigin.x = tankObject.GlobalTransform.m7;
            turretOrigin.y = tankObject.GlobalTransform.m8;
            turretOrigin.z = 1;

            Vector3 bulletStart = new Vector3(turretObject.GlobalTransform.m7, turretObject.GlobalTransform.m8, 1);

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

            // Player controls
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
            if (IsKeyPressed(rl.KeyboardKey.KEY_SPACE))
            {
                bulletObject.CopyTransform(turretObject.GlobalTransform);
            }

            // bullet movement
            if (bullet.Overlaps(border))
            {
                bullet.radius = bulletSprite.Height / 2.0f;
                for (int i = 0; i < 2; i++)
                {
                    Vector3 facing = new Vector3(
                          bulletObject.LocalTransform.m1,
                          bulletObject.LocalTransform.m2, 1) * deltaTime * 100;
                    bulletObject.Translate(facing.x, facing.y);
                }
            }

            // reset bullet position
            if (plane1.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(-100, -100);
            }

            if (plane2.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(-100, -100);
            }

            if (plane3.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(-100, -100);
            }

            if (plane4.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(-100, -100);
            }

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

            tankObject.Update(deltaTime);
            if (bullet.Overlaps(border))
            {
                bulletObject.Update(deltaTime);
            }

            lastTime = currentTime;
        }

        public void Draw()
        {
            if (bulletObject != null)
            {
                bulletOrigin.x = bulletObject.GlobalTransform.m7;
                bulletOrigin.y = bulletObject.GlobalTransform.m8;
                bulletOrigin.z = 1;
            }

            if (bulletSprite != null)
            {
                bulletRadius = bulletSprite.Height / 2.0f;
            }

            min2.x = tankObject.LocalTransform.m7 - 50;
            min2.y = tankObject.LocalTransform.m8 + 50;
            min2.z = 1;

            max2.x = tankObject.LocalTransform.m7 + 50;
            max2.y = tankObject.LocalTransform.m8 - 50;
            max2.z = 1;

            BeginDrawing();

            ClearBackground(rl.Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 14, rl.Color.RED);
            
            DrawRectangleLines((int)(tankObject.LocalTransform.m7 - 52), (int)(tankObject.LocalTransform.m8 - 50), 100, 100, rl.Color.BLACK);
            //DrawLine((int)turretOrigin.x, (int)turretOrigin.y, (int)bulletOrigin.x, (int)bulletOrigin.y, rl.Color.RED);
            //DrawRectangleLines(0, 0, GetScreenWidth(), GetScreenHeight(), rl.Color.BLUE);
            //DrawRectangleLines(320, 240, 1, 1, rl.Color.BLACK); // Center of the window.
            //DrawRectangleLines((int)(turretObject.GlobalTransform.m7), (int)turretObject.GlobalTransform.m8, 100, 1, rl.Color.BLACK); // Center of the window.
            //DrawRectangleLines((int)bulletOrigin.x, (int)bulletOrigin.y, 10, 10, rl.Color.RED);
            //DrawCircleLines((int)bulletOrigin.x, (int)bulletOrigin.y, bulletSprite.Height / 2.0f, rl.Color.RED); // Circle around of the bullet
            //DrawRectangle((int)bulletOrigin.x, (int)bulletOrigin.y, 1, 1, rl.Color.RED);
            
            DrawText((turretObject.GlobalTransform.m7).ToString(), 10, 30, 14, rl.Color.RED);
            //DrawText((turretSprite.GlobalTransform.m8).ToString(), 10, 50, 14, rl.Color.RED);
            DrawText((bulletObject.GlobalTransform.m7).ToString(), 10, 70, 14, rl.Color.RED);
            //DrawText((bulletSprite.GlobalTransform.m8).ToString(), 10, 90, 14, rl.Color.RED);

            tankObject.Draw();
            if (bulletObject != null)
            {
                bulletObject.Draw();
            }

            EndDrawing();
        }

        public void Bullet()
        {
            bulletObject = new SceneObject();
            bulletSprite = new SpriteObject();

            bulletSprite.Load(@"D:\\Windows\\PNG\\Bullets\\bulletBlue.png");
            bulletSprite.SetRotate(90 * (float)(Math.PI / 180.0f));
            // sets an offset for the bullet, so it rotates around the centre
            bulletSprite.SetPosition(bulletSprite.Height / 2.0f, -bulletSprite.Width / 2.0f);

            bulletObject.AddChild(bulletSprite);
            bulletObject.SetPosition(-10, -10);
        }
    }
}
