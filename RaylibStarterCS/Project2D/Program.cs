﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rl = Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            SetTargetFPS(60);
            InitWindow(640, 480, "Tanks for Everything!");

            game.Init();

            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
            }

            game.Shutdown();
            CloseWindow();
        }
    }
}
