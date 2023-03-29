using hw4;

namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test4x4()
    {
        string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
        GameBoard sudokuBoard = new(inputFilePath);
        GuessSolver guessSolver = new(sudokuBoard);
        bool solved = guessSolver.Solve(0, 0);
        Assert.That(new[,]
        {
            {"2", "4", "3", "1"},
            {"1", "3", "2", "4"},
            {"3", "1", "4", "2"},
            {"4", "2", "1", "3"}
        }, Is.EqualTo(sudokuBoard.Board));
    }
}
