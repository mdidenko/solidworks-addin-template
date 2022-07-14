using NLog;
using System;
using System.Runtime.InteropServices;
using SolidWorks.Interop.sldworks;
using CAPP.States;
using CAPP.Utils;

namespace CAPP.Graphic
{
    internal static class SwAddinUI
    {
        private static readonly Lazy<Logger> s_logger = new Lazy<Logger>(() => LogManager.GetCurrentClassLogger());
        private static readonly string[] s_logoImagePaths = ResourcesManager.GetLogoImagePaths(SwAddin.DirectoryPath);
        private static TaskpaneView s_swTaskpaneView = null;
        private static TaskpaneUI s_TaskpaneUI = null;

        #region Getters & Setters
        private static string[] LogoImagePaths
        {
            get
            {
                if (s_logoImagePaths == default)
                {
                    int exceptionId = (int)ExceptionId_e.SWAddinUI1;
                    s_logger.Value.Error(exceptionId);
                    SwAddin.ForsedStop(exceptionId);
                }
                return s_logoImagePaths;
            }
        }
        #endregion

        public static bool LoadUI()
        {
            LoadToTaskpane(ConfigData.Graphic.Taskpane.TOOL_TIP_VALUE);
            LoadToToolbar();
            return true;
        }

        public static bool UnloadUI()
        {
            bool disconnectResult = true;
            
            if (!UnloadFromTaskpane())
            {
                disconnectResult = false;
            }

            UnloadFromToolbar();

            return disconnectResult;
        }

        #region Taskpane
        private static void LoadToTaskpane(in string toolTip)
        {
            s_swTaskpaneView = SwAddin.SWApplication.CreateTaskpaneView3(s_logoImagePaths, toolTip);
            s_TaskpaneUI = s_swTaskpaneView.AddControl(TaskpaneUI.PROG_ID, string.Empty);
        }

        private static bool UnloadFromTaskpane()
        {
            s_TaskpaneUI = null;

            if (s_swTaskpaneView == null)
            {
                return true;
            }

            bool unloadResult = true;

            if (!s_swTaskpaneView.DeleteView())
            {
                s_logger.Value.Error(ExceptionId_e.SWAddinUI2);
                unloadResult = false;
            }

            try
            {
                Marshal.ReleaseComObject(s_swTaskpaneView);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is NullReferenceException)
            {
                s_swTaskpaneView = null;
                var logger = s_logger.Value;
                logger.Debug(ex);
                logger.Error(ExceptionId_e.SWAddinUI3);
                return false;
            }
        
            s_swTaskpaneView = null;
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return unloadResult;
        }
        #endregion

        #region Toolbar
        private static void LoadToToolbar()
        {
            //menuId = SWApplication.AddMenu((int)swDocumentTypes_e.swDocPART, "MyMenu", 0);
            //menuId = SWApplication.AddMenuItem5((int)swDocumentTypes_e.swDocPART, SWCookie, "MyMenuItem@MyMenu", 0, "MyMenuCallback", "MyMenuEnableMethod", "My menu item", logImagePaths);
        }

        private static void UnloadFromToolbar()
        {
            //SWApplication.RemoveToolbar2();
        }
        #endregion
    }
}
