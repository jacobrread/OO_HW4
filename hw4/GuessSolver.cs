using System;
namespace hw4
{
	public class GuessSolver : SolverTemplate
	{
		public GuessSolver(GameBoard board) : base(board) {}

		/// <summary>
		/// Randomly guesses values for the board
		/// </summary>
		public override bool UseStrategy(int row, int col)
		{
			// Try all possible values
			foreach (string value in Board.Characters)
			{
				// If the value is valid, use it
				if (ChecksOut(row, col, value))
				{
					Board.Board[row, col] = value;

					if (Solve(row, col + 1))
					{
						return true;
					}
				}
			}

			return false;
		}
    }
}
