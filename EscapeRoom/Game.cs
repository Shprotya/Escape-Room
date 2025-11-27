using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // For Stopwatch timer

namespace EscapeRoom
{
    /// <summary>
    /// The main class that controls the game flow, manages rooms, and tracks player progress.
    /// </summary>
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

        /// <summary>
        /// Starts the entire game process: displays intro, gets player name, starts timer, and runs the game loop.
        /// </summary>
        public void Start()
        {
            ShowIntro();                          // Show welcome screen

            Console.Write("\nEnter your name, brave explorer: ");
            string name = Console.ReadLine();     // Get player's name
            player = new Player(name);            // Create player object

            // Ready screen - wait for player to start the timer
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

            SetupRooms();                         // Build the rooms and puzzles for the game
            GameLoop();                           // Enter the main interaction loop
        }

        /// <summary>
        /// Displays the game's title, backstory, and rules.
        /// </summary>
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

        /// <summary>
        /// Displays the list of available actions (menu options) to the player.
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("1. 👓 Examine Puzzle ");
            Console.WriteLine("2. 🔍 Examine room (find items & clues)");
            Console.WriteLine("3. 🔐 Attempt to solve puzzle");
            Console.WriteLine("4. 🎒 Check inventory");
            Console.WriteLine("5. 💡 Get hint (-50 points)");
            Console.WriteLine("6. 📊 Show status");
            // Only show the 'back' option if the player has completed at least one room
            if (currentRoomIndex > 0)
                Console.WriteLine("Type 'back' to return to previous room");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.Write("Your choice: ");
        }

        /// <summary>
        /// Creates and populates all rooms and their puzzles for the game.
        /// </summary>
        private void SetupRooms()
        {
            // Room 1: ASTROLOGY HALL - With random planet puzzle!
            var room1Puzzle = new PlanetDiscoveryPuzzle();
            var room1 = new Room(
                "Astrology Hall",
                "You find yourself in the Astrology Hall. Ancient star charts cover the walls.\n" +
                "A magnificent brass telescope stands in the center, pointed toward a skylight.\n" +
                "The only exit is blocked by an ornate door with a numerical keypad.",
                room1Puzzle // Assign the puzzle to the room
            );
            //Add some items to the room
            room1.AddItem("Star Chart");
            room1.AddItem("Museum Guide");
            rooms.Add(room1); // Add room to the game

            // Room 2: LIBRARY - Fibonacci puzzle
            var room2Puzzle = new LibraryPuzzle();
            var room2 = new Room(
                "Ancient Library",
                "You enter the Ancient Library. Dusty shelves filled with old tomes surround you.\n" +
                "In the center, a locked glass cabinet displays a glowing golden key inside.\n" +
                "Above the cabinet, a stone tablet shows a number sequence.",
                room2Puzzle // Assign the puzzle to the room
            );
            //Add some items to the room
            room2.AddItem("Old Book");
            rooms.Add(room2); // Add room to the game
        }

        /// <summary>
        /// The central loop of the game. It controls room transitions and player interactions within the current room.
        /// </summary>
        private void GameLoop()
        {
            // Loop while the player has not completed all rooms
            while (currentRoomIndex < rooms.Count)
            {   
                Room currentRoom = rooms[currentRoomIndex];
                currentRoom.Enter();        // Display the room's entrance message
                ShowCurrentTimer();         // Show timer when entering room

                // Flag to indicate if the player chose to go back
                bool wentBack = false;

                while (!currentRoom.IsCompleted && !wentBack)
                {
                    ShowMenu();
                    // Ensures that 'choice' is always a non-null string, 
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
                        case "back":
                            // Set the flag if going back was successful
                            wentBack = GoBack();
                            break;
                        default:
                            Console.WriteLine("\n✗ Invalid choice!");
                            break;
                    }

                    if (wentBack)
                    {
                        break; // Exit the inner while loop
                    }

                    // Check if the puzzle was just solved and complete the room
                    if (currentRoom.Puzzle.IsSolved && !currentRoom.IsCompleted)
                    {
                        currentRoom.Complete();
                        player.Score += 200; // Bonus for completing room
                        Console.WriteLine("\n🎉 Room completed! You can proceed to the next area...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }

                // Only move forward if the current room is completed
                if (currentRoom.IsCompleted)
                {
                    currentRoomIndex++; // Move to the next room
                }
            }
            gameTimer.Stop(); // STOP THE TIMER when game ends

        } //End of Game Loop method

        #region Helper Methods For main Game Loop

        /// <summary>
        /// Displays the current elapsed time from the game timer.
        /// </summary>
        private void ShowCurrentTimer()
        {
            TimeSpan elapsed = gameTimer.Elapsed;
            Console.WriteLine($"\n⏱️  Time Elapsed: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
        }

        /// <summary>
        /// Displays the puzzle's description for the current room.
        /// </summary>
        private void ExaminePuzzle(Room room)
        {
            Console.WriteLine($"\n📋 PUZZLE:");
            Console.WriteLine(room.Puzzle.Description);
        }

        /// <summary>
        /// Allows the player to examine the current room for clues or to interact with room-specific objects.
        /// Includes special interaction logic for the Astrology Hall.
        /// </summary>
        private void ExamineRoom(Room room)
        {
            // Special examination for Astrology Hall
            if (room.Name == "Astrology Hall" && room.Puzzle is PlanetDiscoveryPuzzle planetPuzzle)
            {
                Console.WriteLine("\nYou examine the room carefully...");
                Console.WriteLine("\n1. Look through the telescope");
                Console.WriteLine("2. Study the planet discovery chart on the wall");
                Console.WriteLine("3. Search for items in this room");
                Console.WriteLine("4. Return to main menu");
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
                        SearchForItems(room);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice.");
                        break;
                }
            }
            else
            {
                // Special examintaion for searching items in any room
                SearchForItems(room);
            }

        }

        /// <summary>
        /// Prompts the player for an answer and attempts to solve the current room's puzzle.
        /// </summary>
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

            // Pass the answer and the player object to the puzzle's Solve method
            room.Puzzle.Solve(answer, player);
        }

        /// <summary>
        /// Displays a hint for the current puzzle if the player chooses to use one, subtracting points.
        /// </summary>
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

        /// <summary>
        /// Displays the player's current game statistics.
        /// </summary>
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

        /// <summary>
        /// Allows the player to find and collect hidden items in the room, awarding bonus points.
        /// </summary>
        private void SearchForItems(Room room)
        {
            Console.WriteLine("\n🔦 You search the room thoroughly...");
            System.Threading.Thread.Sleep(1000); // Dramatic pause

            if (room.ItemsInRoom.Count == 0)
            {
                Console.WriteLine("\n✗ You don't find any items here.");
                Console.WriteLine("(All items in this room have been collected)");
                return;
            }

            Console.WriteLine("\n✓ You found some items:");
            // List available items
            for (int i = 0; i < room.ItemsInRoom.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {room.ItemsInRoom[i]}");
            }

            Console.Write("\nWhich item do you want to take? (enter number or name): ");
            string choice = Console.ReadLine() ?? "";

            // Try to parse as number
            if (int.TryParse(choice, out int itemIndex) && itemIndex > 0 && itemIndex <= room.ItemsInRoom.Count)
            {
                string item = room.ItemsInRoom[itemIndex - 1];
                // Remove item from room and add to player's inventory
                room.TakeItem(item, player);
                player.Score += 50; // BONUS POINTS!
                Console.WriteLine($"💰 +50 bonus points for collecting an item!");
            }
            else
            {
                // Try to find by name
                if (room.TakeItem(choice, player))
                {
                    player.Score += 50; // BONUS POINTS!
                    Console.WriteLine($"💰 +50 bonus points for collecting an item!");
                }
                else
                {
                    Console.WriteLine($"\n✗ Couldn't find '{choice}'.");
                }
            }
        }

        /// <summary>
        /// Allows the player to return to the previous room if they forgot to collect items.
        /// </summary>
        private bool GoBack()
        {
            if (currentRoomIndex == 0)
            {
                Console.WriteLine("\n✗ You're in the first room. Can't go back!");
                return false;
            }

            Console.WriteLine($"\n↩️  Going back to {rooms[currentRoomIndex - 1].Name}...");
            System.Threading.Thread.Sleep(1000);
            currentRoomIndex--;  // Move back one room
            return true;  // Signal that we went back
        }
        #endregion
    }
}