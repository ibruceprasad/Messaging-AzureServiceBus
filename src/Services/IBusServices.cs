namespace Services
{
    public interface IBusServices
    {
        // Receive message 
        Task<T?> GetMessageAsync<T>();

        // Send message
        Task<bool> SendMessageAsync<T>(T message) where T : class;    
    }
}
