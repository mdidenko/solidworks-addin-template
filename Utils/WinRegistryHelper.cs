using NLog;
using System;
using System.IO;
using System.Security;

namespace CAPP.Utils
{
    internal static class WinRegistryHelper
    {
        private static readonly Lazy<Logger> s_logger = new Lazy<Logger>(() => LogManager.GetCurrentClassLogger());
        private const string _SW_ADDIN_KEY_PATH_TEMPLATE = @"SOFTWARE\SolidWorks\AddIns\{{{0}}}";
        private const string _SW_ADDIN_STARTUP_KEY_PATH_TEMPLATE = @"SOFTWARE\SolidWorks\AddInsStartup\{{{0}}}";

        internal static bool RegisterSwAddIn(in string guid, in string title, in string description, in bool loadAtStartup)
        {
            try
            {
                using (var winRegSwAddInKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(string.Format(_SW_ADDIN_KEY_PATH_TEMPLATE, guid), true))
                {
                    winRegSwAddInKey.SetValue(null, 0);
                    winRegSwAddInKey.SetValue("Title", title);
                    winRegSwAddInKey.SetValue("Description", description);
                }

                using (var winRegSwStartupAddInKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Format(_SW_ADDIN_STARTUP_KEY_PATH_TEMPLATE, guid), true))
                {
                    winRegSwStartupAddInKey.SetValue(null, Convert.ToInt32(loadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
                }
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is SecurityException || ex is UnauthorizedAccessException || ex is IOException || ex is FormatException)
            {
                s_logger.Value.Debug(ex);
                return false;
            }
            return true;
        }

        internal static bool UnregisterSwAddIn(in string guid)
        {
            try
            {
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(string.Format(_SW_ADDIN_STARTUP_KEY_PATH_TEMPLATE, guid), false);
                Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(string.Format(_SW_ADDIN_KEY_PATH_TEMPLATE, guid), false);
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentNullException || ex is SecurityException || ex is ObjectDisposedException || ex is UnauthorizedAccessException || ex is FormatException)
            {
                s_logger.Value.Debug(ex);
                return false;
            }
            return true;
        }
    }
}
