using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging
{
    public class MessagingService
    {
        private readonly IBus _bus;

        public MessagingService(IBus bus)
        {
            _bus = bus;
        }

        public async Task SendMessageAsync<T>(T message)
        {
            await _bus.Send(message);
        }
    }
}
