using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    // It is abstract because every puzzle
    // must implement its own specific Solve logic.
    public abstract class Puzzle
    {
        //Properties
        public string Description { get; set; }           // The prompt or question presented to the player
        public bool IsSolved { get; protected set; }      // Status of the puzzle (solved or unsolved)
        public string Hint { get; set; }                  // A piece of text to help the player if they are stuck

        // Methods
        public abstract bool Solve(string answer, Player player); // It must be implemented by any class that inherits from Puzzle.
        public virtual void ShowHint()
        {
            Console.WriteLine($"\n💡 HINT: {Hint}");
        }
    }
}