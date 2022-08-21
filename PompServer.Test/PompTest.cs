using NUnit.Framework;
using PompServer.Models;
namespace PompServer.Test;

[TestFixture]
public class PompTest
{

    [SetUp]
    public void Setup()
    {
        
    }
    [TestCase(true)]
    [TestCase(false)]
    public void StateTest(bool value)
    {
        var pomp = new Pomp();
        pomp.setState(value);
        Assert.AreEqual(value, pomp.getState());
    }

}
