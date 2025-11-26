using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    /// <summary>
    /// Represents the player entity in the game, storing their name, score, inventory, and tracking hints used.
    /// </summary>
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

        /// <summary>
        /// Adds a collected item to the player's inventory list and provides console feedback.
        /// </summary>
        public void AddItem(string item)
        {
            Inventory.Add(item);
            Console.WriteLine($"\n✓ Added '{item}' to inventory!");
        }

        /// <summary>
        /// Checks if the player currently holds a specific item, performing a case-insensitive search.
        /// (Will be used later on)
        /// </summary>
        public bool HasItem(string item)
        {
            return Inventory.Contains(item, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Displays the full list of items currently held in the player's inventory.
        /// </summary>
        public void ShowInventory()
        {
            Console.WriteLine("\n=== INVENTORY ===");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        /// <summary>
        /// Registers that the player has used a hint, penalizes their score by 50 points, and increments the hint counter.
        /// </summary>
        public void UseHint()
        {
            HintsUsed++;
            Score -= 50; // Penalty for using hints
        }
    }
}
