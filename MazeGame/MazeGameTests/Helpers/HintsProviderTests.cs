using System.Collections.Generic;
using FluentAssertions;
using MazeGame.Helpers;
using Q_Learning;
using Xunit;

namespace MazeGameTests.Helpers
{
    public class HintsProviderTests
    {
        
        private readonly MazeModel _model; 
        
        public HintsProviderTests()
        {
            // How the maze model looks like:
            //
            //      0   1 | 2
            //         ___ 
            //      3   4   5
            //         ______
            //      6   7   8
            
            var matrix = new int[9][];
            for (int i = 0; i < 9; ++i) matrix[i] = new int[9];
            matrix[0][1] = matrix[0][3] = matrix[1][0] = matrix[2][5] = 1;
            matrix[3][0] = matrix[3][4] = matrix[3][6] = matrix[4][3] = 1;
            matrix[4][5] = matrix[5][4] = matrix[5][2] = matrix[6][3] = 1;
            matrix[6][7] = matrix[7][3] = matrix[7][8] = matrix[8][7] = 1;
            matrix[6][7] = matrix[7][3] = matrix[7][8] = matrix[8][7] = 1;

            var quality = new double[9][];
            for (int i = 0; i < 9; ++i) quality[i] = new double[9];
            
            var reward = new double[9][];
            for (int i = 0; i < 9; ++i) reward[i] = new double[9];
            reward[0][1] = reward[0][3] = reward[1][0] = reward[2][5] = -0.01;
            reward[3][0] = reward[3][4] = reward[3][6] = reward[4][3] = -0.01;
            reward[4][5] = reward[5][4] = reward[5][2] = reward[6][3] = -0.01;
            reward[6][7] = reward[7][3] = reward[7][8] = reward[8][7] = -0.01;
            reward[6][7] = reward[7][3] = reward[7][8] = reward[8][7] = -0.01;
            reward[5][2] = 10000.0;
            
            _model = new MazeModel
            {
                Id = 1,
                Goal = 2,
                Start = 6,
                QuantityOfColumns = 3,
                QuantityOfRows = 3,
                SizeOfCell = 40,
                Matrix = matrix,
                Quality = quality,
                Reward = reward

            };

        }
        
        [Fact]
        public void AreTestsWorking()
        {
            Assert.True(true);
        }

        [Fact]
        public void Hint1ShouldBeCorrect()
        {
            // given
            var expected = new List<int> {3, 4, 5};
            var crossedCells = new List<int> {6};
            var sut = new HintsProvider(crossedCells, _model);
            
            // when
            var actual = sut.GetHintCellsIndexes();

            // then
            actual.Should().Equal(expected);
        }
        
        [Fact]
        public void Hint2ShouldBeCorrect()
        {
            // given
            var expected = new List<int> {6, 3, 0};
            var crossedCells = new List<int> {0, 3, 4};
            var sut = new HintsProvider(crossedCells, _model);
            
            // when
            var actual = sut.GetHintCellsIndexes();

            // then
            actual.Should().Equal(expected);

        }
        
        [Fact]
        public void Hint3ShouldBeCorrect()
        {
            // given
            var expected = new List<int> {6, 3, 0, 1};
            var crossedCells = new List<int> {1, 0, 3};
            var sut = new HintsProvider(crossedCells, _model);
            
            // when
            var actual = sut.GetHintCellsIndexes();

            // then
            actual.Should().Equal(expected);
        }
        
    }
    
    
}