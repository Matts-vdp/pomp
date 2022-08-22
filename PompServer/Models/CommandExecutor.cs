namespace PompServer.Models;

public class CommandExecutor
{
    private List<Command> commands;
    private IPump pump;

    public CommandExecutor(IPump pump)
    {
        commands = new List<Command>();
        this.pump = pump;
    }

    public void Run(DateTime time)
    {
        lock (commands)
        {
            Command? finished = null;
            foreach (var command in commands)
            {
                if (command.ShouldExecute(time))
                {
                    var action = command.Execute();
                    pump.setState(action);
                    if (command.IsDone())
                        finished = command;
                    break;
                }
            }
            if (finished != null)
                commands.Remove(finished);
        }
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
}