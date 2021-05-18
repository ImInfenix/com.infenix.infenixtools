using InfenixTools.DataStructures;
using NUnit.Framework;

public class IdGeneratorTests
{
    [Test]
    public void NoArgumentConstructor()
    {
        IdGenerator idGen = new IdGenerator();
        Assert.AreEqual(0, idGen.Current);
    }

    [Test]
    public void ArgumentConstructor()
    {
        uint initValue = 89;
        IdGenerator idGen = new IdGenerator(initValue);
        Assert.AreEqual(initValue, idGen.Current);
    }

    [Test]
    public void IncrementValueOnNext()
    {
        uint initValue = 89;
        IdGenerator idGen = new IdGenerator(initValue);
        Assert.AreEqual(initValue, idGen.Current);
        Assert.AreEqual(initValue + 1, idGen.Next);
        Assert.AreEqual(initValue + 1, idGen.Current);
    }

    [Test]
    public void MulitpleIncrement()
    {
        uint initValue = 89;
        uint incrementsCount = 5;
        IdGenerator idGen = new IdGenerator(initValue);
        Assert.AreEqual(initValue, idGen.Current);
        for (uint i = 1; i <= incrementsCount; i++)
            Assert.AreEqual(initValue + i, idGen.Next);
    }
}
