using System;
namespace hw4
{
	public class ReplacementSolver : SolverTemplate
	{
		public override string Name { get; }
		public ReplacementSolver(GameBoard board) : base(board) 
		{
			this.Name = "ReplacementSolver";
		}

		/// <summary>
		/// Calls the recursive solver
		/// </summary>
		public override bool Solve()
		{
			return RecursivelySolve(Board.Board);
		}

		/// <summary>
		/// Solves the board recursively
		/// </summary>
		/// <param name="row">The current row</param>
		/// <param name="col">The current column</param>
		private bool RecursivelySolve(string[,] currentBoard) 
		{
			int row = -1;
			int col = -1;
			bool isEmpty = true;
			for (int i = 0; i < Board.BoardSize; i++) 
			{
				for (int j = 0; j < Board.BoardSize; j++) 
				{
					if (currentBoard[i, j] == "-") 
					{
						row = i;
						col = j;
	
						isEmpty = false;
						break;
					}
				}

				if (!isEmpty) 
				{
					break;
				}
			}
	
			if (isEmpty) 
			{
				return true;
			}
	
			foreach (string value in Board.Characters) 
			{
				if (ChecksOut(currentBoard, row, col, value)) 
				{
					currentBoard[row, col] = value;
					if (RecursivelySolve(currentBoard)) 
					{	
						return true;
					}
					else 
					{
						currentBoard[row, col] = "-";
					}
				}
			}
        	return false;
		}

		private bool ChecksOut(string[,] currentBoard, int row, int col, string value)
		{
			bool rowCheck = CheckRow(currentBoard, row, col, value);
			bool colCheck = CheckCol(currentBoard, row, col, value);
			bool boxCheck = CheckBox(currentBoard, row, col, value);

			return rowCheck && colCheck && boxCheck;
		}

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
    }
}
