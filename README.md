# 🏛️ Escape from the Ancient Museum

A C# console escape room game where you're locked inside the mysterious **Chronos Museum** after closing time. Solve puzzles, collect items, and race the clock to break free before the security system activates.

---

## 🎮 Gameplay

Navigate through three rooms, each with a unique puzzle that must be solved to progress. Examine your surroundings carefully — some items are just bonus points, while others are **required** to escape.

| Room | Puzzle Type |
|------|-------------|
| 🔭 Astrology Hall | Identify a planet through the telescope, enter its discovery year |
| 📚 Ancient Library | Solve the Fibonacci number sequence to unlock the Golden Key |
| 🚪 Exit Chamber | Answer a riddle *and* use the Golden Key to open the final door |

---

## ✨ Features

- ⏱️ **Live timer** — your escape time is recorded and rated
- 💰 **Score system** — starts at 1000, earn bonuses for collecting items (+50 each), lose points for hints (-50 each)
- 🎒 **Inventory system** — pick up items that carry across rooms
- 🔀 **Randomised puzzle** — the planet in Room 1 is randomly selected each playthrough
- ☠️ **Fail state** — attempt the final door without the Golden Key and it's game over
- 🏆 **End-game ratings** — scored on both time and final points

---

## 🏗️ Project Structure
```
EscapeRoom/
├── Program.cs                # Entry point
├── Game.cs                   # Main game loop and flow control
├── Room.cs                   # Room model (items, puzzle, state)
├── Player.cs                 # Player model (inventory, score, hints)
├── Puzzle.cs                 # Abstract base class for all puzzles
├── PlanetDiscoveryPuzzle.cs  # Room 1 — randomised planet/year puzzle
├── LibraryPuzzle.cs          # Room 2 — Fibonacci sequence puzzle
└── ExitDoorPuzzle.cs         # Room 3 — final riddle + key requirement
```

---

## 🛠️ Tech Stack

- **Language:** C# (.NET Framework 4.7.2)
- **IDE:** Visual Studio 2022
- **Concepts used:** OOP, inheritance, abstract classes, interfaces, LINQ, `Stopwatch`, `List<T>`

---

## 🚀 Getting Started

1. Clone the repository
2. Open `EscapeRoom.sln` in Visual Studio
3. Press **F5** to build and run

No external dependencies required.

---

## 💡 Tips

- Always **examine the room** before attempting a puzzle
- **Collect the Golden Key** in Room 2 — you cannot finish the game without it
- Use hints sparingly; each one costs 50 points

---

## 📜 License

Built as a college OOP assignment. Feel free to fork and extend it!
