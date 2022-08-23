using System;

public struct Status
{
	public bool Active { get; set; } 
	public DateTime LastUsed { get; set; }

	public Status()
	{
		Active = false;
		LastUsed = DateTime.MinValue;
	}
}
