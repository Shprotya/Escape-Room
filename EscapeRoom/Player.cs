using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    class Player
    {
        // Player properties
        public string Name { get; set; }
        public List<string> Inventory { get; private set; }
        public int Score { get; set; }
        public int HintsUsed { get; set; }

        // Constructor
        public Player(string name)
        {
            Name = name;
            Inventory = new List<string>();
            Score = 1000; // Start with max score
            HintsUsed = 0;
        }
    }
}
