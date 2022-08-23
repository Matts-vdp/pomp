namespace PompServer.Models;

public class BasicCommand: RepeatedCommand
{
    public BasicCommand(bool action) : base(action){}
    
    public override bool Execute()
    {
        done = true;
        return Action;
    }

    public override bool ShouldExecute(DateTime time)
    {
        return true;
    }
    
    public override string ToString()
    {
        return $"id:{Id} action:{Action}";
    }
}