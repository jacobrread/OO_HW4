using System;
namespace hw4
{
	public class GuessSolver : SolverTemplate
	{
		public override string Name { get; }
		public GuessSolver(GameBoard board) : base(board) 
		{
			this.Name = "GuessSolver";
		}

		/// <summary>
		/// Calls the recursive solver
		/// </summary>
		/// <returns>True if the board is solved, false if it is not</returns>
		public override bool Solve()
		{
			return RecursivelySolve(0, 0);
		}

		/// <summary>
		/// Solves the board recursively
		/// </summary>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		/// <returns>True if the board is solved, false if it is not</returns>
		private bool RecursivelySolve(int row, int col) 
		{
			// Stop at the end of the board
			if (row == Board.BoardSize - 1 && col == Board.BoardSize)
			{
				return true;
			}

			// Change rows
			if (col == Board.BoardSize)
			{
				row++;
				col = 0;
			}

			// Check if the current cell is already filled
			if (Board.Board[row, col] != "-")
			{
				return RecursivelySolve(row, col + 1);
			}

			// Finally, try to solve this cell
			if (UseStrategy(row, col))
			{
				return RecursivelySolve(row, col + 1);
			}

			// If we've reached this point, we've tried all possible values and none of them worked
			return false;
		}

		/// <summary>
		/// Randomly guesses values for the board
		/// </summary>s
		/// <param name="row">The row to start at</param>
		/// <param name="col">The column to start at</param>
		/// <returns>True if it finds a value that fits, false if it does not</returns>
		private bool UseStrategy(int row, int col)
		{
			// Try all possible values
			foreach (string value in Board.Characters)
			{
				// If the value is valid, use it
				if (ChecksOut(row, col, value))
				{
					Board.Board[row, col] = value;
					if (RecursivelySolve(row, col + 1))
					{
						return true;
					}
				}
			}
			return false;
		}
    }
}
