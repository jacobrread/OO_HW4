using System;
namespace hw4
{
    class SudokuSolver
    {
        static void Main(string[] args)
        {
            //if (args.Length != 3)
            //{
            //    Console.WriteLine("Usage: SudokuSolver <input file> <solver>");
            //    Console.WriteLine("Solvers: GuessSolver, BacktrackingSolver, DancingLinksSolver");
            //    Console.WriteLine("Display board: true/false");
            //    return;
            //}

            //// Save input arguments
            //string inputFilePath = args[0];
            //string solverName = args[1];

            //if (args[2] == "true") DisplayBoard(sudokuBoard);

            // TODO: Remove this once you are ready to test your command line arguments
             //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
             //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-9x9-0001.txt";
             //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-16x16-0001.txt";
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-25x25-0101.txt";
            string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-36x36-01-A001.txt";
            GameBoard sudokuBoard = new(inputFilePath);

            DisplayBoardToConsole(sudokuBoard, "Starting");

            GuessSolver guessSolver = new(sudokuBoard);
            NotesSolver notesSolver = new(sudokuBoard);
            ReplacementSolver replacementSolver = new(sudokuBoard);

            // Create a dictionary to store the stats: {SolverName, {Solved, Time}}
            Dictionary<string, long[]> stats = new() {
                { "GuessSolver", new long[2] },
                { "NotesSolver", new long[2] },
                { "ReplacementSolver", new long[2] }
            };

            foreach (var solver in stats.Keys)
            {
                if (solver == "GuessSolver")
                {
                    Console.WriteLine("Spinning up GuessSolver...");
                    stats = UseAlgorithm(guessSolver, stats);
                }
                else if (solver == "NotesSolver")
                {
                    Console.WriteLine("Spinning up NotesSolver...");
                    notesSolver.Board.Reset();
                    Console.WriteLine("Reset NotesSolver board");
                    stats = UseAlgorithm(notesSolver, stats);
                }
                else if (solver == "ReplacementSolver")
                {
                    Console.WriteLine("Spinning up ReplacementSolver...");
                    replacementSolver.Board.Reset();
                    Console.WriteLine("Reset ReplacementSolver board");
                    stats = UseAlgorithm(replacementSolver, stats);
                }
            }

            OutputSolution(sudokuBoard, stats);
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
                //// TODO: Make this output location dynamic
                StreamWriter sw = new StreamWriter("/Users/jacobread/Desktop/OO/hw4/hw4/Output.txt");

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
    }
}