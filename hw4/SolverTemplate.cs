using System;
namespace hw4
{
	public abstract class SolverTemplate
	{
		public GameBoard Board { get; }

		// Constructor
		public SolverTemplate(GameBoard board)
		{
			this.Board = board;
		}

		/// <summary>
		/// Checks if a value is in a row
		/// </summary>
		/// <param name="row">The row to check</param>
		/// <param name="value">The value to check for</param>
		public bool CheckRow(int row, string value)
		{
			for (int i = 0; i < Board.BoardSize; i++)
			{
				if (Board.Board[row, i] == value)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Checks if a value is in a column
		/// </summary>
		/// <param name="col">The column to check</param>
		/// <param name="value">The value to check for</param>
		public bool CheckCol(int col, string value)
		{
			for (int i = 0; i < Board.BoardSize; i++)
			{
				if (Board.Board[i, col] == value)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Checks if a value is in a 3x3 box
		/// </summary>
		/// <param name="row">The row to check</param>
		/// <param name="col">The column to check</param>
		/// <param name="value">The value to check for</param>
		public bool CheckBox(int row, int col, string value)
		{
			int rowStart = row - row % Board.SquareSize;
			int colStart = col - col % Board.SquareSize;

			for (int i = rowStart; i < rowStart; i++)
			{
				for (int j = colStart; j < colStart; j++)
				{
					if (Board.Board[i, j] == value)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Checks if a value is in a row, column, or box
		/// </summary>
		public bool PerformChecks(int row, int col, string value)
		{
			bool inRow = CheckRow(row, value);
			bool inCol = CheckCol(col, value);
			bool inBox = CheckBox(row, col, value);

			return inRow && inCol && inBox;
		}

		/// <summary>
		/// Solves the board
		/// </summary>
		public GameBoard Solve() 
		{
			GameBoard solvedBoard = Board;
			try {
				for (int i = 0; i < solvedBoard.BoardSize; i++)
				{
					for (int j = 0; j < solvedBoard.BoardSize; j++)
					{
						if (solvedBoard.Board[i, j] == "-")
						{
							solvedBoard = UseStrategy(i, j, solvedBoard);
						}
						else
						{
							continue;
						}
					}
				}

				return solvedBoard;
			}
			catch
			{
				Console.WriteLine("Error: Could not solve sudoku board");
				return Board;
			}
		}

		/// <summary>
		///	Uses the specific strategy implemented by the child class
		/// </summary>
		/// <param name="row">The row to check</param>
		/// <param name="column">The column to check</param>
		public abstract GameBoard UseStrategy(int row, int column, GameBoard board);
	}
}
