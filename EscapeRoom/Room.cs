using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal class Room
    {
        // Properties
        public string Name { get; set; }                // Name of the room (e.g., "Astrology")
        public string Description { get; set; }         // Text description shown to the player
        public Puzzle Puzzle { get; set; }              // The puzzle the player must solve to complete the room
        public List<string> ItemsInRoom { get; private set; } // List of interactive items (e.g., "key", "note")
        public bool IsCompleted { get; private set; }   // True if the room's puzzle has been solved

        // Constructor
        public Room(string name, string description, Puzzle puzzle)
        {
            Name = name;                               
            Description = description;                 
            Puzzle = puzzle;                            // Assign the room's puzzle
            ItemsInRoom = new List<string>();           // Create an empty list for items
            IsCompleted = false;                        // Room starts as incomplete
        }

        // Methods
        public void AddItem(string item)
        {
            ItemsInRoom.Add(item);
        }

        public void Enter()
        {
            Console.Clear();
            Console.WriteLine($"\n===================================");
            Console.WriteLine($"\n   {Name.ToUpper()}");
            Console.WriteLine($"\n===================================");
            Console.WriteLine($"\n{Description}");
        }

        public bool TakeItem(string item, Player player)
        {
            var foundItem = ItemsInRoom.FirstOrDefault(i =>
                i.Equals(item, StringComparison.OrdinalIgnoreCase));

            if (foundItem != null)
            {
                ItemsInRoom.Remove(foundItem);
                player.AddItem(foundItem);
                return true;
            }
            return false;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}