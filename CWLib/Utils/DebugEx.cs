using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace CWLib
{
    public class DebugEx
    {
        [Conditional("ENABLE_LOG")]
        public static void Log(object message)
            => Debug.Log(message);

        [Conditional("ENABLE_LOG")]
        public static void LogFormat(string format, params object[] args)
            => Debug.LogFormat(format, args);

        [Conditional("ENABLE_LOG")]
        public static void LogWarning(object message)
            => Debug.LogWarning(message);

        [Conditional("ENABLE_LOG")]
        public static void LogWarningFormat(string format, params object[] args) 
            => Debug.LogWarningFormat(format, args);

        [Conditional("ENABLE_LOG")]
        public static void LogError(object message) 
            => Debug.LogError(message);

        [Conditional("ENABLE_LOG")]
        public static void LogErrorFormat(string format, params object[] args)
            => Debug.LogErrorFormat(format, args);

        [Conditional("ENABLE_LOG")]
        public static void LogAssertion(object message)
            => Debug.LogAssertion(message);
        
        [Conditional("ENABLE_LOG")]
        public static void LogException(Exception exception)
            => Debug.LogException(exception);

        [Conditional("ENABLE_LOG")]
        public static void Assert(bool condition, object message)
            => Debug.Assert(condition, message);

        [Conditional("ENABLE_LOG")]
        public static void AssertFormat(bool condition, string format, object message)
            => Debug.AssertFormat(condition, format, message);
    }
}