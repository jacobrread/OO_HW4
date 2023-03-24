using hw4;

namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        string inputFilePath = "/Users/jacobread/Desktop/OO/hw4/hw4/SamplePuzzles/Input/Puzzle-4x4-0001.txt";
        string solverName = "BruteForceSolver";
        GameBoard sudokuBoard = new(inputFilePath);

        Assert.Pass();
    }
}
