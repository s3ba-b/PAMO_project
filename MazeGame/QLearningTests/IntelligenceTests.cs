using System;
using Q_Learning;
using Xunit;

namespace QLearningTests
{
    public class IntelligenceTests
    {
        private Intelligence _sut;

        public IntelligenceTests()
        {
            _sut = new Intelligence(new MazeModel());
        }
        
        [Fact]
        public void AreTestsWorking()
        {
            Assert.True(true);
        }
    }
}