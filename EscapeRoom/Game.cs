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
            Console.WriteLine($"║ Welcome, {player.Name}!".PadRight(47) + "║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine("\nYou stand at the entrance of the museum.");
            Console.WriteLine("The doors lock behind you with a heavy THUD.");
            Console.WriteLine("\n⏱️ The timer will start when you press any key.");
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
            // The @ symbol turns a standard C# string into a verbatim string literal.
            // which means it allows multi-line text and ignores escape sequences.
            Console.WriteLine(@"
            ╔══════════════════════════════════════════════════════════════╗
            ║                                                              ║
            ║         🏛️ ESCAPE FROM THE ANCIENT MUSEUM 🏛️                 ║
            ║                                                              ║
            ║ You've been locked inside the mysterious Chronos Museum      ║
            ║ after closing time. The exhibits hold ancient secrets        ║
            ║ and puzzles. Solve them to escape before the night           ║
            ║ security system activates!                                   ║
            ║                                                              ║
            ║ ⏱️ TIME IS TICKING...                                        ║
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
            Console.WriteLine("• ⏱️ Your escape time will be recorded");
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
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.Write("Your choice: ");
        }

        /// <summary>
        /// Creates and populates all rooms and their puzzles for the game.
        /// </summary>
        private void SetupRooms()
        {
            // Room 1: ASTROLOGY HALL - With random planet puzzle! (Currently commented out)
            PlanetDiscoveryPuzzle room1Puzzle = new PlanetDiscoveryPuzzle();
            Room room1 = new Room(
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
            Room room2 = new Room(
                "Ancient Library",
                "You enter the Ancient Library. Dusty shelves filled with old tomes surround you.\n" +
                "In the center, a locked glass cabinet displays a glowing golden key inside.\n" +
                "Above the cabinet, a stone tablet shows a number sequence.",
                null  // ← Temporarily null, set it below
            );

            // Create the puzzle AFTER the room, passing room as parameter
            LibraryPuzzle room2Puzzle = new LibraryPuzzle(room2);
            room2.Puzzle = room2Puzzle;  // Now assign the puzzle

            room2.AddItem("Old Book"); // Only the book is here initially
            rooms.Add(room2);

            // Room 3: FINAL ROOM - Simple riddle puzzle
            ExitDoorPuzzle room3Puzzle = new ExitDoorPuzzle();
            Room room3 = new Room(
                "Exit Chamber",
                "You've reached the Exit Chamber. A massive door stands before you, adorned with ancient symbols.\n" +
                "A riddle is inscribed on the door, challenging you to prove your wisdom to escape.",
                room3Puzzle // Assign the puzzle to the room
            );
            //Add some items to the room
            room3.AddItem("Ancient Coin");
            rooms.Add(room3); // Add room to the game

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
                currentRoom.Enter();      // Display the room's entrance message
                ShowCurrentTimer();       // Show timer when entering room

                while (!currentRoom.IsCompleted)
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
                        default:
                            Console.WriteLine("\n✗ Invalid choice!");
                            break;
                    }

                    // CHECK FOR COMPLETION: If the puzzle is solved, trigger the Y/N prompt.
                    HandleRoomCompletion(currentRoom);
                }

                // Only move forward if the current room is completed
                if (currentRoom.IsCompleted)
                {
                    currentRoomIndex++; // Move to the next room
                }
            }
            gameTimer.Stop(); // STOP THE TIMER when game ends
            ShowVictory();            // Show victory screen

        } //End of Game Loop method

        #region Helper Methods For main Game Loop

        /// <summary>
        /// Prompts the player to move to the next room if the current puzzle is solved.
        /// Returns true if the player chooses to advance and the room is completed.
        /// </summary>
        private bool HandleRoomCompletion(Room currentRoom)
        {
            // Only proceed if the puzzle is solved and the room is not already marked complete
            if (currentRoom.Puzzle.IsSolved && !currentRoom.IsCompleted)
            {
                Console.WriteLine("\n🎉 Puzzle solved! The path is open!");
                Console.WriteLine("You can now proceed to the next area.");
                Console.WriteLine("\nPress **Y** to move to the next room, or **N** to stay in this room (to collect items, etc.).");
                Console.Write("Proceed? (Y/N): ");

                string proceedChoice = Console.ReadLine()?.ToLower() ?? "";

                if (proceedChoice == "y")
                {
                    // Mark the room as completed and allow the outer loop to advance the index.
                    currentRoom.Complete();
                    // We only award the room completion bonus when the player chooses to move on
                    player.Score += 200;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return true; // Signal that the room is complete
                }
                else
                {
                    // Player chose 'N' or an invalid choice.
                    // The room remains incomplete, keeping them in the inner loop.
                    Console.WriteLine("\nStaying in the room. Remember to check option 2 to examine for items!");

                }
            }
            return false; // Did not advance/is already complete
        }

        /// <summary>
        /// Displays the current elapsed time from the game timer.
        /// </summary>
        private void ShowCurrentTimer()
        {
            TimeSpan elapsed = gameTimer.Elapsed;
            Console.WriteLine($"\n⏱️ Time Elapsed: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
        }

        /// <summary>
        /// Displays the puzzle's description for the current room.
        /// </summary>
        private void ExaminePuzzle(Room room)
        {
            Console.WriteLine($"\n📋 PUZZLE:");
            // Check if the puzzle is solved and update the description if necessary
            if (room.Puzzle.IsSolved)
            {
                Console.WriteLine("\n✓ This puzzle is already solved! The way forward is open.");
            }
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
            // Special examination for Ancient Library
            else if (room.Name == "Ancient Library" && room.Puzzle is LibraryPuzzle libraryPuzzle)
            {
                Console.WriteLine("What do you want to examine?");
                Console.WriteLine("1. Study the number sequence tablet");
                Console.WriteLine("2. Search for items in this room");
                Console.WriteLine("3. Return to main menu");
                Console.Write("\nWhat do you want to examine? ");

                string examineChoice = Console.ReadLine();

                switch (examineChoice)
                {
                    case "1":
                        libraryPuzzle.ShowNumberSequence();  // Show sequence ONLY here
                        break;
                    case "2":
                        SearchForItems(room);
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
                // Special examintaion for searching items in any room
                SearchForItems(room);
            }

        }

        /// <summary>
        /// Prompts the player for an answer and attempts to solve the current room's puzzle.
        /// If the puzzle is already solved, it immediately prompts the player to proceed.
        /// </summary>
        private void AttemptPuzzle(Room room)
        {
            if (room.Puzzle.IsSolved)
            {
                Console.WriteLine("\n✓ This puzzle is already solved!");
                // New logic: Immediately trigger the 'Y/N' prompt without asking for the answer again.
                HandleRoomCompletion(room);
                return;
            }

            Console.WriteLine($"\n{room.Puzzle.Description}");
            Console.Write("\nYour answer: ");
            string answer = Console.ReadLine() ?? "";

            // Pass the answer and the player object to the puzzle's Solve method
            // If Solve returns true, the puzzle is marked IsSolved, and the GameLoop check will handle the prompt.
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
            Console.WriteLine($"⏱️ Time Elapsed: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
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
        /// Displays the victory screen with final statistics when the player successfully escapes.
        /// </summary>
        private void ShowVictory()
        {
            TimeSpan totalTime = gameTimer.Elapsed;
            Console.Clear();

            // Victory banner
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════╗
║                                                              ║
║           🎊 YOU ESCAPED THE MUSEUM! 🎊                      ║
║                                                              ║
║ Congratulations! You've solved all the ancient puzzles       ║
║ and collected the necessary items to unlock your freedom!    ║
║                                                              ║
║ The morning sun greets you as you step outside.              ║
║ The nightmare is over. You are FREE!                         ║
║                                                              ║
╚══════════════════════════════════════════════════════════════╝
");

            System.Threading.Thread.Sleep(2000);

            // Final statistics
            Console.WriteLine($"\n{new string('─', 60)}");
            Console.WriteLine("                     FINAL RESULTS");
            Console.WriteLine($"{new string('─', 60)}");
            Console.WriteLine($"🎭 Explorer Name: {player.Name}");
            Console.WriteLine($"💰 Final Score: {player.Score} points");
            Console.WriteLine($"💡 Hints Used: {player.HintsUsed}");
            Console.WriteLine($"⏱️ Escape Time: {totalTime.Minutes:D2}:{totalTime.Seconds:D2}");
            Console.WriteLine($"{new string('─', 60)}");

            // Time-based rating
            string timeRating;
            if (totalTime.TotalMinutes < 3)
                timeRating = "⚡ LIGHTNING FAST! You're a speedrunner!";
            else if (totalTime.TotalMinutes < 5)
                timeRating = "🏃 SPEEDY ESCAPE! Very impressive!";
            else if (totalTime.TotalMinutes < 10)
                timeRating = "🚶 STEADY PACE - Well done!";
            else
                timeRating = "🐌 TOOK YOUR TIME - But you made it!";

            Console.WriteLine($"\n⏱️ Time Rating: {timeRating}");

            // Score-based rating
            string scoreRating;
            if (player.Score >= 1400)
                scoreRating = "⭐⭐⭐ MASTER EXPLORER - Perfect run!";
            else if (player.Score >= 1200)
                scoreRating = "⭐⭐⭐ EXPERT ADVENTURER - Excellent!";
            else if (player.Score >= 1000)
                scoreRating = "⭐⭐ SKILLED EXPLORER - Great job!";
            else if (player.Score >= 800)
                scoreRating = "⭐ BRAVE SURVIVOR - You made it!";
            else
                scoreRating = "⭐ LUCKY ESCAPEE - Close call!";

            Console.WriteLine($"💯 Score Rating: {scoreRating}");

            Console.WriteLine($"\n{new string('═', 60)}");
            Console.WriteLine("           🏆 CONGRATULATIONS ON YOUR ESCAPE! 🏆");
            Console.WriteLine($"{new string('═', 60)}");

            // Inventory summary
            Console.WriteLine("\n📦 Items Collected During Your Journey:");
            if (player.Inventory.Count > 0)
            {
                foreach (var item in player.Inventory)
                {
                    Console.WriteLine($"  • {item}");
                }
            }
            else
            {
                Console.WriteLine("  (None - you traveled light!)");
            }

            Console.WriteLine($"\n{new string('─', 60)}");
            Console.WriteLine("\n✨ Thank you for playing Escape from the Ancient Museum! ✨");
        }

        #endregion
    }
}