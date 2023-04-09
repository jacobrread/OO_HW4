using System;
namespace hw4
{
    // Trey: Template method -> Concrete class 
    public class NotesSolverWithReplacement : SolverTemplate
	{
		public override string Name { get; }
		private List<string>[,] NotesList;

		// Constructor
		public NotesSolverWithReplacement(GameBoard board) : base(board) 
		{
			this.Name = "NotesSolver2";
			NotesList = new List<string>[Board.BoardSize, Board.BoardSize];
			BuildNotes();
		}

		/// <summary>
		/// Iteratively solves the board using the notes method
		/// </summary>
		/// <returns>True if the board is solved, false if it is not</returns>
		public override bool Solve() 
		{
			return RecursivelySolve(Board.Board);
		}

		/// <summary>
		/// Solves the board recursively
		/// </summary>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		/// <returns>True if the board is solved, false if it is not</returns>
		private bool RecursivelySolve(string[,] currentBoard) 
		{
			int row = -1;
			int col = -1;
			bool isEmpty = false;

			// Find empty cells
			for (int i = 0; i < Board.BoardSize; i++) 
			{
				for (int j = 0; j < Board.BoardSize; j++) 
				{
					if (currentBoard[i, j] == "-") 
					{
						row = i;
						col = j;
	
						isEmpty = true;
						break;
					}
				}

				if (isEmpty) 
				{
					break;
				}
			}

			// Check to see if board is complete
			if (!isEmpty) 
			{
				return true;
			}

			// Use note list to solve
			foreach (string value in NotesList[row, col])
			{
				// If the value is valid, use it
				if (ChecksOut(row, col, value))
				{
					currentBoard[row, col] = value;
					if (RecursivelySolve(currentBoard))
					{
						return true;
					}
					else 
					{
						// Mark the cell as empty so the algorithm will return to it
						currentBoard[row, col] = "-";
					}
				}
			}
        	return false;
		}

		/// <summary>
		/// Overloaded method to allow for the board to be passed into the method. It still checks the row, column, and box to see if it is safe.
		/// </summary>
		/// <param name="currentBoard">The current board</param>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		/// <param name="value">The value to check</param>
		/// <returns>True if it is safe, false if it is not</returns>
		private bool ChecksOut(string[,] currentBoard, int row, int col, string value)
		{
			bool rowCheck = CheckRow(currentBoard, row, col, value);
			bool colCheck = CheckCol(currentBoard, row, col, value);
			bool boxCheck = CheckBox(currentBoard, row, col, value);

			return rowCheck && colCheck && boxCheck;
		}

       /// <summary>
		/// Checks the row to see if the value is safe - overloaded method to allow for the board to be passed in
		/// </summary>
		/// <param name="currentBoard">The current board</param>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		/// <param name="value">The value to check</param>
		/// <returns>True if it is safe, false if it is not</returns>
		private bool CheckRow(string[,] currentBoard, int row, int col, string value)
		{
			for (int i = 0; i < Board.BoardSize; i++) 
			{
				if (currentBoard[row, i] == value) 
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Checks the column to see if the value is safe - overloaded method to allow for the board to be passed in
		/// </summary>
		/// <param name="currentBoard">The current board</param>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		/// <param name="value">The value to check</param>
		/// <returns>True if it is safe, false if it is not</returns>
		private bool CheckCol(string[,] currentBoard, int row, int col, string value)
		{
			for (int i = 0; i < Board.BoardSize; i++)
			{
				if (currentBoard[i, col] == value)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Checks the box to see if the value is safe - overloaded method to allow for the board to be passed in
		/// </summary>
		/// <param name="currentBoard">The current board</param>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		/// <param name="value">The value to check</param>
		/// <returns>True if it is safe, false if it is not</returns>
		private bool CheckBox(string[,] currentBoard, int row, int col, string value)
		{
			int rowStart = row - row % Board.SquareSize;
			int colStart = col - col % Board.SquareSize;

			for (int i = 0; i < Board.SquareSize; i++)
			{
				for (int j = 0; j < Board.SquareSize; j++)
				{
					if (currentBoard[i + rowStart, j + colStart] == value)
					{
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
		}
	}
}