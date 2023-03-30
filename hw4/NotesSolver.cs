using System;
namespace hw4
{
	public class NotesSolver : SolverTemplate
	{
		List<string>[,] notesList;

		public NotesSolver(GameBoard board) : base(board) 
		{
			notesList = new List<string>[Board.BoardSize, Board.BoardSize];
			BuildNotes();
		}

		/// <summary>
		/// Solves the board using the notes strategy
		/// </summary>
		/// <param name="row">The row to start at</param>
		/// <param name="col">The column to start at</param>
		public override bool UseStrategy(int row, int col)
		{
			foreach (string value in notesList[row, col])
			{
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

		/// <summary>
		/// Builds a list of possible values for each cell
		/// </summary>
		private void BuildNotes()
		{
			for (int row = 0; row < Board.BoardSize; row++) {
				for (int col = 0; col < Board.BoardSize; col++) {
					notesList[row, col] = new List<string>();
				}
			}

			for (int row = 0; row < Board.BoardSize; row++) {
				for (int col = 0; col < Board.BoardSize; col++) {
					if (Board.Board[row, col] == "-") {
						foreach (string value in Board.Characters) {
							if (ChecksOut(row, col, value)) {
								notesList[row, col].Add(value);
							}
						}
					}
				}
			}
		}
	}
}

