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

            startTime = DateTime.Now;             // Record start time
            gameTimer.Start();                    // Start the timer!
        }
    }
}