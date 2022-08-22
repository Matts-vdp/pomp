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
    public void AddTest(int id)
    {
        var command = new Command(id, true);

        commandExecutor.Add(command);
        var commandReturn = commandExecutor.GetCommand(id);

        Assert.AreEqual(command, commandReturn);
    }

}
