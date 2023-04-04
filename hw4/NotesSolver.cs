using System;
namespace hw4
{
	public class NotesSolver : SolverTemplate
	{
		public override string Name { get; }
		List<string>[,] notesList;

		public NotesSolver(GameBoard board) : base(board) 
		{
			this.Name = "NotesSolver";
			notesList = new List<string>[Board.BoardSize, Board.BoardSize];
			BuildNotes();
		}

		/// <summary>
		/// Solves the board recursively
		/// </summary>
		// public override bool Solve() 
		// {
		// 	for (int i = 0; i < Board.BoardSize; i++) // row
		// 	{
		// 		int[] alreadyChecked = FindSingles(i);

		// 		// Solve the 'singles' first
		// 		for (int j = 0; j < Board.BoardSize; j++)
		// 		{
		// 			if (alreadyChecked[j] == 1) UseStrategy(i, j);
		// 		}

		// 		// Solve the rest of the board
		// 		for (int j = 0; j < Board.BoardSize; j++)
		// 		{
		// 			if (alreadyChecked[j] == 0) UseStrategy(i, j);
		// 		}
		// 	}

		// 	return IsSolved();
		// }

		public override bool Solve()
		{
			return RecursivelySolve(0, 0);
		}

		private bool RecursivelySolve(int row, int col) 
		{
			// If we've reached the end of the board, we're done
			if (row == Board.BoardSize - 1 && col == Board.BoardSize)
			{
				return true;
			}

			// If we've reached the end of the row, move to the next row
			if (col == Board.BoardSize)
			{
				row++;
				col = 0;
			}

			// If the current cell is already filled, move to the next cell
			if (Board.Board[row, col] != "-")
			{
				return RecursivelySolve(row, col + 1);
			}

			if (UseStrategy(row, col))
			{
				return RecursivelySolve(row, col + 1);
			}

			// If we've reached this point, we've tried all possible values and none of them worked
			return false;
		}

		public bool UseStrategy(int row, int col)
		{
			foreach (string value in notesList[row, col])
			{
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

		/// <summary>
		/// Finds all cells in the current row that only have one possible solution
		/// </summary>
		/// <param name="row">The row to check</param>
		private int[] FindSingles(int row)
		{
			int[] hasSingle = new int[Board.BoardSize];
			for (int i = 0; i < Board.BoardSize; i++)
			{
				if (notesList[row, i].Count == 1)
				{
					hasSingle[i] = 1;
				}
				else
				{
					hasSingle[i] = 0;
				}
			}
			return hasSingle;
		}

		/// <summary>
		/// Checks if the board is solved
		/// </summary>
		private bool IsSolved() 
		{
			for (int i = 0; i < Board.BoardSize; i++) {
				for (int j = 0; j < Board.BoardSize; j++) {
					if (Board.Board[i, j] == "-") {
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Solves the board using the notes strategy
		/// </summary>
		/// <param name="row">The row to start at</param>
		/// <param name="col">The column to start at</param>
		// private void UseStrategy(int row, int col)
		// {
		// 	// Try all values in the notes list
		// 	foreach (string value in notesList[row, col])
		// 	{
		// 		// If the value is valid, use it
		// 		if (ChecksOut(row, col, value))
		// 		{
		// 			Board.Board[row, col] = value;
		// 		}
		// 	}
		// }
		
		/// <summary>
		/// Builds a list of possible values for each cell
		/// </summary>
		private void BuildNotes()
		{
			for (int row = 0; row < Board.BoardSize; row++) 
			{
				for (int col = 0; col < Board.BoardSize; col++) 
				{
					notesList[row, col] = new List<string>();
				}
			}

			for (int row = 0; row < Board.BoardSize; row++)
			{
				for (int col = 0; col < Board.BoardSize; col++)
				{
					if (Board.Board[row, col] == "-")
					{
						foreach (string value in Board.Characters)
						{
							if (ChecksOut(row, col, value))
							{
								notesList[row, col].Add(value);
							}
						}
					}
				}
			}

			//// Print notes
			//for (int row = 0; row < Board.BoardSize; row++)
			//{
			//	for (int col = 0; col < Board.BoardSize; col++)
			//	{
			//		Console.Write("[{0},{1}]: ", row, col);
			//		foreach (string value in notesList[row, col])
			//		{
			//			Console.Write(value + " ");
			//		}
			//		Console.WriteLine();
			//	}
			//	Console.WriteLine();
			//}
		}
	}
}

