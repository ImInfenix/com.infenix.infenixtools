using InfenixTools.DataStructures;
using NUnit.Framework;

public class GenericGridTests
{
    GenericGrid<int> grid;
    readonly int width = 10;
    readonly int height = 5;
    readonly float cellSize = 2.3f;
    readonly int initialValue = 56;

    int newValue;
    int x;
    int y;

    /*
     * Constructor Test
     */

    [Test]
    public void GridConstructorWidth()
    {
        Assert.AreEqual(width, grid.Width);
    }

    [Test]
    public void GridConstructorHeight()
    {
        Assert.AreEqual(height, grid.Height);
    }

    [Test]
    public void GridConstructorCellSize()
    {
        Assert.AreEqual(cellSize, grid.CellSize);
    }

    [Test]
    public void GridConstructorInitialized()
    {
        bool allInitToValue = true;
        for (int i = 0; i < grid.Width && allInitToValue; i++)
        {
            for (int j = 0; j < grid.Height && allInitToValue; j++)
            {
                int value = grid.Get(i, j);
                if (value != initialValue)
                    allInitToValue = false;
            }
        }
        Assert.True(allInitToValue);
    }

    /*
     * Access and modification tests
     */
    [Test]
    public void GridAccess()
    {
        Assert.AreEqual(initialValue, grid.Get(x, y));
    }

    [Test]
    public void GridAccessAndModification()
    {
        Assert.AreEqual(initialValue, grid.Get(x, y));
        grid.Set(x, y, newValue);
        Assert.AreEqual(newValue, grid.Get(x, y));
    }

    /*
     * Test setup
     */

    [SetUp]
    public void CreateGrid()
    {
        newValue = initialValue + 1;
        x = width / 2;
        y = height / 2;
        grid = new GenericGrid<int>(width, height, cellSize, initialValue);
    }

    [Test]
    public void DependentTestParametersInitialized()
    {
        Assert.AreNotEqual(width, height, "Width and height different");
        Assert.AreNotEqual(initialValue, newValue, "Values to fill");
    }
}
