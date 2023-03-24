using System;
namespace hw4
{
	public class GameBoard
	{
		public int BoardSize { get; }
		public string[] Characters { get; }
		public string[,] Board { get; set; }
		public int SquareSize { get; }

		public GameBoard(string filePath)
		{
			// Read the input file
			StreamReader sr = new(filePath);
			string[] lines = File.ReadAllLines(filePath);
			this.BoardSize = int.Parse(lines[0]);
			this.Characters = lines[1].Split(' ');
			this.Board = new string[BoardSize, BoardSize];
			this.Board = new string[BoardSize, BoardSize];
			this.SquareSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(BoardSize)));

			// Populate the board
			for (int i = 2; i < lines.Length; i++)
			{
				if (lines[i] == "") continue;

				string[] row = lines[i].Split(' ');
				for (int j = 0; j < row.Length; j++)
				{
					Board[i - 2, j] = row[j];
				}
			}

			// Close the file
			sr.Close();
		}
	}
}

