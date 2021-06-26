using System;
using System.Xml;
using MazeGame.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace MazeGameTests.Helpers
{
    public class ScoreDbTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ScoreDb _sut;

        public ScoreDbTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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

            // when
            _sut.Add(expected);
            var actual = _sut.Get(1);
            
            
            // then
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ScoreShouldBeDeleted()
        {
            // given
            Score score = new Score {MazeId = 1, BestScore = 1500};

            // when
            _sut.Add(score);
            _sut.Delete(score);
            var actual = _sut.Get(1);
            
            
            // then
            Assert.Null( actual);

        }

        
        [Fact]
        // This test takes 2 minutes!
        public void ScoreShouldBeUpdated()
        {
            // given
            _sut.Add(new Score {MazeId = 1, BestScore = 1200});

            // when
            Score expected = new Score {MazeId = 1, BestScore = 1500};
            _sut.Update(expected);
            var actual = _sut.Get(1);
           
            
            // then
            Assert.Equal(expected.BestScore, actual.BestScore);
        }
    }
}