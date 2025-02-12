namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging;

public interface IMessagingService
{
    Task SendMessageAsync<T>(T message);
}