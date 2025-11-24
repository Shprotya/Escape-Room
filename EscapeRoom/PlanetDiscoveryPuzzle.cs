using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EscapeRoom
{
    public class PlanetDiscoveryPuzzle : Puzzle
    {
        // Property
        private string[] planets;
        private int[] years;
        private string selectedPlanet;
        private int correctYear;
        private bool hasUsedTelescope; // Track if player looked through telescope

        // Constructor
        public PlanetDiscoveryPuzzle()
        {
            // Set up arrays
            planets = new string[] { "Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };
            years = new int[] { 1631, 1610, 1610, 1610, 1610, 1781, 1846, 1930 };

            // Pick random planet each round
            Random random = new Random();
            int randomIndex = random.Next(8);

            // Save which planet we use
            selectedPlanet = planets[randomIndex];
            correctYear = years[randomIndex];

            hasUsedTelescope = false; //Player didn't look in telescope yet
            IsSolved = false; // Puzzle starts unsolved

            Description = "A sturdy wooden door blocks your path. It has a 4-digit keypad lock.\n" +
                         "On the wall, there's an ancient telescope pointed at the night sky.\n" +
                         "A plaque reads: 'The stars hold the key.'";

            Hint = "Look through the telescope first to see which planet is visible, then enter the year it was discovered.";
                
        }

        // Methods

        // Method to look through telescope
        public void LookThroughTelescope()
        {
            if (hasUsedTelescope)
            {
                Console.WriteLine($"\nYou peer through the telescope again. {selectedPlanet} is still visible in the night sky.");
                return;
            }

            hasUsedTelescope = true;
            Console.WriteLine("\n🔭 You look through the ancient telescope...");
            Thread.Sleep(1000); // Dramatic pause
            Console.WriteLine($"You can see a celestial body clearly labeled: {selectedPlanet.ToUpper()}");
            Console.WriteLine("The planet shines brightly in the night sky.");
        }
        public override bool Solve(string answer, Player player)
        {
            return true;
        }
    }
}
