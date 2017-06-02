using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class GameObjectFactory
    {
        private Level lvl;
        public GameObjectFactory(Level lvl)
        {
            this.lvl = lvl;
        }


        public GameObject CreateObject(int x, int y, MapSymbol symbol)
        {
            int deltaTime = 33;
            if (symbol == MapSymbol.Player)
            {
                IObserver AsciiControllerComponent = new AsciiControllerComponent();
                IObserver AsciiAudioComponent = new GenericAudioComponent();
                AsciiPhysicsComponent AsciiPhysicsComponent = new AsciiPhysicsComponent(1000);
                AsciiPhysicsComponent.AddObserver(AsciiControllerComponent);
                AsciiPhysicsComponent.AddObserver(AsciiAudioComponent);
                GameObject ascii = new GameObject(new AsciiControllerComponent(), AsciiPhysicsComponent, new GenericGraphicsComponent('@'), new NullAudioComponent());
                ascii.Start(new int[] { x, y }, "User");
                return ascii;
            }
            else if (symbol == MapSymbol.Gazer)
            {
                int v = 1;
                GameObject gaze = new GameObject(new GazeControllerComponent(v), new GenericPhysicsComponent(deltaTime), new GenericGraphicsComponent('G'), new NullAudioComponent());
                gaze.Start(new int[] { x, y }, "Gaze");
                return gaze;
            }
            else if (symbol == MapSymbol.Skeleton)
            {
                GameObject skeleton = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('S'), new NullAudioComponent());
                skeleton.Start(new int[] { x, y }, "Skeleton");
                return skeleton;
            }
            else if (symbol == MapSymbol.Arrow)
            {
                GameObject arrow = new GameObject(new ArrowControllerComponent(1, lvl.Width, lvl.Height), new GenericPhysicsComponent(deltaTime/2), new GenericGraphicsComponent('~'), new NullAudioComponent());
                arrow.Start(new int[] { x, y }, "Arrow");
                return arrow;
            }
            else if (symbol == MapSymbol.Exit)
            {
                GameObject exit = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('X'), new NullAudioComponent());
                exit.Start(new int[] { x, y }, "Exit");
                return exit;
            }
            else if (symbol == MapSymbol.VerticalWall)
            {
                GameObject wall = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('|'), new NullAudioComponent());
                wall.Start(new int[] { x, y }, "Wall");
                return wall;
            }
            else if (symbol == MapSymbol.HorizontalWall)
            {
                GameObject wall = new GameObject(new NullControllerComponent(), new NullPhysicsComponent(), new GenericGraphicsComponent('-'), new NullAudioComponent());
                wall.Start(new int[] { x, y }, "Wall");
                return wall;
            }
            else if (symbol == MapSymbol.Rat)
            {
                GameObject rat = new GameObject(new RatControllerComponent(), new AsciiPhysicsComponent(deltaTime/2), new GenericGraphicsComponent('R'), new NullAudioComponent());
                rat.Start(new int[] { x, y }, "Rat");
                return rat;
            }
            else if (symbol == MapSymbol.Zombie)
            {
                GameObject rat = new GameObject(new ZombieControllerComponent(lvl), new GenericPhysicsComponent(deltaTime), new GenericGraphicsComponent('Z'), new NullAudioComponent());
                rat.Start(new int[] { x, y }, "Zombie");
                return rat;
            }
            else if (symbol == MapSymbol.Space || symbol == MapSymbol.HorizontalWall)
            {
                return null;
            }
            return null;
        }
    }
}
