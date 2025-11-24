using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    public class Player
    {
        // Player properties
        public string Name { get; set; }
        public List<string> Inventory { get; private set; } // List of items the player has collected
        public int Score { get; set; }
        public int HintsUsed { get; set; } 

        // Constructor
        public Player(string name)
        {
            Name = name;
            Inventory = new List<string>(); // Start with empty inventory
            Score = 1000; // Start with max score
            HintsUsed = 0;
        }

        // Methods
        public void UseHint()
        {
            HintsUsed++;
            Score -= 50; // Penalty for using hints
        }
    }
}
