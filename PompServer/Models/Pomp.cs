namespace PompServer.Models;

public interface IPomp
{
    void setState(bool value);
}

public class Pomp : IPomp
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
