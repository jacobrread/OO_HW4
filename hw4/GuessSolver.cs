using System;
namespace hw4
{
	public class GuessSolver : SolverTemplate
	{
		// Constructor
		public GuessSolver(GameBoard board) : base(board) {}

		/// <summary>
		/// Solves the board
		/// </summary>
		public override GameBoard UseStrategy(int row, int column, GameBoard board)
		{
			foreach (string value in board.Characters)
			{
				if (PerformChecks(row, column, value))
				{
					board.Board[row, column] = value;
					//Console.WriteLine("Updated a value");
				}
			}	

			return board;
		}
	}
}
