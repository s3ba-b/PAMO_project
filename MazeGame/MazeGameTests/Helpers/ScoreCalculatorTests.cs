﻿using System;
using System.Diagnostics;
using System.Threading;
using MazeGame.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace MazeGameTests.Helpers
{
    public class ScoreCalculatorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ScoreCalculator _sut;
        public ScoreCalculatorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _sut = new ScoreCalculator();
        }
        
        
        [Fact]
        public void AreTestsWorking()
        {
            Assert.True(true);
        }
    
        [Fact]
        public void ShouldReturnCorrectScores(){
            // given
            var testTime = new Stopwatch();
            _sut.StartGame();
            testTime.Start();

            // when
            Thread.Sleep(1500);
            _sut.EndGame();
            testTime.Stop();
            var endingTestTime = DateTime.Now;
            int scores = 120000 - (int) testTime.Elapsed.TotalMilliseconds; 
            
            // then
            _testOutputHelper.WriteLine(_sut.Score.ToString());
            // 100 milliseconds is added as a range because the test is not executed exactly at the same time    
            Assert.InRange(_sut.Score, scores-100, scores);

        }
        
        [Fact]
        public void ShouldReturnZero(){
            // given
            _sut.StartGame();

            // when
            Thread.Sleep(120002);
            _sut.EndGame();

            
            // then
            _testOutputHelper.WriteLine(_sut.Score.ToString());
            Assert.Equal(0, _sut.Score);

        }

    }
    

}