using System;
namespace hw4
{
	public class GameBoard
	{
		public int BoardSize { get; }
		public string[] Characters { get; }
		public string[,] Board { get; set; }
		public string[,] ?OriginalBoard { get; }
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

			// Save the original board
			this.OriginalBoard = this.Board.Clone() as string[,];

			// Close the file
			sr.Close();
		}

		public void Reset()
		{
			this.Board = this.OriginalBoard.Clone() as string[,];
		}

		public GameBoard(int boardSize, string[] characters, string[,] board, int squareSize)
		{
			this.BoardSize = boardSize;
			this.Characters = characters;
			this.Board = board.Clone() as string[,];
			this.SquareSize = squareSize;
			this.OriginalBoard = board.Clone() as string[,];
		}

		// public GameBoard Clone()
		// {
		// 	GameBoard clone = new(this.BoardSize, this.Characters, this.Board, this.SquareSize);
		// 	return clone;
		// }

		// private string[,] CloneBoard()
		// {
		// 	string[,] clone = new string[BoardSize, BoardSize];
		// 	for (int i = 0; i < BoardSize; i++)
		// 	{
		// 		for (int j = 0; j < BoardSize; j++)
		// 		{
		// 			clone[i, j] = Board[i, j];
		// 		}
		// 	}
		// 	return clone;
		// }
	}
}

