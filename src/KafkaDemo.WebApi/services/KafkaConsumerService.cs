using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace KafkaDemo.WebApi.services;

public class KafkaConsumerService:BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IServiceScopeFactory _factory;
    private readonly IOptions<ApplicationSettings> _options;

    private ConsumerConfig _configConsumer;


    public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IServiceScopeFactory factory, IOptions<ApplicationSettings> options)
    {
        _logger = logger;
        _factory = factory;
        _options = options;

        var applicationSettings = options.Value;

        _configConsumer = new ConsumerConfig
        {
            GroupId = "local-consumer",
            BootstrapServers = applicationSettings.KafkaConnection,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() => Consumer(stoppingToken));
        
        return Task.CompletedTask;
    }

    private void Consumer(CancellationToken stoppingToken)
    {
        
        using var consumer = new ConsumerBuilder<string, string>(_configConsumer).Build();

        consumer.Subscribe("test");
        var cts = new CancellationTokenSource();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumerResult = consumer.Consume(cts.Token);

                if (consumerResult.Message != null)
                {
                    _logger.LogInformation($"{consumerResult.Message.Timestamp} - Consumer - topic ({consumerResult.Topic}) with message ({consumerResult.Message.Key}:{consumerResult.Message.Value})");
                }
            
            
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Consumer - error: {e.Message}");
            }
        }
        
    }
}