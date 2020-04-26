using Microsoft.Extensions.Hosting;

namespace DistributedExceptionHandling.Worker.Service
{
    public interface IRabbitBackgroundWorkerService : IHostedService
    {
        void ConsumeMessage();
    }
}