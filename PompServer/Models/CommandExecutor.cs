using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PompServer.Models;

public class CommandExecutor
{
    private List<Command> commands;
    private List<RepeatedCommand> repeatedCommands;
    private IPump pump;
    private ILogger logger;
    private Task? task;

    public CommandExecutor(IPump pump, ILogger logger)
    {
        commands = new List<Command>();
        repeatedCommands = new List<RepeatedCommand>();
        this.pump = pump;
        this.logger = logger;
    }

    public void Execute(DateTime time, List<Command> commands)
    {
        lock (commands)
        {
            Command? finished = null;
            foreach (var command in commands)
            {
                if (command.ShouldExecute(time))
                {
                    logger.LogInformation(command.ToString());
                    var action = command.Execute();
                    pump.SetState(action);
                    if (command.IsDone())
                        finished = command;
                    break;
                }
            }
            if (finished != null)
                commands.Remove(finished);
        }
    }

    public void Run(DateTime time)
    {
        Execute(time, commands);
        Execute(time, repeatedCommands.Cast<Command>().ToList());
    }

    public void Delete(int id)
    {
        lock (commands)
        {
            var command = GetCommand(id);
            if (command != null)
                commands.Remove(command);
        }
    }

    public void AddCommand(Command command)
    {
        Add(command);
        if (task == null || task.IsCompleted)
            task = StartTask();
    }
    public void Add(Command command) 
    { 
        lock (commands)
        {
            commands.Add(command); 
        }
    }

    public Command? GetCommand(int id)
    {
        lock (commands)
        {
            if (commands.Count != 0)
                return commands.Where((c) => c.Id == id).First();
            return null;
        }
    }

    public List<Command> GetCommands() { return commands; }

    public void Clear()
    {
        lock(commands)
        {
            commands.Clear();
        }
    }

    public void ExecutingLoop()
    {
        while (!IsDone())
        {
            Run(DateTime.Now);
            Thread.Sleep(200);
        }
    }

    public bool IsDone()
    {
        lock (commands)
        {
            return commands.Count == 0;
        }
    }

    public Task StartTask()
    {
        var task = Task.Run(() =>{ ExecutingLoop(); });
        return task;
    }
}