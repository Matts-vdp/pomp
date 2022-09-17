namespace PompServer.Models;

public class PlannedCommand : Command
{
    private int amount;
    private TimeSpan offTime;
    private TimeSpan onTime;
    private TimeSpan waitTime;
    private DateTime nextTime;

    public PlannedCommand(
        int id, 
        TimeSpan offTime,
        TimeSpan onTime,
        int amount,
        TimeSpan waitTime,
        DateTime nextTime
        ) : base(id, true)
    {
        this.amount = amount;
        this.offTime = offTime;
        this.onTime = onTime;
        this.waitTime = waitTime;
        this.nextTime = nextTime;
    }

    public override bool ShouldExecute()
    {
        var now = DateTime.Now;
        return nextTime <= now;
    }
    public override bool Execute()
    {
        if (Action) // als eerste keer 
        {
            nextTime += onTime;
            Action = false;
            return true;

        }
        else
        {
            nextTime += offTime;
            Action = true;
            
            return false;
        }
    }
    public override bool IsDone()
    {
        return base.IsDone();
    }
}