using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PompServer.Models;

public class CommandExecutor
{
    private List<RepeatedCommand> commands;
    private IPump pump;
    private ILogger logger;
    private Task? task;

    public CommandExecutor(IPump pump, ILogger logger)
    {
        commands = new List<RepeatedCommand>();
        this.pump = pump;
        this.logger = logger;
    }

    public void Run(DateTime time)
    {
        lock (commands)
        {
            RepeatedCommand? finished = null;
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

    public bool Delete(Guid id)
    {
        lock (commands)
        {
            var command = GetCommand(id);
            if (command != null)
            {
                commands.Remove(command);
                return true;
            }
            return false;
        }
    }

    public void AddCommand(RepeatedCommand command)
    {
        Add(command);
        if (task == null || task.IsCompleted)
            task = StartTask();
    }
    public void Add(RepeatedCommand command) 
    { 
        lock (commands)
        {
            commands.Add(command); 
        }
    }

    public RepeatedCommand? GetCommand(Guid id)
    {
        lock (commands)
        {
            if (commands.Count != 0)
                return commands.Where((c) => c.Id == id).First();
            return null;
        }
    }

    public List<RepeatedCommand> GetCommands() { return commands; }

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

    public bool GetStatus()
    {
        return pump.GetState();
    }
}