using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and start the game
            Game game = new Game();
            game.Start();

            Console.WriteLine("\n\nThank you for playing! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
