using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    /// <summary>
    /// The final puzzle - a door with two locks: a keyhole and a word lock.
    /// Player needs the Golden Key from Room 2 AND must enter the correct word.
    /// </summary>
    class ExitDoorPuzzle : Puzzle
    {
        // Properties
        private string correctWord;
        private string requiredKey;

        // Constructor
        public ExitDoorPuzzle()
        {
            correctWord = "museum";
            requiredKey = "Golden Key";
            IsSolved = false;

            Description = "🚪 You've reached the EXIT DOOR! Freedom is so close!\n\n" +
                         "The door has TWO security locks:\n" +
                         "   🔑 1. A KEYHOLE (needs a physical key)\n" +
                         "   🔤 2. A WORD LOCK with 6 empty slots: [ _ _ _ _ _ _ ]\n\n" +
                         "A brass plaque on the wall reads:\n" +
                         "'Name the sacred temple where knowledge and history reside.'\n\n" +
                         "💡 You must have BOTH the key AND the correct word to escape!";

            Hint = "Think about where you are right now. What type of building holds ancient artifacts and history?";
        }
    }
}
