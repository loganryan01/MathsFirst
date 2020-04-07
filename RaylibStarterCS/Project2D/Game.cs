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

        AABB tank = new AABB();

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

            tank.SetToTransformedBox(tank, tankObject.LocalTransform);
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

            

            lastTime = currentTime;
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(rl.Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 14, rl.Color.RED);
            

            tankObject.Draw();

            EndDrawing();
        }

    }
}
