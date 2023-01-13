namespace EmployeeData.Log
{
    public enum LogEventsKey : int
    {
        ErrFileUplaod = 1000,
    }

    public static class LogEventsMessage
    {
        public static readonly Dictionary<int, string> Meesages = new Dictionary<int, string>()
        {
            {(int) LogEventsKey.ErrFileUplaod, "파일 업로드에 실패하였습니다.\n msg : {0}" }
        };


        public static readonly Dictionary<int, LogLevel> Levels = new Dictionary<int, LogLevel>()
        {
            {(int) LogEventsKey.ErrFileUplaod, LogLevel.Error }
        };
    }
    public static class LogUtils
    {
        public static void WriteLog<T>(ILogger<T> logger, LogEventsKey key, params object[] args)
        {
            DateTime now = DateTime.Now;
            string logPrefix = $"[{now.ToString("yyyy-MM-dd HH:mm:ss")}]";
            if (false == LogEventsMessage.Levels.ContainsKey((int)key))
            {
                logger.Log(LogLevel.Error, $"{logPrefix} Key가 정의되어있지 않은 로그입니다.");
            }

            if (false == LogEventsMessage.Meesages.ContainsKey((int)key))
            {
                logger.Log(LogLevel.Error, $"{logPrefix} Meesages가 정의되어있지 않은 로그입니다.");
            }

            LogLevel level = LogEventsMessage.Levels[(int)key];
            string message = String.Format(LogEventsMessage.Meesages[(int)key], args);
            logger.Log(level, $"{logPrefix} {message}");
        }
    }
}
