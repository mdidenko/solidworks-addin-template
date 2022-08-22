using SolidworksAddinTemplate.Utils;
using SolidworksAddinTemplate.States;

namespace SolidworksAddinTemplate.Graphic;
#nullable enable

internal static class SwAddinUI
{
    public static string[] LogoImagePaths { get; } = ResourceManager.GetLogoImagePaths(SwAddin.DirectoryPath);
    private static TaskpaneView? s_swTaskpaneView = null;
    private static TaskpaneUI? s_taskpaneUI = null;

    # region (Un)load UI 
    public static void LoadUI()
    {
        try
        {
            InjectToTaskpane(TaskpaneData.TOOL_TIP_VALUE);
            InjectToToolbar();
        }
        catch (Exception ex)
        {
            string errorMessage = $"Interface loading error for {AddinData.TITLE}!";
            SwAddin.SwApplication!.SendMsgToUser2(errorMessage, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
            throw new Exception("Exception when loading UI.", ex)
                .SetCode(301);
        }
    }

    public static void UnloadUI()
    {
        try
        {
            UninjectFromTaskpane();
            UninjectFromToolbar();
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when unloading UI.", ex)
                .SetCode(302);
        }
    }
    # endregion

    #region Taskpane
    private static void InjectToTaskpane(in string toolTip)
    {
        s_swTaskpaneView = SwAddin.SwApplication!.CreateTaskpaneView3(LogoImagePaths, toolTip);
        s_taskpaneUI = s_swTaskpaneView.AddControl(UiProgId.TASKPANE_UI, string.Empty);
    }

    private static void UninjectFromTaskpane()
    {
        s_taskpaneUI = null;
        if (s_swTaskpaneView == null) return;

        s_swTaskpaneView.DeleteView();

        try
        {
            Marshal.ReleaseComObject(s_swTaskpaneView);
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when uninjecting from taskpane.", ex)
                .SetCode(303);
        }
        finally
        {
            s_swTaskpaneView = null;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
    #endregion

    #region Toolbar
    private static void InjectToToolbar()
    {
        // menuId = SWApplication.AddMenu((int)swDocumentTypes_e.swDocPART, "MyMenu", 0);
        // menuId = SWApplication.AddMenuItem5((int)swDocumentTypes_e.swDocPART, SWCookie, "MyMenuItem@MyMenu", 0, "MyMenuCallback", "MyMenuEnableMethod", "My menu item", logImagePaths);
    }

    private static void UninjectFromToolbar()
    {
        // SWApplication.RemoveToolbar2();
    }
    #endregion
}
