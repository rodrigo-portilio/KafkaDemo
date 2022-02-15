namespace KafkaDemo.WebApi.services;

public interface IKafkaProducerService
{
    Task RegisterProducer(string topic, string key, string value);
}