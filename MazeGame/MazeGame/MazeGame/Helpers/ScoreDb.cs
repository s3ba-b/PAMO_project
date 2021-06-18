using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazeGame.Helpers
{
    public class ScoreDb
    {
        private readonly List<Score> _scores;

        public ScoreDb()
        {
            _scores = new List<Score>();
        }

        public void Add(Score score)
        {
            _scores.Add(score);
        }
        
        public List<Score> Get()
        {
            return _scores;
        }
        
        public Score Get(int mazeId)
        {
            return _scores.FirstOrDefault(x => x.MazeId == mazeId);
        }
        
        public void Update(Score score)
        {
            var obj = _scores.FirstOrDefault(x => x.MazeId == score.MazeId);
            if (obj != null) obj.BestScore = score.BestScore;
        }
        
        public void Delete(Score score)
        {
            var obj = _scores.FirstOrDefault(x => x.MazeId == score.MazeId);
            if (obj != null) _scores.Remove(obj);
        }
    }

    public class Score
    {
        public int MazeId { get; set; }
        public int BestScore { get; set; }
    }
}