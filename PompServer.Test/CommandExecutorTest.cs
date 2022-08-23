using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NSubstitute;
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
    private IPump pump;

    [SetUp]
    public void Setup()
    {
        pump = Substitute.For<IPump>();

        commandExecutor = new CommandExecutor(
            pump, 
            Substitute.For<ILogger>()
        );
    }


    [TestCase(1)]
    [TestCase(2)]
    public void AddTest(int id)
    {
        var command = new BasicCommand(id, true);

        commandExecutor.Add(command);
        var commandReturn = commandExecutor.GetCommand(id);

        Assert.AreEqual(command, commandReturn);
    }

    [Test]
    public void RunTest()
    {
        var id = 1;
        var value = true;
        var command = new BasicCommand(id, value);
        
        commandExecutor.Add(command);
        commandExecutor.Run(DateTime.Now);

        pump.Received<IPump>().SetState(value);
        Assert.AreEqual(0, commandExecutor.GetCommands().Count);
        Assert.AreEqual(null, commandExecutor.GetCommand(id));
    }
    [Test]
    public void IsDoneTest()
    {
        var id = 1;
        var value = true;
        var command = new BasicCommand(id, value);
        commandExecutor.Add(command);

        var result = commandExecutor.IsDone();
        commandExecutor.Run(DateTime.Now);
        var result2 = commandExecutor.IsDone();

        Assert.AreEqual(false, result);
        Assert.AreEqual(true, result2);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void ExecutingLoopTest(int number)
    {
        for(int i = 0; i < number; i++)
        {
            var id = i;
            var value = true;
            var command = new BasicCommand(id, value);
            commandExecutor.Add(command);
        }
        
        commandExecutor.ExecutingLoop();

        Assert.AreEqual(0, commandExecutor.GetCommands().Count);
    }

    [Test]
    public void StartTaskTest()
    {
        var id = 1;
        var value = true;
        var command = new BasicCommand(id, value);
        commandExecutor.Add(command);

        var task = commandExecutor.StartTask();
        task.Wait();

        pump.Received<IPump>().SetState(true);
        Assert.AreEqual(0, commandExecutor.GetCommands().Count);
    }
}
