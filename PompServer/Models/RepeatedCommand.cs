namespace PompServer.Models;

public class RepeatedCommand : Command
{
    private int amount;
    private TimeSpan offTime;
    private TimeSpan onTime;
    private DateTime nextTime;

    public RepeatedCommand(
        int id,
        TimeSpan offTime,
        TimeSpan onTime,
        int amount
        ) : base(id, true)
    {
        this.amount = amount;
        this.offTime = offTime;
        this.onTime = onTime;
        this.nextTime = DateTime.Now;
    }

    public override bool ShouldExecute(DateTime time)
    {
        return nextTime <= time;
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
            amount--;
            if (amount == 0) done = true;
            return false;
        }
    }
}