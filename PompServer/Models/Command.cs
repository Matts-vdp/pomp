namespace PompServer.Models;

public class Command
{
    public int Id { get; set; }
    public bool Action { get; set; }
    protected bool done { get; set; }

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
    public override string ToString()
    {
        return $"id:{Id} action:{Action}";
    }
}