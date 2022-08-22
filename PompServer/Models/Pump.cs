namespace PompServer.Models;

public interface IPump
{
    void setState(bool value);
}

public class Pump : IPump
{
    private bool Status = false;

    public void setState(bool value)
    {
        Status = value;
    }
    public bool getState()
    {
        return Status;
    }
}
