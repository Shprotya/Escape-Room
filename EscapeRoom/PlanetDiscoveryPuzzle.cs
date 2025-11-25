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
            Thread.Sleep(2000); // Dramatic pause
            Console.WriteLine($"You can see a celestial body clearly labeled: {selectedPlanet.ToUpper()}");
            Console.WriteLine("The planet shines brightly in the night sky.");
        }

        public void ShowDiscoveryChart()
        {
            // Zip combines the two arrays into an enumerable of anonymous objects
            var planetData = planets.Zip(years, (p, y) => new { Planet = p, Year = y });

            Console.WriteLine("\n📜 PLANET DISCOVERY CHART");
            Console.WriteLine("═══════════════════════════════════");

            // Order the combined data by Year, then print
            foreach (var item in planetData.OrderBy(item => item.Year))
            {
                Console.WriteLine($"{item.Planet,-12} - Year {item.Year}");
            }
            Console.WriteLine("═══════════════════════════════════");
        }

        public override bool Solve(string answer, Player player)
        {
            // Check if player looked through telescope first
            if (!hasUsedTelescope)
            {
                Console.WriteLine("\n✗ The keypad doesn't respond. Maybe you should examine the room first...");
                return false;
            }

            // Try to convert answer to a number
            if (int.TryParse(answer, out int enteredYear))
            {
                // Check if correct
                if (enteredYear == correctYear)
                {
                    IsSolved = true;
                    Console.WriteLine($"\n✓ CORRECT! {selectedPlanet} was discovered in {correctYear}!");
                    Console.WriteLine("The keypad beeps and the door unlocks with a satisfying CLICK!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"\n✗ The year {enteredYear} is incorrect. The keypad beeps angrily.");

                    // Give helpful feedback if they entered a valid planet year
                    for (int i = 0; i < years.Length; i++)
                    {
                        if (years[i] == enteredYear)
                        {
                            Console.WriteLine($"💡 That's when {planets[i]} was discovered, but that's not what you saw through the telescope!");
                            break;
                        }
                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("\n✗ Please enter a 4-digit year (example: 1781)");
                return false;
            }
        }
    }
}
