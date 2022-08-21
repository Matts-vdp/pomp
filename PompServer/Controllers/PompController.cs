using Microsoft.AspNetCore.Mvc;

namespace PompServer.Controllers;

[ApiController]
[Route("[controller]")]
public class PompController : ControllerBase
{
    private readonly ILogger<PompController> _logger;

    public PompController(ILogger<PompController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPomp")]
    public string Index()
    {
        return "Hallo";
    }
}
