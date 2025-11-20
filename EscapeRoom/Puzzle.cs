using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    public abstract class Puzzle
    {
        // Puzzle Properties
        public string Description { get; set; }
        public bool IsSolved { get; protected set; } // This Property can be changed only in child classes
        public string Hint { get; set; }

        // Constructor
        public abstract bool Solve(string answer, Player player);


        // Methods
        public virtual void ShowHint()
        {
            Console.WriteLine($"\n💡 HINT: {Hint}");
        }
    }
}
