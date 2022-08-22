namespace PompServer.Models;

public interface IPump
{
    void SetState(bool value);
    bool GetState();
}

public class Pump : IPump
{
    private bool Status = false;

    public void SetState(bool value)
    {
        Status = value;
    }
    public bool GetState()
    {
        return Status;
    }
}
