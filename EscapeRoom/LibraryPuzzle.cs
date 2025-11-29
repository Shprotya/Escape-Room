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
        private Room parentRoom;  // Reference to the room this puzzle is in

        // Constructor - now takes the room as parameter
        public LibraryPuzzle(Room room)
        {
            correctAnswer = 13;
            IsSolved = false;
            parentRoom = room;  // Save reference to the room

            Description = "A locked glass cabinet displays a glowing GOLDEN KEY inside.\n" +
                         "Above the cabinet, a stone tablet shows a mysterious number sequence.\n" +
                         "You need to solve it to unlock the cabinet.";

            Hint = "This is the famous Fibonacci sequence! Each number is the sum of the previous two. (5 + 8 = ?)";
        }

        // Methods

        public void ShowNumberSequence()
        {
            Console.WriteLine("\n📊 You study the stone tablet carefully...");
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║   ANCIENT NUMBER SEQUENCE             ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║      1,  1,  2,  3,  5,  8,  ?        ║");
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
                    Console.WriteLine("\n✓ CORRECT! The answer is 13!");
                    Console.WriteLine("🔓 The glass cabinet clicks open!");

                    // ADD THE GOLDEN KEY TO THE ROOM NOW!
                    parentRoom.AddItem("Golden Key");

                    Console.WriteLine("🔑 The Golden Key is now available!");
                    Console.WriteLine("💡 Search the room to collect it!");
                    Console.WriteLine("\n⚠️  WARNING: You'll need this key to escape the museum!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"\n✗ {playerAnswer} is not correct. The cabinet remains locked.");
                    Console.WriteLine("💡 Look at how each number relates to the previous ones...");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("\n✗ Please enter a number.");
                return false;
            }
        }
    }
}