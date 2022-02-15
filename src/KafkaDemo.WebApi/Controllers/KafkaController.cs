using KafkaDemo.WebApi.services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaDemo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KafkaController : ControllerBase
{

    private readonly ILogger<KafkaController> _logger;
    private readonly IKafkaProducerService _kafkaProducerService;

    public KafkaController(ILogger<KafkaController> logger, IKafkaProducerService kafkaProducerService)
    {
        _logger = logger;
        _kafkaProducerService = kafkaProducerService;
    }


    [HttpPost("registerProducer")]
    public async Task<IActionResult> RegisterProducer(string key, string value)
    {
        await _kafkaProducerService.RegisterProducer("test",key,value);

        return Ok();
    }
    
}