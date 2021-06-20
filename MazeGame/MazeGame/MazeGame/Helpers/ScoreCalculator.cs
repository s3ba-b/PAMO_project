using System;
using System.Diagnostics;

namespace MazeGame.Helpers
{
    public class ScoreCalculator
    {
        public Stopwatch _stopwatch;

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