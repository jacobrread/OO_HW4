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
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-9x9-0103.txt";
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-16x16-0002.txt";
            //string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-25x25-0902.txt";
            string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-36x36-01-C001.txt";
            string solverName = "GuessSolver";
            GameBoard sudokuBoard = new(inputFilePath);

            DisplayBoard(sudokuBoard, "Starting");

            GuessSolver guessSolver = new(sudokuBoard);
            
            // Start the timer
            var watch = System.Diagnostics.Stopwatch.StartNew();
            GameBoard solvedBoard = guessSolver.Solve();
            // Stop the timer
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                TimeSpan.FromMilliseconds(elapsedMs).Hours,
                TimeSpan.FromMilliseconds(elapsedMs).Minutes,
                TimeSpan.FromMilliseconds(elapsedMs).Seconds,
                TimeSpan.FromMilliseconds(elapsedMs).Milliseconds / 10);

            OutputSolution(solvedBoard, false, elapsedTime);
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
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Outputs the solution to a file
        /// </summary>
        public static void OutputSolution(GameBoard board, bool displayToConsole, string elapsedTime)
        {
            if (displayToConsole)
            {
                DisplayBoard(board, "\nFinal");
            }
            else
            {
                try
                {
                    //// TODO: Make this output location dynamic
                    StreamWriter sw = new StreamWriter("/Users/jacobread/Desktop/OO/hw4/hw4/Output.txt");
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
}