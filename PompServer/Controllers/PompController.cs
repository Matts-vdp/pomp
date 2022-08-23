using Microsoft.AspNetCore.Mvc;
using PompServer.Models;
using PompServer.Services;

namespace PompServer.Controllers;

[ApiController]
[Route("[controller]")]
public class PompController : ControllerBase
{
    private readonly ILogger<PompController> _logger;
    private readonly PumpService pumpService;

    public PompController(
        ILogger<PompController> logger, 
        PumpService pumpService
        )
    {
        _logger = logger;
        this.pumpService = pumpService;
    }

    [HttpGet(Name = "GetPomp")]
    public string Index()
    {
        return "Hallo";
    }

    [HttpGet("Commands")]
    public List<Command> Commands()
    {
        return pumpService.GetCommands();
    }

    [HttpPost("Commands")]
    public IActionResult AddCommand(bool action)
    {
        var command = new Command(0, action);
        _logger.LogInformation("Created: " + command.ToString());
        pumpService.AddCommand(command);
        return Ok();
    }

    [HttpPost("RepeatedCommand")]
    public IActionResult AddRepeatedCommand(int offTime, int onTime, int amount)
    {
        var command = new RepeatedCommand(
            0, 
            TimeSpan.FromSeconds(offTime),
            TimeSpan.FromSeconds(onTime), 
            amount
            );
        _logger.LogInformation("Created: " + command.ToString());
        pumpService.AddCommand(command);
        return Ok();
    }
}
