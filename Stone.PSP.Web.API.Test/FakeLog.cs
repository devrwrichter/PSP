using Microsoft.Extensions.Logging;

namespace Stone.PSP.Web.API.Test
{
    public class FakeLog<T> : ILogger<T>
    {
        public void LogError(string msg)
        {

        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

        }
    }
}
