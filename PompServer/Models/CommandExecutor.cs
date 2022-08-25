using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using PompServer.Hubs;
using System;

namespace PompServer.Models;

public class CommandExecutor
{
    private List<RepeatedCommand> commands;
    private IPump pump;
    private ILogger logger;
    private Task? task;
    private IHubContext<UpdateHub> updateHub;

    public CommandExecutor(IPump pump, ILogger logger, IHubContext<UpdateHub> updateHub)
    {
        commands = new List<RepeatedCommand>();
        this.pump = pump;
        this.logger = logger;
        this.updateHub = updateHub;
    }

    private void Notify()
    {
        _ = updateHub.Clients.All.SendAsync("update", "update");
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
                    logger.LogInformation(DateTime.Now + ">> Executing: " + command.ToString());
                    var action = command.Execute();
                    pump.SetState(action);
                    if (command.IsDone())
                        finished = command;
                    Notify();
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
                Notify();
                return true;
            }
            return false;
        }
    }

    public void AddCommand(RepeatedCommand command)
    {
        Add(command);
        Notify();
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
                return commands.Where((c) => c.Id == id).FirstOrDefault();
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

    public Status GetStatus()
    {
        return pump.GetStatus();
    }
}