# 🎮 Specialisterne Wordle

***Project explained to ChatGPT, who generated this ReadMe file***

A clean, desktop-based Wordle implementation built with **.NET + Uno Platform**.

This project was developed as part of the *Specialisterne Academy* and demonstrates structured architecture, persistence, logging, and MVVM-based UI composition.

---

## 🚀 How To Play

1. Download the latest release from:

   👉 https://github.com/andr9528/Specialisterne-Wordle/releases

2. Download the file:

 `Wordle.Frontend.exe`
 
3. Double-click the `.exe` file to launch the game.

No installation required.

---

## 🕹 Gameplay

- The game uses a word list containing **2000+ five-letter words**.
- Enter a guess using the input box.
- Submit your guess.
- Tiles will indicate:
    - 🟩 Correct letter, correct position
    - 🟨 Correct letter, wrong position
    - ⬜ Letter not in word

---

## 💾 Persistence

The game state is persisted locally.

- If the application is closed before finishing a game, it will **resume the unfinished game** on next startup.
- A new game is automatically started immediately after completing one.

---

## 📜 Logging

The application includes structured logging.

At the moment:

- The log is the **only place** where you can see whether the last game was **won or lost**.
- Logging is useful for debugging and tracking internal game state transitions.

---

## ⚙️ Functionality Overview

- ✅ Persistent game state (resume unfinished games)
- ✅ 2000+ five-letter word dictionary
- ✅ Automatic new game upon completion
- ✅ Structured logging
- ✅ Clean MVVM architecture
- ✅ Entity Framework Core persistence layer
- ✅ Fully self-contained single-file desktop release

---

## 🛠 Architecture

The solution is structured into separate projects:

- `Wordle.Abstraction`
- `Wordle.Model`
- `Wordle.Persistence`
- `Wordle.Services`
- `Wordle.Frontend`
- `Wordle.Tests`

The project follows:

- SOLID principles
- Dependency Injection
- ViewModel-driven UI
- EF Core persistence
- Unit testing (NUnit + FluentAssertions + Moq)

---

## 🧪 Running From Source

### Requirements

- .NET SDK
- Visual Studio 2022+
- Uno Platform workload

### Build & Run

```bash
dotnet build
dotnet run --project Wordle.Frontend
```
