using System.Device.Gpio;

namespace PompServer.Models;

public interface IPump
{
    void SetState(bool value);
    Status GetStatus();
}

public class Pump : IPump
{
    private Status Status = new Status();

    private readonly GpioController? controller;

    private const int PIN = 18;

    public Pump()
    {
        #if DEBUG
        #else
        controller = new();
        controller.OpenPin(PIN, PinMode.Output);
        #endif
    }

    private void setPin(bool value)
    {
        #if DEBUG
        #else
        controller?.Write(PIN, value ? PinValue.Low : PinValue.High);
        #endif
    }

    public void SetState(bool value)
    {
        Status.Active = value;
        setPin(value);
        Status.LastUsed = DateTime.Now;
    }
    public Status GetStatus()
    {
        return Status;
    }
}
