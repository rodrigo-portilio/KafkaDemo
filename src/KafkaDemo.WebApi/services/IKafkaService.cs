namespace KafkaDemo.WebApi.services;

public interface IKafkaService
{
    Task RegisterProducer(string topic, string key, string value);
}