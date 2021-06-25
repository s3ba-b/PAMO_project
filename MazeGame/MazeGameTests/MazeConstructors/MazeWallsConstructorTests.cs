using MazeGame.Helpers;
using MazeGame.MazeConstructors;
using Q_Learning;
using Xunit;

namespace MazeGameTests.MazeConstructors
{
    public class MazeWallsConstructorTests
    {

        private MazeWallsConstructor _sut;

        public MazeWallsConstructorTests()
        {
            _sut = new MazeWallsConstructor(new MazeSettings());
        }
        
        [Fact]
        public void AreTestsWorking()
        {
            Assert.True(true);
        }
        
        [Fact]
        public void ShouldReturnTrueIfBeWallBetween()
        {
            // given
            int firstSquareIndex = 0;
            int secondSquareIndex = 1;

            // when

            // _sut.

            // Assert
            Assert.True(_sut.ShouldBeWallBetween(firstSquareIndex, secondSquareIndex));
            
        }
    }
}