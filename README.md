# MazeGame
## Short description
A simple game in which the player solves a maze using a limited number of prompts provided by the reinforcement learning algorithm.
## Goal of the application
The purpose of the application is to present the role of machine learning in games, even as simple as maze games. The optimal path to the maze solution has already been calculated by the reinforcement learning algorithm (QLearninig) and is used to prompt the user on the next steps to take if a problem is encountered. Currently, the game does not implement more complicated scenarios, but this may change in the future.
## Requirements and features
- Choose from 3 different mazes to play.
- Timing the game and assigning points based on it.
- Storing the best result obtained so far for a given maze and displaying it in the menu next to the button that starts the game for the given maze.
- The user moves through the labyrinth using three buttons - Up, Left, Right and Down.
- During the game, the user can use three tips.
- The hints the player has access to are provided by the reinforcement learning algorithm - Qlearning.
- After passing the labyrinth, a message is displayed in the dialog window about the number of points scored, then, after accepting, the player is taken back to the Application Menu.
## Challenges found during development
- Transfer of the game from the previous version of WPF and without user control to the current version - Xamarin with user control.
- The need to get acquainted with the new framework in the .NET ecosystem for the mobile area - Xamarin.
- Transition of WPF objects to Xamarin objects.
- Adaptation of the Qlearning library for the needs of the new mobile application.
- Implementation of the player's movement on the board.
- Ensuring that the player cannot pass through any of the walls.
- Ensuring that clues are displayed correctly in relation to the player's current position on the board.
- Maintaining a properly functioning game logic.
## Authors
Katarzyan Czerwińska, Sebastian Bobrowski, Tomasz Mnich
