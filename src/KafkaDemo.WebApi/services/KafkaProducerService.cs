using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace KafkaDemo.WebApi.services;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ILogger<KafkaProducerService> _logger;

    private readonly ApplicationSettings _applicationSettings;

    ProducerConfig _configProducer;

    public KafkaProducerService(ILogger<KafkaProducerService> logger, IOptions<ApplicationSettings> appSettings)
    {
        _logger = logger;
        _applicationSettings = appSettings.Value;
        _configProducer = new ProducerConfig
        {
            BootstrapServers = _applicationSettings.KafkaConnection
        };
    }
    
    public async Task RegisterProducer(string topic, string key, string value)
    {
        
        using var producer = new ProducerBuilder<string, string>(_configProducer).Build();

        try
        {
            var dr = await producer.ProduceAsync(topic,
                new Message<string, string> {Key = key, Value = value});
        
            //_logger.LogInformation($"{dr.Timestamp.UtcDateTime} - Register in topic ({topic}) the message ({key}:{value}).");
        }
        catch (ProduceException<string,string> e)
        {
            _logger.LogError($"Producer - error: {e.Message}");
        }
        
    }

}