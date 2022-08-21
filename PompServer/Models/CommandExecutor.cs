namespace PompServer.Models;

public class CommandExecutor
{
    private List<Command> commands;
    private IPomp pomp;

    public CommandExecutor(IPomp pomp)
    {
        commands = new List<Command>();
        this.pomp = pomp;
    }
}