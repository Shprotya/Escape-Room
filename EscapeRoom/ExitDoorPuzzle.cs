using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    /// <summary>
    /// The final puzzle - a door with two locks: a keyhole and a word lock.
    /// Player needs the Golden Key from Room 2 AND must enter the correct word.
    /// </summary>
    public class ExitDoorPuzzle : Puzzle  // Change to public
    {
        // Properties
        private string correctWord;
        private string requiredKey;
        private int attemptsWithoutKey;

        // Constructor
        public ExitDoorPuzzle()
        {
            correctWord = "museum";
            requiredKey = "Golden Key";
            IsSolved = false;
            attemptsWithoutKey = 0;

            Description = "🚪 You've reached the EXIT DOOR! Freedom is so close!\n\n" +
                         "The door has TWO security locks:\n" +
                         "   🔑 1. A KEYHOLE (needs a physical key)\n" +
                         "   🔤 2. A WORD LOCK with 6 empty slots: [ _ _ _ _ _ _ ]\n\n" +
                         "A brass plaque on the wall reads:\n" +
                         "'Name the sacred temple where knowledge and history reside.'\n\n" +
                         "💡 You must have BOTH the key AND the correct word to escape!";

            Hint = "Think about where you are right now. What type of building holds ancient artifacts and history?";
        }

        // Override Solve method
        public override bool Solve(string answer, Player player)
        {
            // Check for key
            if (!player.HasItem(requiredKey))
            {
                attemptsWithoutKey++;

                if (attemptsWithoutKey >= 3)
                {
                    ShowGameOver();
                    Environment.Exit(0);
                }

                Console.WriteLine("\n✗ ERROR: The keyhole is empty!");
                Console.WriteLine($"💡 You need the '{requiredKey}' to unlock this door.");
                Console.WriteLine($"\n⚠️  Attempts without key: {attemptsWithoutKey}/3");
                Console.WriteLine("If you can't find the key after 3 attempts, you'll be trapped forever!");
                return false;
            }

            // Check if they entered the correct word
            if (answer.Equals(correctWord, StringComparison.OrdinalIgnoreCase))
            {
                IsSolved = true;

                // Victory animation!
                Console.WriteLine("\n✓ CORRECT! The word is 'MUSEUM'!");
                Console.WriteLine("\n" + new string('═', 50));
                Console.WriteLine("🔑 You insert the Golden Key into the keyhole...");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("   *CLICK*");
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("\n🔤 You enter M-U-S-E-U-M into the word lock...");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("   *CLACK*");
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("\n✨ Both locks disengage simultaneously! ✨");
                Console.WriteLine(new string('═', 50));
                Console.WriteLine("\n🚪 The heavy door swings open!");
                Console.WriteLine("🌅 Fresh morning air rushes in. Sunlight floods the corridor.");
                Console.WriteLine("🎉 YOU ARE FREE!");

                return true;
            }
            else
            {
                Console.WriteLine($"\n✗ '{answer}' is incorrect.");
                Console.WriteLine("The word lock doesn't budge. Try again!");

                if (answer.Length != 6)
                {
                    Console.WriteLine($"💡 Hint: The word has 6 letters, your answer has {answer.Length}.");
                }

                return false;
            }
        }

        // Show dramatic game over screen
        private void ShowGameOver()
        {
            Console.Clear();
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════╗
║                                                              ║
║                    ☠️  GAME OVER  ☠️                          ║
║                                                              ║
╚══════════════════════════════════════════════════════════════╝
");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\nYou stand before the exit door, so close to freedom...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("But you realize with horror: YOU DON'T HAVE THE KEY!");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\nYou frantically search your pockets... Nothing.");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("The key must still be in the Ancient Library...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\nBut you can't go back now. The doors have sealed behind you.");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n💀 You hear a mechanical whirring...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("The security system activates. Metal shutters slam down.");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n🚨 ALARM BLARING 🚨");
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("\nYou are trapped in the museum...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Forever.");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("          You failed to escape the museum.");
            Console.WriteLine("      Always collect important items before moving on!");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
} 