using System;
using System.Diagnostics;

namespace MazeGame.Helpers
{
    public interface IScoreCalculator
    {
        int Score { get; }
        bool IsGameStarted { get; }
        void StartGame();
        void EndGame();
    }
    
    public class ScoreCalculator : IScoreCalculator
    {
        private readonly Stopwatch _stopwatch;

        public ScoreCalculator()
        {
            _stopwatch = new Stopwatch();
        }

        public int Score
        {
            get
            {
                var ts = new TimeSpan(0, 2, 0);
                var time = ts - _stopwatch.Elapsed;
                return (int) time.TotalMilliseconds;
            }
        }

        public bool IsGameStarted { get; private set; }

        public void StartGame()
        {
            IsGameStarted = true;
            _stopwatch.Start();
        }

        public void EndGame()
        {
            IsGameStarted = false;
            _stopwatch.Stop();
        }
    }
}