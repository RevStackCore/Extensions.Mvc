using System;
namespace RevStackCore.Extensions.Mvc
{
    public class LogLevelConfiguration
    {
        public string Default { get; set; }
    }

    public class LoggingConfiguration
    {
        public bool IncludeScopes { get; set; }
        public LogLevelConfiguration LogLevel { get; set; }
    }

    public class BaseAppSettings
    {
        public LoggingConfiguration Logging { get; set; }
    }
}
