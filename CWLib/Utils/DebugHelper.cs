using System.Diagnostics;
using Debug = UnityEngine.Debug;

#nullable disable
namespace CWLib
{
    public class DebugHelper
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message)
            => Debug.Log($"[💻] {message}");

        [Conditional("UNITY_EDITOR")]
        public static void LogFormat(string format, params object[] args)
            => Debug.Log(string.Format($"[💻] {format}", args));
        
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object message)
            => Debug.LogWarning($"[💻] {message}");
        
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object message)
            => Debug.LogError($"[💻] {message}");

        [Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition, string desc = "")
        {
            if (condition)
                return;
            Debug.LogError($"[💻] [Assert] {desc}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void AssertNotNull(object obj, string desc = "")
        {
            if (obj != null)
                return;
            Debug.LogError($"[💻][AssertNotNull] {desc}");
        }
    }
}