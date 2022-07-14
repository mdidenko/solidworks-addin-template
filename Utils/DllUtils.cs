using NLog;
using System;
using System.IO;

namespace CAPP.Utils
{
    internal static class DllUtils
    {
        private static readonly Lazy<Logger> s_logger = new Lazy<Logger>(() => LogManager.GetCurrentClassLogger());

        internal static string GetDirectoryPath<TClass>()
        {
            string dllPath = string.Empty;
            try
            {
                dllPath = Path.GetDirectoryName(typeof(TClass).Assembly.CodeBase + string.Empty).Replace(@"file:\", "");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is PathTooLongException)
            {
                s_logger.Value.Debug(ex);
            }
            return dllPath;
        }
    }
}
