using System;

using Microsoft.AspNetCore.Mvc;
using PompServer.Hubs;
using PompServer.Models;
using PompServer.Services;

namespace PompServer.Controllers;

[ApiController]
[Route("")]
public class IndexController : ControllerBase
{
    private readonly ILogger<IndexController> _logger;

    public IndexController(
        ILogger<IndexController> logger
        )
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return Redirect("/index.html");
    }
}
