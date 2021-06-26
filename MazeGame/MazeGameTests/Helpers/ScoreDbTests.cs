﻿using MazeGame.Helpers;
using Xunit;

namespace MazeGameTests.Helpers
{
    public class ScoreDbTests
    {
        private readonly ScoreDb _sut;

        public ScoreDbTests()
        {
            _sut = new ScoreDb();
        }
        
        [Fact]
        public void AreTestsWorking()
        {
            Assert.True(true);
        }

        [Fact]
        public void ScoreShouldBeAdded()
        {
            // given
            Score expected = new Score {MazeId = 1, BestScore = 1500};
            _sut.Add(expected);
            
            // when
            var actual = _sut.Get(1);
            
            
            // then
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ScoreShouldBeDeleted()
        {
            // given
            Score score = new Score {MazeId = 1, BestScore = 1500};
            _sut.Add(score);
            
            // when
            _sut.Delete(score);
            var actual = _sut.Get(1);
            
            
            // then
            Assert.Null( actual);

        }

        
        [Fact]
        public void ScoreShouldBeUpdated()
        {
            // given
            _sut.Add(new Score {MazeId = 1, BestScore = 1200});
            Score expected = new Score {MazeId = 1, BestScore = 1500};
            _sut.Update(expected);
            
            // when
            
            var actual = _sut.Get(1);
            
            
            // then
            Assert.Equal(expected, actual);
        }
    }
}