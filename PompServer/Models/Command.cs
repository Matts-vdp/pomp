namespace PompServer.Models;

public class Command 
{
    public int Id;
    public bool Action;
    private bool done;

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
    public virtual bool ShouldExecute()
    {
        return true;
    }
    public virtual bool IsDone()
    {
        return done;
    }
}