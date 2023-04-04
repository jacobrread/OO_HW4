using System;
namespace hw4
{
	public abstract class SolverTemplate
	{
		public GameBoard Board { get; }
		public abstract string Name { get; }

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

			for (int i = 0; i < Board.SquareSize; i++)
			{
				for (int j = 0; j < Board.SquareSize; j++)
				{
					if (Board.Board[i + rowStart, j + colStart] == value)
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
		public bool ChecksOut(int row, int col, string value)
		{
			bool inRow = CheckRow(row, value);
			bool inCol = CheckCol(col, value);
			bool inBox = CheckBox(row, col, value);

			return inRow && inCol && inBox;
		}

		/// <summary>
		/// Solves the board recursively
		/// </summary>
		// public abstract bool Solve();
		public abstract bool Solve();
	}
}
