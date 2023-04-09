using hw4;

namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestGuess4x4()
    {
        string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
        GameBoard sudokuBoard = new(inputFilePath);
        GuessSolver guessSolver = new(sudokuBoard);
        bool solved = guessSolver.Solve();
        Assert.That(new[,]
        {
            {"2", "4", "3", "1"},
            {"1", "3", "2", "4"},
            {"3", "1", "4", "2"},
            {"4", "2", "1", "3"}
        }, Is.EqualTo(sudokuBoard.Board));
    }

    [Test]
    public void TestGuess9x9()
    {
        string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-9x9-0001.txt";
        GameBoard sudokuBoard = new(inputFilePath);
        GuessSolver guessSolver = new(sudokuBoard);
        bool solved = guessSolver.Solve();
        Assert.That(new[,]
        {
            {"4", "9", "2", "1", "3", "6", "7", "5", "8",},
            {"7", "6", "3", "5", "4", "8", "1", "9", "2",},
            {"5", "1", "8", "7", "2", "9", "3", "6", "4",},
            {"8", "2", "5", "3", "1", "7", "9", "4", "6",},
            {"6", "7", "4", "8", "9", "5", "2", "1", "3",},
            {"9", "3", "1", "2", "6", "4", "5", "8", "7",},
            {"1", "8", "6", "9", "7", "3", "4", "2", "5",},
            {"2", "4", "7", "6", "5", "1", "8", "3", "9",},
            {"3", "5", "9", "4", "8", "2", "6", "7", "1",}
        }, Is.EqualTo(sudokuBoard.Board));
    }

    [Test]
    public void TestFindSingles() 
    {
        string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
        GameBoard sudokuBoard = new(inputFilePath);
        NotesSolver notesSolver = new(sudokuBoard);
        bool[] singles = notesSolver.FindSingles(0);
        Assert.That(singles, Is.EqualTo(new[] {false, true, false, false}));
    }

    [Test]
    public void TestNotes() 
    {
        string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
        GameBoard sudokuBoard = new(inputFilePath);
        NotesSolver notesSolver = new(sudokuBoard);
        Assert.That(notesSolver.NotesList[0,1], Is.EqualTo(new[] {"4"}));
        Assert.That(notesSolver.NotesList[1,2], Is.EqualTo(new[] {"2"}));
        Assert.That(notesSolver.NotesList[2,3], Is.EqualTo(new[] {"2"}));
        Assert.That(notesSolver.NotesList[3,0], Is.EqualTo(new[] {"4"}));
    }

    [Test]
    public void TestCheckRow()
    {
        string[,] board = new[,]
        {
            {"2", "-", "3", "1"},
            {"1", "3", "-", "4"},
            {"3", "1", "4", "-"},
            {"-", "2", "1", "3"}
        };
        string[] values = {"1", "2", "3", "4"};

        GameBoard sudokuBoard = new(4, values, board, 2);
        GuessSolver solver = new(sudokuBoard);
        bool isRowSafe = solver.CheckRow(0, "4");
        Assert.That(isRowSafe, Is.EqualTo(true));
    }

    [Test]
    public void TestCheckColumn() 
    {
        string[,] board = new[,]
        {
            {"2", "-", "3", "1"},
            {"1", "3", "-", "4"},
            {"3", "1", "4", "-"},
            {"-", "2", "1", "3"}
        };
        string[] values = {"1", "2", "3", "4"};

        GameBoard sudokuBoard = new(4, values, board, 2);
        GuessSolver solver = new(sudokuBoard);
        bool isRowSafe = solver.CheckCol(0, "4");
        Assert.That(isRowSafe, Is.EqualTo(true));
    }

    [Test]
    public void TestCheckSquare()
    {
        string[,] board = new[,]
        {
            {"2", "-", "3", "1"},
            {"1", "3", "-", "4"},
            {"3", "1", "4", "-"},
            {"-", "2", "1", "3"}
        };
        string[] values = {"1", "2", "3", "4"};

        GameBoard sudokuBoard = new(4, values, board, 2);
        GuessSolver solver = new(sudokuBoard);
        bool isRowSafe = solver.CheckBox(0, 0, "4");
        Assert.That(isRowSafe, Is.EqualTo(true));
    }

    [Test]
    public void TestIsValidBoard()
    {
        string[,] board = new[,]
        {
            {"2", "-", "3", "1"},
            {"1", "9", "-", "4"},
            {"3", "1", "4", "-"},
            {"-", "2", "1", "3"}
        };
        string[] values = {"1", "2", "3", "4"};

        GameBoard sudokuBoard = new(4, values, board, 2);
        bool isValid = SudokuSolver.IsValidBoard(sudokuBoard);
        Assert.That(isValid, Is.EqualTo(false));
    }
}
