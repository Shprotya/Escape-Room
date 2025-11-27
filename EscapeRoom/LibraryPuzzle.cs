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

            Description = "\n🔒 The cabinet is locked. You see a glowing key inside" +
                "To open it, solve the number sequence puzzle." +
                "\n📊 You study the stone tablet carefully..." +
                "\n╔═══════════════════════════════════════╗" +
                "║   ANCIENT NUMBER SEQUENCE             ║" +
                "╠═══════════════════════════════════════╣" +
                "║                                       ║" +
                "║      1,  1,  2,  3,  5,  8,  ?        ║" +
                "║                                       ║" +
                "║   'What number comes next?'           ║" +
                "║                                       ║" +
                "╚═══════════════════════════════════════╝" +
                "\n💡 Look for a pattern in how the numbers grow...";

            Hint = "This is the famous Fibonacci sequence! Each number is the sum of the previous two. (5 + 8 = ?)";
        }

        // Methods
            
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
