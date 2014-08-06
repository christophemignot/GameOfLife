using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class LifeBoard
    {
        private LifeBoard()
        { }

        public static string GetNextGeneration(string board)
        {
            if (string.IsNullOrEmpty(board))
                throw new ArgumentNullException("board");
            LifeBoard gameBoard = new LifeBoard();
            gameBoard.Parse(board);
            gameBoard.ComputeNextGeneration();
            return gameBoard.ToString();
        }

        private List<Cell> Cells;
        private List<Cell> NextGenerationCells;
        private int RowsCount;
        private int ColumnsCount;

        private void Parse(string board)
        {
            var rows = board.Replace(Environment.NewLine, "|").Split('|');
            var boardDefinition = rows[0].Split(' ');
            RowsCount = int.Parse(boardDefinition[0]);
            ColumnsCount = int.Parse(boardDefinition[1]);
            Cells = new List<Cell>();
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    var cell = new Cell();
                    cell.Row = i;
                    cell.Column = j;
                    cell.IsAlive = (rows[i + 1][j] == '*');
                    Cells.Add(cell);
                }
            }
        }

        private void ComputeNextGeneration()
        {
            NextGenerationCells = new List<Cell>();
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    Cell theCell = Cells.Find(cell => cell.Row == i && cell.Column == j);
                    Cell nextGenCell = (Cell)theCell.Clone();
                    if (theCell.IsAlive)
                    {
                        var neibourgCount = GetNeibourgCount(i, j);
                        nextGenCell.IsAlive = (neibourgCount == 2 || neibourgCount == 3);
                    }
                    else
                    {
                        var neibourgCount = GetNeibourgCount(i, j);
                        nextGenCell.IsAlive = neibourgCount == 3;
                    }
                    NextGenerationCells.Add(nextGenCell);
                }
            }
        }

        private int GetNeibourgCount(int row, int col)
        {
            int result = 0;
            result += OneIfLivingCell(row - 1, col - 1);
            result += OneIfLivingCell(row - 1, col);
            result += OneIfLivingCell(row - 1, col + 1);
            result += OneIfLivingCell(row, col - 1);
            result += OneIfLivingCell(row, col + 1);
            result += OneIfLivingCell(row + 1, col - 1);
            result += OneIfLivingCell(row + 1, col);
            result += OneIfLivingCell(row + 1, col + 1);
            return result;
        }

        private int OneIfLivingCell(int row, int col)
        {
            var theCell = Cells.FirstOrDefault(cell => cell.Row == row && cell.Column == col);
            if (theCell == null)
                return 0;
            if (theCell.IsAlive)
                return 1;
            return 0;
        }


        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0} {1}", this.RowsCount, this.ColumnsCount);
            result.Append(Environment.NewLine);
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    result.Append(NextGenerationCells.Find(cell => cell.Row == i && cell.Column == j).ToString());
                }
                if (i != RowsCount-1)
                    result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        private class Cell : ICloneable
        {
            public int Column { get; set; }
            public int Row { get; set; }
            public bool IsAlive { get; set; }
            public override string ToString()
            {
                if (this.IsAlive)
                {
                    return "*";
                }
                return ".";
            }

            public object Clone()
            {
                Cell result = new Cell();
                result.Column = this.Column;
                result.Row = this.Row;
                result.IsAlive = this.IsAlive;
                return result;
            }
        }


    }
}
