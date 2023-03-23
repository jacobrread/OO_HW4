using System.IO;

namespace hw4
{
    class SudokuSolver
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: SudokuSolver <input file> <solver>");
                Console.WriteLine("Solvers: BruteForceSolver, BacktrackingSolver, DancingLinksSolver");
                return;
            }

            string inputFileName = args[0];
            string solverName = args[1];

            // Read the input file
            String? line;
            try
            {
                StreamReader sr = new StreamReader(inputFileName);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            // TODO: create appropriate board
            // int[,] board = new int[9, 9];

            // TODO: create new solver based on the solverName argument
        }

        /// <summary>
        /// Outputs the solution to a file
        /// </summary>
        public static void OutputSolution()
        {
            try
            {
                // TODO: Make this output location dynamic
                StreamWriter sw = new StreamWriter("C/Users/jacobread/Desktop/OO/hw4/hw4/Output/output.txt");
                //Write a line of text
                sw.WriteLine("Hello World!!");
                //Write a second line of text
                sw.WriteLine("From the StreamWriter class");
                //Close the file
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}