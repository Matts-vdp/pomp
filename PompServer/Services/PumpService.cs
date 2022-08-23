using PompServer.Models;
using System;

namespace PompServer.Services;

public class PumpService
{
	private CommandExecutor commandExecutor;
	public PumpService(ILogger<PumpService> logger)
	{
		commandExecutor = new CommandExecutor(new Pump(), logger);
	}

	public List<RepeatedCommand> GetCommands()
	{
		return commandExecutor.GetCommands();
	}

	public void AddCommand(RepeatedCommand command)
	{
		commandExecutor.AddCommand(command);
	}
}
