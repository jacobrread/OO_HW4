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
            // string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
             string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-9x9-0001.txt";
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-16x16-0001.txt";
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-25x25-0101.txt";
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-36x36-01-A001.txt";
            string solverName = "GuessSolver";
            GameBoard sudokuBoard = new(inputFilePath);

            DisplayBoard(sudokuBoard, "Starting");

            GuessSolver guessSolver = new(sudokuBoard);

            // Start the timer
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (guessSolver.Solve(0, 0)) 
            {
                Console.WriteLine("\nSolved!\n");
            } 
            else 
            {
                Console.WriteLine("\nNo solution found\n");
            }

            // Stop the timer
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                TimeSpan.FromMilliseconds(elapsedMs).Hours,
                TimeSpan.FromMilliseconds(elapsedMs).Minutes,
                TimeSpan.FromMilliseconds(elapsedMs).Seconds,
                TimeSpan.FromMilliseconds(elapsedMs).Milliseconds / 10);

            OutputSolution(sudokuBoard, elapsedTime);
        }

        /// <summary>
        /// Displays the board to the console
        /// </summary>
        /// <param name="board">The board to display</param>
        /// <param name="name">The name of the board</param>
        public static void DisplayBoard(GameBoard board, string name)
        {
            Console.WriteLine("{0} Board:\n", name);

            for (int i = 0; i < board.BoardSize; i++)
            {
                for (int j = 0; j < board.BoardSize; j++)
                {
                    Console.Write(board.Board[i, j] + " ");

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
        /// Outputs the solution to a file
        /// </summary>
        public static void OutputSolution(GameBoard board, string elapsedTime)
        {
            DisplayBoard(board, "\nFinal");

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

                sw.WriteLine("\nTotal time: {0}", elapsedTime);

                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}