using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    public class LibraryPuzzle : Puzzle
    {
        // Property
        private int correctAnswer;

        // Constructor
        public LibraryPuzzle()
        {
            correctAnswer = 13;  // Answer to the sequence
            IsSolved = false;

            Description = "A locked glass cabinet displays a glowing GOLDEN KEY inside.\n" +
                         "Above the cabinet, a stone tablet shows a number sequence:\n" +
                         "   1, 1, 2, 3, 5, 8, ?\n" +
                         "Below it reads: 'Enter the next number to unlock the cabinet.'";

            Hint = "This is the famous Fibonacci sequence! Each number is the sum of the previous two. (5 + 8 = ?)";
        }

        // Methods

        // Show the number sequence
        public void ShowNumberSequence()
        {
            Console.WriteLine("\n📊 You study the stone tablet carefully...");
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║   ANCIENT NUMBER SEQUENCE             ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║      1,  1,  2,  3,  5,  8,  ?       ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║   'What number comes next?'           ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine("\n💡 Look for a pattern in how the numbers grow...");
        }

        public override bool Solve(string answer, Player player)
        {
            if (int.TryParse(answer, out int playerAnswer))
            {
                if (playerAnswer == correctAnswer)
                {
                    IsSolved = true;
                    Console.WriteLine("\n🔓 The cabinet clicks open, revealing the GOLDEN KEY! You take it.");
                    player.Inventory.Add("Golden Key");
                    return true;
                }
                else
                {
                    Console.WriteLine("\n❌ That's not the correct number. Try again!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("\n❌ Please enter a valid number.");
                return false;
            }
        }
    }
}
