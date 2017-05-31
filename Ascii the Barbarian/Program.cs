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
        static int FPMS = 33;    //frames per milliseconds

        static movement ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo KeyPress = Console.ReadKey();
                if (KeyPress.Equals(movement.Up)) {return movement.Up; }
                if (KeyPress.Equals(movement.Down)) { return movement.Down; }
                if (KeyPress.Equals(movement.Left)) { return movement.Left; }
                if (KeyPress.Equals(movement.Right)) { return movement.Right; }
            }
            return movement.F;
        }

        static void Render(GameObject ascii)
        {
            Console.Clear();
            Console.SetCursorPosition(ascii.GetPosition()[0], ascii.GetPosition()[1]);
            Console.Write(ascii.GetSymbol());
            foreach (GameObject gameObject in gameObjects)
            {
                Console.SetCursorPosition(gameObject.GetPosition()[0], gameObject.GetPosition()[1]);
                Console.Write(gameObject.GetSymbol());
            } 
        }
        
        static void Main(string[] args)
        {
            Level lvl = new Level();
            GameObjectFactory object_factory = new GameObjectFactory();
            GameObject ascii = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('X'), new NullAudioComponent());

            lvl.LoadFromFile("test.txt");

            for (int x = 0; x < lvl.Width; x++)
            {
                for (int y = 0; y < lvl.Height; y++)
                {
                    MapSymbol current_symbol = lvl.GetMapSymbol(x, y);
                    if (current_symbol == MapSymbol.Player)
                    {
                        ascii = object_factory.CreateObject(x, y, current_symbol);
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

                ascii.Update(gameObjects, '@', positions);

                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(gameObjects, '@', positions);
                }
                Render(ascii);
                time.Stop(); 
                int elapsed = Convert.ToInt32(time.ElapsedMilliseconds);
                if (elapsed < FPMS) Thread.Sleep(FPMS - elapsed);
            }
        }
    }
}

