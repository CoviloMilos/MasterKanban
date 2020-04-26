namespace Common.DistribudetExcetionHandling
{
    public interface IRabbitMqManager
    {
        void Publish<T>(T message) where T : class;  
    }
}