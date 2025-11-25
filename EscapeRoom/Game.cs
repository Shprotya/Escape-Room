using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // For Stopwatch timer

namespace EscapeRoom
{
    public class Game
    {
        // Properties
        private Player player;                    // The player object
        private List<Room> rooms;                 // List of all rooms
        private int currentRoomIndex;             // Which room we're in (0, 1, 2...)
        private DateTime startTime;               // When game started
        private Stopwatch gameTimer;              // Timer for tracking time

        // Constructor 
        public Game()
        {
            rooms = new List<Room>();             // Create empty list of rooms
            currentRoomIndex = 0;                 // Start at room 0 (first room)
            gameTimer = new Stopwatch();          // Create the timer
        }

        // Methods
        // ShowIntro - displays the welcome screen
        private void ShowIntro()
        {
            Console.Clear();
            Console.WriteLine(@"
            ╔══════════════════════════════════════════════════════════════╗
            ║                                                              ║
            ║        🏛️  ESCAPE FROM THE ANCIENT MUSEUM  🏛️                ║
            ║                                                              ║
            ║  You've been locked inside the mysterious Chronos Museum     ║
            ║  after closing time. The exhibits hold ancient secrets       ║
            ║  and puzzles. Solve them to escape before the night          ║
            ║  security system activates!                                  ║
            ║                                                              ║
            ║  ⏱️  TIME IS TICKING...                                      ║
            ║                                                              ║
            ╚══════════════════════════════════════════════════════════════╝
            ");
            Console.WriteLine("\n📜 GAME RULES:");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("• Solve puzzles to unlock doors and progress");
            Console.WriteLine("• EXAMINE rooms carefully to find hidden items");
            Console.WriteLine("• Collect items for BONUS POINTS (+50 points each)");
            Console.WriteLine("• Some items are needed to solve puzzles");
            Console.WriteLine("• Use hints if stuck (costs 50 points)");
            Console.WriteLine("• ⏱️  Your escape time will be recorded");
            Console.WriteLine("═══════════════════════════════════════════════════════");

            Console.WriteLine("\n💡 TIP: Always examine rooms before attempting puzzles!");
            Console.WriteLine("\nPress any key to begin your escape...");
            Console.ReadKey();                    // Wait for player to press a key
        }

        // Start method - begins the game
        public void Start()
        {
            ShowIntro();                          // Show welcome screen

            Console.Write("\nEnter your name, brave explorer: ");
            string name = Console.ReadLine();     // Get player's name
            player = new Player(name);            // Create player object

            // Ready screen
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine($"║  Welcome, {player.Name}!".PadRight(47) + "║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine("\nYou stand at the entrance of the museum.");
            Console.WriteLine("The doors lock behind you with a heavy THUD.");
            Console.WriteLine("\n⏱️  The timer will start when you press any key.");
            Console.WriteLine("\nPress any key to START THE TIMER...");
            Console.ReadKey();

            startTime = DateTime.Now;             // Record start time
            gameTimer.Start();                    // Start the timer!
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("1. 🔍 Examine room (find items & clues)");
            Console.WriteLine("2. 🔐 Attempt to solve puzzle");
            Console.WriteLine("3. 🎒 Check inventory");
            Console.WriteLine("4. 💡 Get hint (-50 points)");
            Console.WriteLine("5. 📊 Show status");
            if (currentRoomIndex > 0)
                Console.WriteLine("Type 'back' to return to previous room");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.Write("Your choice: ");
        }
    }
}