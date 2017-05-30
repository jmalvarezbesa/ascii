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
        static void addWallLine(int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                GameObject wall = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('#'), new NullAudioComponent());
                if (horizontal)
                {
                    wall.Start(new int[] { x + i, y }, "Wall");
                    gameObjects.Add(wall);
                }
                else
                {
                    wall.Start(new int[] { x, y + i }, "Wall");
                    gameObjects.Add(wall);
                }

                
            }
        }

        static void Generateboard()
        {
            addWallLine(40, 10, 60, true);
            addWallLine(40, 20, 60, true);
            addWallLine(40, 10, 10, false);
            addWallLine(100, 10, 11, false);
        }

        static void Main(string[] args)
        {
            IObserver AsciiControllerComponent = new PlayerControllerComponent();
            IObserver AsciiAudioComponent = new GenericAudioComponent();
            AsciiPhysicsComponent AsciiPhysicsComponent = new AsciiPhysicsComponent();
            AsciiPhysicsComponent.AddObserver(AsciiControllerComponent);
            AsciiPhysicsComponent.AddObserver(AsciiAudioComponent);

            GameObject ascii = new GameObject(new PlayerControllerComponent(), AsciiPhysicsComponent, new GenericGraphicsComponent('@'), new NullAudioComponent());
            GameObject gaze = new GameObject(new GazeControllerComponent(), new GenericPhysicsComponent(), new GenericGraphicsComponent('G'), new NullAudioComponent());
            GameObject skeleton = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('S'), new NullAudioComponent());
            GameObject arrow = new GameObject(new ArrowControllerComponent(1), new GenericPhysicsComponent(), new GenericGraphicsComponent('~'), new NullAudioComponent());
            GameObject exit = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('X'), new NullAudioComponent());

            ascii.Start(new int[] { 1, 1 }, "User");
            gaze.Start(new int[] { 10, 0 }, "Gaze");
            skeleton.Start(new int[] { 20, 20 }, "Skeleton");
            arrow.Start(new int[] { 20, 20 }, "Arrow");
            exit.Start(new int[] { 18, 18 }, "Exit");

            gameObjects.Add(gaze);
            gameObjects.Add(skeleton);
            gameObjects.Add(arrow);
            gameObjects.Add(exit);
            Generateboard();

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

