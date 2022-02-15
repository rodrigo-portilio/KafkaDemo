using KafkaDemo.WebApi.services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaDemo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KafkaController : ControllerBase
{

    private readonly ILogger<KafkaController> _logger;
    private readonly IKafkaService _kafkaService;

    public KafkaController(ILogger<KafkaController> logger, IKafkaService kafkaService)
    {
        _logger = logger;
        _kafkaService = kafkaService;
    }


    [HttpPost("registerProducer")]
    public async Task<IActionResult> RegisterProducer(string key, string value)
    {
        await _kafkaService.RegisterProducer("test",key,value);

        return Ok();
    }
    
}