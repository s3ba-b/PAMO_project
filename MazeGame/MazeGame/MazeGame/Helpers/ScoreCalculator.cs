using System;

namespace MazeGame.Helpers
{
    public class ScoreCalculator
    {
        public TimeSpan _time;
        
        public ScoreCalculator()
        {
            
        }
        
        public int Score { get; private set; }
        public bool IsGameStarted { get; private set; }

        public void StartGame()
        {
            IsGameStarted = true;
        }

        public void EndGame()
        {
            IsGameStarted = false;
        }
    }
}