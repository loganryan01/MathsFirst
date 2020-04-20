﻿using System;
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
        
        private int score = 0;
        private int highscore = 0;
        private int smokeFrames;
        private bool gameOver = false;

        private float deltaTime = 0.005f;

        static Vector3 targetOrigin = new Vector3(GetRandomValue(30, 580), GetRandomValue(30, 420), 1);
        private static float targetRadius = 30;
        Sphere target = new Sphere(targetOrigin, targetRadius);

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
            bulletSprite.SetPosition(50, -6);

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
            treeObject.SetPosition(GetRandomValue(54, 216), GetRandomValue(49, 431));
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

            //Planes for tank to collide
            Vector3 edge5 = new Vector3(640, 0, 1);
            Vector3 edge6 = new Vector3(640, 480, 1);
            Vector3 edge7 = new Vector3(0, 480, 1);
            Vector3 edge8 = new Vector3(0, 0, 1);

            Plane plane5 = new Plane(edge5, edge6);
            Plane plane6 = new Plane(edge6, edge7);
            Plane plane7 = new Plane(edge7, edge8);
            Plane plane8 = new Plane(edge8, edge5);

            //Planes for bullet to collide
            Vector3 edge1 = new Vector3(610, 30, 1);
            Vector3 edge2 = new Vector3(610, 450, 1);
            Vector3 edge3 = new Vector3(30, 450, 1);
            Vector3 edge4 = new Vector3(30, 30, 1);

            Plane plane1 = new Plane(edge1, edge2); //Right side
            Plane plane2 = new Plane(edge2, edge3); //Bottom
            Plane plane3 = new Plane(edge3, edge4); //Left Side
            Plane plane4 = new Plane(edge4, edge1); //Top

            //Circle for tank
            Vector3 tankOrigin = new Vector3(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8, 1);
            float tankRadius = tankSprite.Height / 2.0f;
            Sphere tank = new Sphere(tankOrigin, tankRadius);


            //Circle for bullet 
            Vector3 bulletOrigin = new Vector3(bulletObject.GlobalTransform.m7, bulletObject.GlobalTransform.m8, 1);
            float bulletRadius = bulletSprite.Height / 2.0f;
            Sphere bullet = new Sphere(bulletOrigin, bulletRadius);

            //Circle for tree
            Vector3 treeOrigin = new Vector3(treeObject.GlobalTransform.m7 + 4, treeObject.GlobalTransform.m8 + 5, 1);
            float treeRadius = 60;
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
            if (IsKeyDown(rl.KeyboardKey.KEY_SPACE))
            {
                if (!bullet.Overlaps(border))
                {
                    bulletObject.CopyTransform(turretObject.GlobalTransform);
                }
            }
            if (IsMouseButtonPressed(rl.MouseButton.MOUSE_LEFT_BUTTON) && gameOver)
            {
                gameOver = false;
                highscore = score;
                score = 0;
                tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
                bulletObject.SetPosition(-100, -100);
                smokeObject.SetPosition(-100, -100);
                treeObject.SetPosition(GetRandomValue(54, 216), GetRandomValue(49, 431));
                targetOrigin.x = GetRandomValue(30, 580);
                targetOrigin.y = GetRandomValue(30, 450);
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

            // Bullet Destruction
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
            if (bullet.Overlaps(tree))
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(bulletObject.GlobalTransform.m7, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(bulletObject.GlobalTransform.m7, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                bulletObject.SetPosition(-100, -100);
            }
            if (bullet.Overlaps(target))
            {
                turretObject.SetPosition(0, 0);
                bulletObject.SetPosition(bulletObject.GlobalTransform.m7, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(bulletObject.GlobalTransform.m7, bulletObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                bulletObject.SetPosition(-100, -100);
                targetOrigin.x = GetRandomValue(30, 580);
                targetOrigin.y = GetRandomValue(30, 450);
                score++;
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

            // Destroy tank f it goes out of bounds
            if (plane5.TestSide(tank) == Plane.ePlaneResult.INTERSECTS)
            {
                gameOver = true;
                smokeObject.SetPosition(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                tankObject.SetPosition(-100, -100);
            }
            if (plane6.TestSide(tank) == Plane.ePlaneResult.INTERSECTS)
            {
                gameOver = true;
                smokeObject.SetPosition(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                tankObject.SetPosition(-100, -100);
            }
            if (plane7.TestSide(tank) == Plane.ePlaneResult.INTERSECTS)
            {
                gameOver = true;
                smokeObject.SetPosition(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                tankObject.SetPosition(-100, -100);
            }
            if (plane8.TestSide(tank) == Plane.ePlaneResult.INTERSECTS)
            {
                gameOver = true;
                smokeObject.SetPosition(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                tankObject.SetPosition(-100, -100);
            }

            // Tank Destruction
            if (tree.Overlaps(tank))
            {
                gameOver = true;
                smokeObject.SetPosition(tankObject.GlobalTransform.m7, tankObject.GlobalTransform.m8);
                smokeObject.SetPosition(smokeObject.GlobalTransform.m7, smokeObject.GlobalTransform.m8);
                tankObject.SetPosition(-100, -100);
            }

            // Target positioning
            if (target.Overlaps(tree))
            {
                targetOrigin.x = GetRandomValue(30, 580);
                targetOrigin.y = GetRandomValue(30, 450);
            }

            // Tree positioning
            if (treeObject.GlobalTransform.m7 == tankObject.GlobalTransform.m7 ||
                treeObject.GlobalTransform.m8 == tankObject.GlobalTransform.m8)
            {
                treeObject.SetPosition(GetRandomValue(54, 586), GetRandomValue(49, 431));
            }

            if (gameOver)
            {
                treeObject.SetPosition(-200, 0);
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
            DrawText("Score: " + score, 10, 30, 14, rl.Color.RED);
            DrawText("Highscore: " + highscore, 10, 50, 14, rl.Color.RED);

            DrawCircle((int)targetOrigin.x, (int)targetOrigin.y, 30, rl.Color.RED);
            //DrawCircle((int)bulletObject.GlobalTransform.m7, (int)bulletObject.GlobalTransform.m8, bulletSprite.Height / 2.0f, rl.Color.BLACK);
            DrawText(bulletObject.GlobalTransform.m7.ToString(), 10, 70, 14, rl.Color.RED);
            DrawText(bulletObject.GlobalTransform.m8.ToString(), 10, 90, 14, rl.Color.RED);

            if (gameOver && score < highscore || gameOver && score == highscore)
            {
                ClearBackground(rl.Color.RED);
                DrawText("Game Over", 240, 200, 30, rl.Color.BLACK);
                DrawText("You Crashed", 240, 230, 15, rl.Color.BLACK);
                DrawText("Your score was " + score, 240, 245, 15, rl.Color.BLACK);
                DrawText("Left click to restart", 240, 260, 15, rl.Color.BLACK);
            }
            if (gameOver && score > highscore)
            {
                ClearBackground(rl.Color.RED);
                DrawText("Game Over", 240, 200, 30, rl.Color.BLACK);
                DrawText("You Crashed", 240, 230, 15, rl.Color.BLACK);
                DrawText("Your score was " + score, 240, 245, 15, rl.Color.BLACK);
                DrawText("Congratulations! New highscore", 240, 260, 15, rl.Color.BLACK);
                DrawText("Left click to restart", 240, 275, 15, rl.Color.BLACK);
            }

            tankObject.Draw();
            bulletObject.Draw();
            smokeObject.Draw();
            treeObject.Draw();

            EndDrawing();
        }
    }
}
