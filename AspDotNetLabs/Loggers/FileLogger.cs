namespace AspDotNetLabs.Loggers
{
   public class FileLogger : ILogger
    {
        private static object _lock = new object();
        private readonly string path;
        public FileLogger(string path)
        {
            this.path = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
 
        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }
 
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(path, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
