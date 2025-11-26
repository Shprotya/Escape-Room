using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    /// <summary>
    /// Serves as the abstract base class for all specific puzzles in the game (e.g., PlanetDiscoveryPuzzle).
    /// It defines the common structure and required functionality (the Solve method) for any game puzzle.
    /// </summary>
    public abstract class Puzzle
    {
        //Properties
        public string Description { get; set; }           // The prompt or question presented to the player
        public bool IsSolved { get; protected set; }      // Status of the puzzle (solved or unsolved)
        public string Hint { get; set; }                  // A piece of text to help the player if they are stuck

        /// <summary>
        /// Abstract method that must be implemented by all concrete puzzle classes. It contains the specific logic 
        /// to determine if the player's answer is correct for that puzzle.
        /// </summary>
        public abstract bool Solve(string answer, Player player);

        /// <summary>
        /// Displays the puzzle's hint text to the console. This method is virtual, allowing specific puzzles 
        /// to override it with more complex hint logic if needed.
        /// </summary>
        public virtual void ShowHint()
        {
            Console.WriteLine($"\n💡 HINT: {Hint}");
        }
    }
}