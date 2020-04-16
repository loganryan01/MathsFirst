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
        SceneObject smokeObject = new SceneObject();
        SceneObject treeObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();
        SpriteObject bulletSprite = new SpriteObject();
        SpriteObject smokeSprite = new SpriteObject();
        SpriteObject treeSprite = new SpriteObject();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private int smokeFrames;

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
            bulletSprite.SetRotate(90 * (float)(Math.PI / 180.0f));
            // sets an offset for the bullet, so it rotates around the centre
            bulletSprite.SetPosition(bulletSprite.Height / 2.0f, -bulletSprite.Width / 2.0f);

            smokeSprite.Load(@"D:\\Windows\\PNG\\Smoke\\smokeOrange0.png");
            smokeSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            smokeSprite.SetPosition(-smokeSprite.Width / 2.0f, smokeSprite.Height / 2.0f);

            treeSprite.Load(@"D:\\Windows\\PNG\\Environment\\treeLarge.png");
            treeSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            treeSprite.SetPosition(-treeSprite.Width / 2.0f, treeSprite.Height / 2.0f);

            treeObject.AddChild(treeSprite);
            smokeObject.AddChild(smokeSprite);
            bulletObject.AddChild(bulletSprite);
            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);

            tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
            bulletObject.SetPosition(-100, -100);
            smokeObject.SetPosition(-100, -100);
            treeObject.SetPosition(GetRandomValue(54, 584), GetRandomValue(49, 431));

            if (treeObject.GlobalTransform.m7 == tankObject.GlobalTransform.m7 ||
                treeObject.GlobalTransform.m8 == tankObject.GlobalTransform.m8)
            {
                treeObject.SetPosition(GetRandomValue(54, 584), GetRandomValue(49, 431));
            }
        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            //Box for window
            Vector3 borderMin = new Vector3(0, 0, 1);
            Vector3 borderMax = new Vector3(640, 480, 1);
            AABB border = new AABB(borderMin, borderMax);

            //Planes for window
            Vector3 edge1 = new Vector3(640, 0, 1);
            Vector3 edge2 = new Vector3(640, 480, 1);
            Vector3 edge3 = new Vector3(0, 480, 1);
            Vector3 edge4 = new Vector3(0, 0, 1);

            Plane plane1 = new Plane(edge1, edge2); //Right side
            Plane plane2 = new Plane(edge2, edge3); //Bottom
            Plane plane3 = new Plane(edge3, edge4); //Left Side
            Plane plane4 = new Plane(edge4, edge1); //Top

            //Box for tank
            Vector3 tankMin = new Vector3(tankObject.LocalTransform.m7 - 50, tankObject.LocalTransform.m8 + 50, 1);
            Vector3 tankMax = new Vector3(tankObject.LocalTransform.m7 + 50, tankObject.LocalTransform.m8 - 50, 1);
            AABB tank = new AABB(tankMin, tankMax);

            //Circle for bullet 
            Vector3 bulletOrigin = new Vector3(bulletObject.GlobalTransform.m7, bulletObject.GlobalTransform.m8, 1);
            float bulletRadius = bulletSprite.Height / 2.0f;
            Sphere bullet = new Sphere(bulletOrigin, bulletRadius);

            //Circle for tree
            Vector3 treeOrigin = new Vector3(treeObject.GlobalTransform.m7 + 5, treeObject.GlobalTransform.m8 + 4, 1);
            float treeRadius = 70;
            Sphere tree = new Sphere(treeOrigin, treeRadius);

            //Circle for smoke
            Vector3 smokeOrigin = new Vector3(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8, 1);
            float smokeRadius = 70;
            Sphere smoke = new Sphere(smokeOrigin, smokeRadius);

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
                if (!bullet.Overlaps(border))
                {
                    bulletObject.CopyTransform(turretObject.GlobalTransform);
                }
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
                // Return turretObject to original position
                turretObject.SetPosition(0, 0);

                // Set smoke to y-position of where the bullet collided with the plane
                bulletObject.SetPosition(630, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(597, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(597, smokeObject.GlobalTransform.m8);

                // Move bullet outside the window
                bulletObject.SetPosition(-100, -100);
            }
            if (plane2.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(bulletObject.GlobalTransform.m7, 470);
                smokeObject.SetPosition(bulletObject.GlobalTransform.m7, 437);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, 437);
                bulletObject.SetPosition(-100, -100);
            }
            if (plane3.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(10, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(43, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(43, smokeObject.GlobalTransform.m8);
                bulletObject.SetPosition(-100, -100);
            }
            if (plane4.TestSide(bullet) == Plane.ePlaneResult.INTERSECTS)
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(bulletObject.GlobalTransform.m7, 10);
                smokeObject.SetPosition(bulletObject.GlobalTransform.m7, 43);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, 43);
                bulletObject.SetPosition(-100, -100);
            }

            // Move smoke outside the window after a little bit
            if (smoke.Overlaps(border))
            {
                smokeFrames++;

                if (smokeFrames == 10)
                {
                    smokeObject.SetPosition(-100, -100);
                    smokeFrames = 0;
                }
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

            if (tree.Overlaps(tank))
            {
                smokeObject.SetPosition(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                tankObject.SetPosition(320, 240);
            }

                tankObject.Update(deltaTime);
            if (bullet.Overlaps(border))
            {
                bulletObject.Update(deltaTime);
            }
            smokeObject.Update(deltaTime);
            treeObject.Update(deltaTime);

            lastTime = currentTime;
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(rl.Color.GREEN);
            DrawText(fps.ToString(), 10, 10, 14, rl.Color.RED);

            DrawCircle((int)(smokeObject.GlobalTransform.m7), (int)(smokeObject.GlobalTransform.m8), 70, rl.Color.BLACK);
            

            tankObject.Draw();
            bulletObject.Draw();
            smokeObject.Draw();
            treeObject.Draw();

            EndDrawing();
        }
    }
}
