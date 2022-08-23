namespace PompServer.Models;

public interface IPump
{
    void SetState(bool value);
    Status GetStatus();
}

public class Pump : IPump
{
    private Status Status = new Status();

    public void SetState(bool value)
    {
        Status.Active = value;
    }
    public Status GetStatus()
    {
        return Status;
    }
}
