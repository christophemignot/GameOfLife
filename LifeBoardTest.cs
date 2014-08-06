using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife
{
    [TestClass]
    public class LifeBoardTest
    {

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void WhenNoBoardThrowsException()
        {
            LifeBoard.GetNextGeneration(string.Empty);
        }

        [TestMethod]
        public void WhenBoardEmptyThenNoCells()
        {
            var input = @"4 4
....
....
....
....";
            var result = LifeBoard.GetNextGeneration(input);
            Assert.AreEqual(@"4 4
....
....
....
....", result);
        }

        [TestMethod]
        public void WhenOneCellThenNoCells()
        {
            var input = @"4 4
....
.*..
....
....";
            var result = LifeBoard.GetNextGeneration(input);
            Assert.AreEqual(@"4 4
....
....
....
....", result);
        }

        [TestMethod]
        public void WhenTwoCellsThenLive()
        {
            var input = @"4 4
....
.**.
....
....";
            var result = LifeBoard.GetNextGeneration(input);
            Assert.AreEqual(@"4 4
....
....
....
....", result);
        }

        [TestMethod]
        public void WhenThreeHorizontalCellsThenThreeVertical()
        {
            var input = @"4 4
....
***.
....
....";
            var result = LifeBoard.GetNextGeneration(input);
            Assert.AreEqual(@"4 4
.*..
.*..
.*..
....", result);
        }


        [TestMethod]
        public void WhenTCellsThenRectangle()
        {
            var input = @"4 4
....
***.
.*..
....";
            var result = LifeBoard.GetNextGeneration(input);
            Assert.AreEqual(@"4 4
.*..
***.
***.
....", result);
        }

        [TestMethod]
        public void CompleteBoardTest()
        {
            var input = @"4 8
........
....*...
...**...
........";
            var result = LifeBoard.GetNextGeneration(input);
            Assert.AreEqual(@"4 8
........
...**...
...**...
........", result);
        }
    }
}
