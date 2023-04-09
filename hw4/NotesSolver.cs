using System;
namespace hw4
{
    // Trey: Template method -> Concrete class 
    public class NotesSolver : SolverTemplate
	{
		public override string Name { get; }
		public List<string>[,] NotesList;

		// Constructor
		public NotesSolver(GameBoard board) : base(board) 
		{
			this.Name = "NotesSolver";
			NotesList = new List<string>[Board.BoardSize, Board.BoardSize];
			BuildNotes();
		}

		/// <summary>
		/// Iteratively solves the board using the notes method
		/// </summary>
		/// <returns>True if the board is solved, false if it is not</returns>
		public override bool Solve() 
		{
			for (int i = 0; i < Board.BoardSize; i++) // row
			{
				bool[] singles = FindSingles(i);

				// Solve the 'singles' first
				for (int j = 0; j < Board.BoardSize; j++)
				{
					if (singles[j]) UseStrategy(i, j);
				}

				// Solve the rest of the board
				for (int j = 0; j < Board.BoardSize; j++)
				{
					if (!singles[j]) UseStrategy(i, j);
				}
			}
			return IsSolved();
		}

		/// <summary>
		/// Solves the board using the notes strategy
		/// </summary>
		/// <param name="row">The row to start at</param>
		/// <param name="col">The column to start at</param>
		private void UseStrategy(int row, int col)
		{
			// Try all values in the notes list
			foreach (string value in NotesList[row, col])
			{
				// If the value is valid, use it
				if (ChecksOut(row, col, value))
				{
					Board.Board[row, col] = value;
				}
			}
		}

		/// <summary>
		/// Finds all cells in the current row that only have one possible solution
		/// </summary>
		/// <param name="row">The row to check</param>
		/// <returns>An boolean array where true means the cell has only one possible solution</returns>
		public bool[] FindSingles(int row)
		{
			bool[] hasSingle = new bool[Board.BoardSize];
			for (int i = 0; i < Board.BoardSize; i++)
			{
				if (NotesList[row, i].Count == 1)
				{
					hasSingle[i] = true;
				}
				else
				{
					hasSingle[i] = false;
				}
			}
			return hasSingle;
		}

		/// <summary>
		/// Checks if the board is solved
		/// </summary>
		/// <returns>True if the board is solved, false otherwise</returns>
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
		/// Builds a list of possible values for each cell
		/// </summary>
		private void BuildNotes()
		{
			for (int row = 0; row < Board.BoardSize; row++) 
			{
				for (int col = 0; col < Board.BoardSize; col++) 
				{
					NotesList[row, col] = new List<string>();
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
								NotesList[row, col].Add(value);
							}
						}
					}
				}
			}

			//PrintNotes();
		}

		/// <summary>
		/// Prints the notes list
		/// </summary>
		private void PrintNotes()
		{
			for (int row = 0; row < Board.BoardSize; row++)
			{
				for (int col = 0; col < Board.BoardSize; col++)
				{
					Console.Write("[{0},{1}]: ", row, col);
					foreach (string value in NotesList[row, col])
					{
						Console.Write(value + " ");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}
		}
	}
}

