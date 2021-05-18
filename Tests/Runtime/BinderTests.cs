using InfenixTools.DataStructures;
using NUnit.Framework;
using System.Collections.Generic;

public class BinderTests
{
    [Test]
    public void ConstructorTest()
    {
        Binder<int, string> binder = null;
        Assert.IsNull(binder);
        binder = new Binder<int, string>(null);
        Assert.IsNotNull(binder);
    }

    [Test]
    public void AddSingleElement()
    {
        int key = 5;
        string value = "newBind";
        Binder<int, string> binder = new Binder<int, string>(null);
        binder.AddBinding(key, value);
        Assert.IsTrue(binder.ContainsKey(key));
        Assert.IsTrue(binder.ContainsValue(value));
    }

    [Test]
    public void InitializeWithList()
    {
        int key1 = 5, key2 = 8;
        string value1 = "newBind", value2 = "otherBind";
        Assert.AreNotEqual(key1, key2);
        Assert.AreNotEqual(value1, value2);
        Bind<int, string> bind1 = new Bind<int, string>(key1, value1);
        Bind<int, string> bind2 = new Bind<int, string>(key2, value2);
        Binder<int, string> binder = new Binder<int, string>(new List<Bind<int, string>> { bind1, bind2 });
        Assert.IsTrue(binder.ContainsKey(key1));
        Assert.IsTrue(binder.ContainsKey(key2));
        Assert.IsTrue(binder.ContainsValue(value1));
        Assert.IsTrue(binder.ContainsValue(value2));
    }

    [Test]
    public void ColorBinderRandom()
    {
        ColorBinder<uint> colorBinder = new ColorBinder<uint>();
        uint key = 5;
        colorBinder.BindToRandomColor(key);
        Assert.IsTrue(colorBinder.ContainsKey(key));
        Assert.IsNotNull(colorBinder.GetValue(key));
    }
}
