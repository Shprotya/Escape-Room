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

            SetupRooms();
            GameLoop();
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("1. 👓 Examine Puzzle ");
            Console.WriteLine("2. 🔍 Examine room (find items & clues)");
            Console.WriteLine("3. 🔐 Attempt to solve puzzle");
            Console.WriteLine("4. 🎒 Check inventory");
            Console.WriteLine("5. 💡 Get hint (-50 points)");
            Console.WriteLine("6. 📊 Show status");
            if (currentRoomIndex > 0)
                Console.WriteLine("Type 'back' to return to previous room");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.Write("Your choice: ");
        }

        private void SetupRooms()
        {
            // Room 1: ASTROLOGY HALL - With random planet puzzle!
            var room1Puzzle = new PlanetDiscoveryPuzzle();
            var room1 = new Room(
                "Astrology Hall",
                "You find yourself in the Astrology Hall. Ancient star charts cover the walls.\n" +
                "A magnificent brass telescope stands in the center, pointed toward a skylight.\n" +
                "The only exit is blocked by an ornate door with a numerical keypad.",
                room1Puzzle
            );
            room1.AddItem("Star Chart");
            room1.AddItem("Museum Guide");
            rooms.Add(room1);
        }

        private void GameLoop()
        {
            while (currentRoomIndex < rooms.Count)
            {   
                Room currentRoom = rooms[currentRoomIndex];
                currentRoom.Enter();
                ShowCurrentTimer(); // Show timer when entering room

                while (!currentRoom.IsCompleted)
                {
                    ShowMenu();
                    string choice = Console.ReadLine()?.ToLower() ?? "";

                    switch (choice)
                    {
                        case "1":
                            ExaminePuzzle(currentRoom);
                            break;
                        case "2":
                            ExamineRoom(currentRoom);
                            break;
                        case "3":
                            AttemptPuzzle(currentRoom);
                            break;
                        case "4":
                            player.ShowInventory();
                            break;
                        case "5":
                            ShowHint(currentRoom);
                            break;
                        case "6":
                            ShowStatus();
                            break;
                        default:
                            Console.WriteLine("\n✗ Invalid choice!");
                            break;
                    }

                    if (currentRoom.Puzzle.IsSolved && !currentRoom.IsCompleted)
                    {
                        currentRoom.Complete();
                        player.Score += 200; // Bonus for completing room
                        Console.WriteLine("\n🎉 Room completed! You can proceed to the next area...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }

                currentRoomIndex++;
            }
            gameTimer.Stop(); // STOP THE TIMER when game ends

        } //End of Game Loop method

        private void ShowCurrentTimer()
        {
            TimeSpan elapsed = gameTimer.Elapsed;
            Console.WriteLine($"\n⏱️  Time Elapsed: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
        }

        private void ExaminePuzzle(Room room)
        {
            Console.WriteLine($"\n📋 PUZZLE:");
            Console.WriteLine(room.Puzzle.Description);
        }

        private void ExamineRoom(Room room)
        {
            // Special examination for Astrology Hall
            if (room.Name == "Astrology Hall" && room.Puzzle is PlanetDiscoveryPuzzle planetPuzzle)
            {
                Console.WriteLine("\nYou examine the room carefully...");
                Console.WriteLine("\n1. Look through the telescope");
                Console.WriteLine("2. Study the planet discovery chart on the wall");
                Console.WriteLine("3. Return to main menu");
                Console.Write("\nWhat do you want to examine? ");

                string examineChoice = Console.ReadLine();

                switch (examineChoice)
                {
                    case "1":
                        planetPuzzle.LookThroughTelescope();
                        break;
                    case "2":
                        planetPuzzle.ShowDiscoveryChart();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nYou examine the room carefully but find nothing new.");
            }
        }

        private void AttemptPuzzle(Room room)
        {
            if (room.Puzzle.IsSolved)
            {
                Console.WriteLine("\n✓ This puzzle is already solved!");
                return;
            }

            Console.WriteLine($"\n{room.Puzzle.Description}");
            Console.Write("\nYour answer: ");
            string answer = Console.ReadLine() ?? "";

            room.Puzzle.Solve(answer, player);
        }

        private void ShowHint(Room room)
        {
            if (room.Puzzle.IsSolved)
            {
                Console.WriteLine("\n✓ Puzzle already solved - no hint needed!");
                return;
            }

            player.UseHint();
            room.Puzzle.ShowHint();
        }

        private void ShowStatus()
        {
            TimeSpan elapsed = gameTimer.Elapsed;
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║           CURRENT STATUS              ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine($"Player: {player.Name}");
            Console.WriteLine($"Current Location: {rooms[currentRoomIndex].Name}");
            Console.WriteLine($"Rooms Completed: {currentRoomIndex}/{rooms.Count}");
            Console.WriteLine($"Score: {player.Score}");
            Console.WriteLine($"Hints Used: {player.HintsUsed}");
            Console.WriteLine($"⏱️  Time Elapsed: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
            Console.WriteLine("═══════════════════════════════════════");
        }
    }
}