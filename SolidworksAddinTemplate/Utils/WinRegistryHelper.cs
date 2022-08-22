namespace SolidworksAddinTemplate.Utils;

internal static class WinRegistryHelper
{
    private const string _SW_ADDIN_KEY_PATH_TEMPLATE = @"SOFTWARE\SolidWorks\AddIns\{{{0}}}";
    private const string _SW_ADDIN_STARTUP_KEY_PATH_TEMPLATE = @"SOFTWARE\SolidWorks\AddInsStartup\{{{0}}}";

    public static void RegisterSwAddin(in string guid, in string title, in string description, in bool loadAtStartup)
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
        catch (Exception ex)
        {
            throw new Exception("Exception when registering SW Add-In in Windows.", ex)
                .AddParameter(nameof(guid), guid)
                .AddParameter(nameof(title), title)
                .AddParameter(nameof(description), description)
                .AddParameter(nameof(loadAtStartup), loadAtStartup.ToString())
                .SetCode(102);
        }
    }

    public static void UnregisterSwAddin(in string guid)
    {
        try
        {
            Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(string.Format(_SW_ADDIN_STARTUP_KEY_PATH_TEMPLATE, guid), false);
            Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(string.Format(_SW_ADDIN_KEY_PATH_TEMPLATE, guid), false);
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when unregistering SW Add-In in Windows.", ex)
                .AddParameter(nameof(guid), guid)
                .SetCode(103);
        }
    }
}
