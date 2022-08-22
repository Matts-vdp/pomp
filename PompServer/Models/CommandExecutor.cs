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

    public void Delete(int id)
    {
        var command = commands.Where((c) => c.Id == id).First();
        commands.Remove(command);
    }

    public void Add(Command command) { commands.Add(command); }

    public Command GetCommand(int id)
    {
        return commands.Where((c) => c.Id == id).First();
    }

    public List<Command> GetCommands() { return commands; }

}