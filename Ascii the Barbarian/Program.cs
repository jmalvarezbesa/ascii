using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    enum movement
    {
        Up = ConsoleKey.UpArrow,
        Down = ConsoleKey.DownArrow,
        Left = ConsoleKey.LeftArrow,
        Right = ConsoleKey.RightArrow,
        F = 0
    }
    class Program
    {
        static public List<GameObject> gameObjects = new List<GameObject>();
        static public DoubleBuffer.DoubleGraphicsBuffer doubleBuffer;
        static int FPMS = 33;    //frames per milliseconds

        static movement ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo KeyPress = Console.ReadKey();
                if ((int)KeyPress.Key == (int)movement.Up) {return movement.Up; }
                if ((int)KeyPress.Key == (int)movement.Down) { return movement.Down; }
                if ((int)KeyPress.Key == (int)movement.Left) { return movement.Left; }
                if ((int)KeyPress.Key == (int)movement.Right) { return movement.Right; }
            }
            return movement.F;
        }

        static void Render(GameObject ascii, DoubleBuffer.DoubleGraphicsBuffer doubleBuffer)
        {
            doubleBuffer.Render();
        }
        
        static void Main(string[] args)
        {
            Level lvl = new Level();
            lvl.LoadFromFile("rat_level.txt");

            GameObjectFactory object_factory = new GameObjectFactory(lvl);
            GameObject ascii = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('X'), new NullAudioComponent());
            doubleBuffer = new DoubleBuffer.DoubleGraphicsBuffer(lvl.Width, lvl.Height, ConsoleColor.White, ConsoleColor.DarkGreen);

            for (int x = 0; x < lvl.Width; x++)
            {
                for (int y = 0; y < lvl.Height; y++)
                {
                    MapSymbol current_symbol = lvl.GetMapSymbol(x, y);
                    if (current_symbol == MapSymbol.Player)
                    {
                        ascii = object_factory.CreateObject(x, y, current_symbol);
                        gameObjects.Add(ascii);
                    } else {
                        GameObject current_object = object_factory.CreateObject(x, y, current_symbol);
                        if (current_object == null)
                        {
                            continue;
                        }
                        else
                        {
                            gameObjects.Add(current_object);
                        }
                    }
                }
            }


            Console.CursorVisible = false;
            Console.SetCursorPosition(1,1);

            while (true) {
                System.Diagnostics.Stopwatch time = System.Diagnostics.Stopwatch.StartNew();
                time.Start();
                movement positions = ProcessInput();
                doubleBuffer.Clear();

                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(gameObjects, doubleBuffer, positions);
                }
                Render(ascii, doubleBuffer);
                time.Stop(); 
                int elapsed = Convert.ToInt32(time.ElapsedMilliseconds);
                if (elapsed < FPMS) Thread.Sleep(FPMS - elapsed);
            }
        }
    }
}

