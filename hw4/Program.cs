using System;
namespace hw4
{
    public class SudokuSolver
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
               Console.WriteLine("Usage: SudokuSolver <input file>");
               return;
            }

            // Save input arguments
            string inputFilePath = args[0];

            // File paths to good puzzles
            // /Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt
            // /Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-9x9-0001.txt
            // /Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-16x16-0001.txt
            // /Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-25x25-0101.txt
            // /Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-36x36-01-A001.txt

            // File paths to bad puzzles
            // /Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0901.txt

            GameBoard sudokuBoard1 = new(inputFilePath);

            if (!IsValidBoard(sudokuBoard1))
            {
                Console.WriteLine("Invalid board");
                BadBoardOutput(sudokuBoard1);
                return;
            }

            GameBoard sudokuBoard2 = new(inputFilePath);
            GameBoard sudokuBoard3 = new(inputFilePath);
            GameBoard sudokuBoard4 = new(inputFilePath);

            DisplayBoardToConsole(sudokuBoard1, "Starting");

            GuessSolver guessSolver = new(sudokuBoard1);
            NotesSolver notesSolver = new(sudokuBoard2);
            ReplacementSolver replacementSolver = new(sudokuBoard3);
            NotesSolverWithReplacement notesSolverWithReplacement = new(sudokuBoard4);

            SolverTemplate[] boards = { guessSolver, notesSolver, replacementSolver, notesSolverWithReplacement };

            // Create a dictionary to store the stats: {SolverName, {Solved, Time}}
            Dictionary<string, long[]> stats = new() {
                { "GuessSolver", new long[2] },
                { "NotesSolver", new long[2] },
                { "NotesSolver2", new long[2] },
                { "ReplacementSolver", new long[2] }
            };

            Parallel.ForEach(stats.Keys, solver =>
            {
                if (solver == "GuessSolver")
                {
                    stats = UseAlgorithm(guessSolver, stats);
                }
                else if (solver == "NotesSolver")
                {
                    stats = UseAlgorithm(notesSolver, stats);
                }
                else if (solver == "ReplacementSolver")
                {
                    stats = UseAlgorithm(replacementSolver, stats);
                }
                else if (solver == "NotesSolver2")
                {
                    stats = UseAlgorithm(notesSolverWithReplacement, stats);
                }
            });

            DetermineSolved(boards, stats);
        }

        /// <summary>
        /// Checks the board to see if there are any invalid characters
        /// </summary>
        /// <param name="board">The board to check</param>
        /// <returns>True if the board is valid, false if not</returns>
        public static bool IsValidBoard(GameBoard board)
        {
            for (int i = 0; i < board.BoardSize; i++)
            {
                for (int j = 0; j < board.BoardSize; j++)
                {
                    string value = board.Board[i, j];
                    if (!board.Characters.Contains(value))
                    {
                        if (!value.Equals("-")) return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Finds a solved board out of the four solving methods
        /// </summary>
        /// <param name="solvers">Array containing the four solving method objects</param>
        /// <param name="stats">Dictionary containing the stats for the four solving methods</param>
        /// <returns>True if a solved board is found, false if not</returns>
        private static bool DetermineSolved(SolverTemplate[] solvers, Dictionary<string, long[]> stats)
        {
            foreach (SolverTemplate solver in solvers)
            {
                if (IsSolved(solver.Board))
                {
                    OutputSolution(solver.Board, stats);
                    return true;
                }
            }
            OutputSolution(null, stats);
            return false;
        }

        /// <summary>
		/// Checks if the board is solved
		/// </summary>
		/// <returns>True if the board is solved, false otherwise</returns>
		private static bool IsSolved(GameBoard board) 
		{
			for (int i = 0; i < board.BoardSize; i++) {
				for (int j = 0; j < board.BoardSize; j++) {
					if (board.Board[i, j] == "-") {
						return false;
					}
				}
			}
			return true;
		}

        /// <summary>
        /// Uses the specified algorithm to solve the puzzle
        /// </summary>
        /// <param name="solver">The algorithm to use</param>
        /// <param name="stats">The dictionary to store the stats</param>
        /// <returns>The dictionary with the updated stats</returns>
        public static Dictionary<string, long[]> UseAlgorithm(SolverTemplate solver, Dictionary<string, long[]> stats)
        {
            // Start the timer
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Solve the puzzle
            bool solved = solver.Solve();

            // Stop the timer
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;

            // Save the stats
            stats[solver.Name][0] = solved ? 1 : 0;
            stats[solver.Name][1] = elapsedMs;

            return stats;
        }

        /// <summary>
        /// Displays the board to the console
        /// </summary>
        /// <param name="board">The board to display</param>
        /// <param name="name">The name of the board</param>
        public static void DisplayBoardToConsole(GameBoard board, string name)
        {
            Console.WriteLine("{0} Board:\n", name);

            for (int i = 0; i < board.BoardSize; i++)
            {
                for (int j = 0; j < board.BoardSize; j++)
                {
                    if (board.Board[i,j] != board.OriginalBoard[i,j])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    
                    Console.Write(board.Board[i, j] + " ");
                    Console.ForegroundColor = ConsoleColor.White;

                    if ((j + 1) % board.SquareSize == 0 && j != board.BoardSize - 1)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();

                if ((i + 1) % board.SquareSize == 0 && i != board.BoardSize - 1)
                {
                    for (int j = 0; j < board.BoardSize; j++)
                    {
                        Console.Write("--");
                        if ((j + 1) % board.SquareSize == 0 && j != board.BoardSize - 1)
                        {
                            Console.Write("+-");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Displays the stats to the console
        /// </summary>
        /// <param name="stats">The dictionary of stats</param>
        private static long GetTotalTime(Dictionary<string, long[]> stats)
        {
            long totalTime = 0;

            foreach (var solver in stats.Keys)
            {
                totalTime += stats[solver][1];
            }

            return totalTime;
        }

        /// <summary>
        /// Outputs the solution to a file
        /// </summary>
        /// <param name="board">The board to output</param>
        /// <param name="stats">The dictionary of stats</param>
        public static void OutputSolution(GameBoard board, Dictionary<string, long[]> stats)
        {
            DisplayBoardToConsole(board, "\nFinal");

            try
            {
                string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))));
                StreamWriter sw = new StreamWriter(Path.Combine(projectPath, "hw4/Output.txt"));

                // Add board size, characters, and original board to output file
                sw.WriteLine(board.BoardSize);
                foreach (string c in board.Characters)
                {
                    sw.Write(c + " ");
                }
                sw.WriteLine();

                for (int i = 0; i < board.BoardSize; i++)
                {
                    for (int j = 0; j < board.BoardSize; j++)
                    {
                        sw.Write(board.OriginalBoard[i, j] + " ");
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();

                if (board == null)
                {
                    sw.WriteLine("The board could not be solved");
                }
                else
                {
                    // Add solution to output file
                    sw.WriteLine("Solved");
                    for (int i = 0; i < board.BoardSize; i++)
                    {
                        for (int j = 0; j < board.BoardSize; j++)
                        {
                            sw.Write(board.Board[i, j] + " ");
                        }
                        sw.WriteLine();
                    }
                }

                // Add stats to output file
                long elapsedTime = GetTotalTime(stats);
                sw.WriteLine("\nTotal time (ms): {0}", elapsedTime);
                foreach (var solver in stats.Keys)
                {
                    sw.WriteLine("{0} Solved: {1}", solver, stats[solver][0] == 1 ? "Yes" : "No");
                    sw.WriteLine("{0} Time (ms): {1}", solver, stats[solver][1]);
                }

                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Alternate output method for bad boards
        /// </summary>
        /// <param name="board">The board to output</param>
        private static void BadBoardOutput(GameBoard board)
        {
            string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))));
                StreamWriter sw = new StreamWriter(Path.Combine(projectPath, "hw4/Output.txt"));

                // Add board size, characters, and original board to output file
                sw.WriteLine(board.BoardSize);
                foreach (string c in board.Characters)
                {
                    sw.Write(c + " ");
                }
                sw.WriteLine();

                for (int i = 0; i < board.BoardSize; i++)
                {
                    for (int j = 0; j < board.BoardSize; j++)
                    {
                        sw.Write(board.OriginalBoard[i, j] + " ");
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();
                sw.WriteLine("This board has an invalid character");
                sw.Close();
        }
    }
}