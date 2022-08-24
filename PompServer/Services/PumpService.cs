using Microsoft.AspNetCore.SignalR;
using PompServer.Hubs;
using PompServer.Models;
using System;

namespace PompServer.Services;

public class PumpService
{
	private CommandExecutor commandExecutor;

	public PumpService(ILogger<PumpService> logger,  IHubContext<UpdateHub> updateHub)
	{
		commandExecutor = new CommandExecutor(new Pump(), logger, updateHub);
	}

	public List<RepeatedCommand> GetCommands()
	{
		return commandExecutor.GetCommands();
	}

	public void AddCommand(RepeatedCommand command)
	{
		commandExecutor.AddCommand(command);
	}

	public bool DeleteCommand(Guid id)
	{
		return commandExecutor.Delete(id);
	}
    public void ClearCommands()
    {
        commandExecutor.Clear();
    }

    public Status GetStatus()
	{
		return commandExecutor.GetStatus();
	}
}
