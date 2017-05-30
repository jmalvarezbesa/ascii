using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class PlayerControllerComponent : IControllerComponent, IObserver
    {
        public void OnNotify(GameObject object_, string event_)
        {
            if (event_ == "Exit")
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Ha ganado el juego");
                Console.ReadLine();
                Environment.Exit(1);
            }
            else if (!(event_ == "Wall"))
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Ha perdido el juego");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public void Update(GameObject ascii, movement command)
        {
            switch (command)
            {
                case movement.Right:
                    ascii.velocity[0] += 1;
                    break;
                case movement.Left:
                    ascii.velocity[0] -= 1;
                    break;
                case movement.Up:
                    ascii.velocity[1] -= 1;
                    break;
                case movement.Down:
                    ascii.velocity[1] += 1;
                    break;
                case movement.F:
                    break;
            }
        }
    }
}
