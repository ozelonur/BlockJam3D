namespace _GAME_.Scripts.Extensions
{
    public static class OBDebug
    {
        /// <summary>
        /// This Debug class is used to log messages to the console. I do not use Unity's Debug class directly because I want to be able to disable logging in release builds.
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}