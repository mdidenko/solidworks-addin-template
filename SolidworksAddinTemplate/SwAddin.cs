using SolidworksAddinTemplate.Graphic;
using SolidworksAddinTemplate.States;
using SolidworksAddinTemplate.Utils;

namespace SolidworksAddinTemplate;
#nullable enable

[ComVisible(true)]
[Guid(AddinData.GUID)]
public sealed class SwAddin : ISwAddin
{
    internal static string DirectoryPath { get; } = DllUtils.GetDirectoryPath<SwAddin>();
    internal static SldWorks? SwApplication { get; private set; } = null;
    internal static int SwCookie { get; private set; } = 0;

    #region Add-In COM (un)registration
    [ComRegisterFunction]
    public static void RegisterFunction(Type t)
    {
        try
        {
            WinRegistryHelper.RegisterSwAddin(t.GUID.ToString(), AddinData.TITLE, AddinData.DESCRIPTION, AddinData.LOAD_AT_STARTUP);
        }
        catch (Exception ex)
        {
            // Warning: logging not implemented! 
            throw new NotImplementedException();
        }
    }

    [ComUnregisterFunction]
    public static void UnregisterFunction(Type t)
    {
        try
        {
            WinRegistryHelper.UnregisterSwAddin(t.GUID.ToString());
        } 
        catch (Exception ex)
        {
            // Warning: logging not implemented! 
            throw new NotImplementedException();
        }
    }
    #endregion

    # region ISwAddin realization
    public bool ConnectToSW(object thisSW, int cookie)
    {
        SwApplication = (SldWorks)thisSW;
        SwCookie = cookie;
        try
        {
            LoadInterfacesLanguage();
            SwVersionCheckHandler(SwApplication.GetLatestSupportedFileVersion());
            SwAddinUI.LoadUI();
        }
        catch (Exception ex)
        {
            // Warning: logging not implemented! 
            DisconnectFromSW();
            throw new NotImplementedException();
            return false;
        }
        return true;
    }

    public bool DisconnectFromSW()
    {
        try
        {
            SwAddinUI.UnloadUI();
            UnloadSW();
        }
        catch (Exception ex)
        {
            // Warning: logging not implemented! 
            throw new NotImplementedException();
            return false;
        }
        return true;
    }
    #endregion

    private static void UnloadSW()
    {
        if (SwApplication == null) return;

        try
        {
            Marshal.ReleaseComObject(SwApplication);
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when unloading SW.", ex)
                .SetCode(201);
        }
        finally
        {
            SwApplication = null;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    private static void LoadInterfacesLanguage()
    {
        try
        {
            // Warning: not implemented!
        }
        catch (Exception ex)
        {
            string errorMessage = $"Upload interface language for {AddinData.TITLE} failed!";
            SwApplication!.SendMsgToUser2(errorMessage, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
            throw new Exception("Exception when loading interface language.", ex)
                .SetCode(202);
        }
    }

    private static void SwVersionCheckHandler(in int version) 
    {
        bool isSupported;
        try
        {
            isSupported = Enum.IsDefined(typeof(SwApplicationVersion_e), version);
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when checking that SW verion is supported.", ex)
                .AddParameter(nameof(version), version.ToString())
                .SetCode(203);
        }
        if (!isSupported)
        {
            string errorMessage = $"{AddinData.TITLE} doesn't support the current SolidWorks version!";
            SwApplication!.SendMsgToUser2(errorMessage, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
            throw new Exception("Exception when current SW version doesn't supported.")
                .AddParameter(nameof(version), version.ToString())
                .SetCode(204);
        }
    }
}
