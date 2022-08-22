using NUnit.Framework;
using PompServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PompServer.Test;
public class CommandExecutorTest
{
    private CommandExecutor commandExecutor;
    private Pump pump;

    [SetUp]
    public void Setup()
    {
        pump = new Pump();
        commandExecutor = new CommandExecutor(pump);
    }


    [TestCase(1)]
    [TestCase(2)]
    public void AddTest(int id)
    {
        var command = new Command(id, true);

        commandExecutor.Add(command);
        var commandReturn = commandExecutor.GetCommand(id);

        Assert.AreEqual(command, commandReturn);
    }

    [Test]
    public void RunTest()
    {
        var id = 1;
        var value = true;
        var command = new Command(id, value);
        
        commandExecutor.Add(command);
        commandExecutor.Run(DateTime.Now);

        Assert.AreEqual(value, pump.getState());
        Assert.AreEqual(0, commandExecutor.GetCommands().Count);
        Assert.AreEqual(null, commandExecutor.GetCommand(id));
    }
}
