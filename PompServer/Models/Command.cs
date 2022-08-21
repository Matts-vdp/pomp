namespace PompServer.Models;

public class Command
{
    public int Id;
    public bool Action;
    protected bool done;

    public Command(int id, bool action)
    {
        Id = id;
        Action = action;
    }
    public virtual bool Execute()
    {
        done = true;
        return Action;
    }
    public virtual bool ShouldExecute(DateTime time)
    {
        return true;
    }
    public virtual bool IsDone()
    {
        return done;
    }
}