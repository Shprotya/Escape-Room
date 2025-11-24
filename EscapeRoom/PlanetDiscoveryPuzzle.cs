using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    public class PlanetDiscoveryPuzzle : Puzzle
    {
        // Property
        private string[] planets;
        private int[] years;
        private string selectedPlanet;
        private int correctYear;

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

        }

        // Methods
        public override bool Solve(string answer, Player player)
        {
            return true;
        }
    }
}
