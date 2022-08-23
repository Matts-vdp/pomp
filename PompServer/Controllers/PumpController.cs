using Microsoft.AspNetCore.Mvc;
using PompServer.Models;
using PompServer.Services;

namespace PompServer.Controllers;

[ApiController]
[Route("[controller]")]
public class PumpController : ControllerBase
{
    private readonly ILogger<PumpController> _logger;
    private readonly PumpService pumpService;

    public PumpController(
        ILogger<PumpController> logger, 
        PumpService pumpService
        )
    {
        _logger = logger;
        this.pumpService = pumpService;
    }

    [HttpGet("")]
    public string Index()
    {
        return "Hallo";
    }

    [HttpGet("Status")]
    public bool Status()
    {
        return pumpService.GetStatus();
    }

    [HttpGet("Commands")]
    public List<RepeatedCommand> Commands()
    {
        return pumpService.GetCommands();
    }

    [HttpDelete("Commands")]
    public IActionResult DeleteCommand(Guid id)
    {
        var completed = pumpService.DeleteCommand(id);
        if (completed)
            return Ok();
        return BadRequest();
    }

    [HttpPost("BasicCommand")]
    public IActionResult AddCommand(bool action)
    {
        var command = new BasicCommand(action);
        _logger.LogInformation("Created: " + command.ToString());
        pumpService.AddCommand(command);
        return Ok();
    }

    [HttpPost("RepeatedCommand")]
    public IActionResult AddRepeatedCommand(int offTime, int onTime, int amount)
    {
        var command = new RepeatedCommand(amount, offTime, onTime);
        _logger.LogInformation("Created: " + command.ToString());
        pumpService.AddCommand(command);
        return Ok();
    }
}
