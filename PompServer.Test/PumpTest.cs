using NUnit.Framework;
using PompServer.Models;
namespace PompServer.Test;

[TestFixture]
public class PumpTest
{

    [SetUp]
    public void Setup()
    {
        
    }
    [TestCase(true)]
    [TestCase(false)]
    public void StateTest(bool value)
    {
        var pump = new Pump();
        pump.setState(value);
        Assert.AreEqual(value, pump.getState());
    }

}
