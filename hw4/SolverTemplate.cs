using System;
namespace hw4
{
	public abstract class SolverTemplate
	{
		private int[,] board { get; }
		private int[,] solution { get; set; }

		// Constructor
		public SolverTemplate(int[,] board)
		{
			this.board = board;
			this.solution = new int[board.Length, board.Length];
		}

		/// <summary>
		/// Checks if a value is in a row
		/// </summary>
		/// <param name="row">The row to check</param>
		/// <param name="value">The value to check for</param>
		public bool CheckRow(int row, int value)
		{
			for (int i = 0; i < 9; i++)
			{
				if (board[row, i] == value)
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
		public bool CheckCol(int col, int value)
		{
			for (int i = 0; i < 9; i++)
			{
				if (board[i, col] == value)
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
		public bool CheckBox(int row, int col, int value)
		{
			int rowStart = row - row % 3;
			int colStart = col - col % 3;

			for (int i = rowStart; i < rowStart + 3; i++)
			{
				for (int j = colStart; j < colStart + 3; j++)
				{
					if (board[i, j] == value)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Solves the board
		/// </summary>
		public abstract bool Solve();
	}
}

