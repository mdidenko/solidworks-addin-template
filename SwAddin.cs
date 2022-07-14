using NLog;
using System;
using System.Runtime.InteropServices;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using CAPP.Utils;
using CAPP.States;
using CAPP.Graphic;

namespace CAPP
{
    [ComVisible(true)]
    [Guid(ConfigData.AddIn.GUID)]
    public class SwAddin : ISwAddin
    {
        internal const string TITLE = ConfigData.AddIn.TITLE;
        internal const string DESCRIPTION = ConfigData.AddIn.DESCRIPTION;
        internal const bool LOAD_AT_STARTUP = ConfigData.AddIn.LOAD_AT_STARTUP;
        private static readonly Lazy<Logger> s_logger = new Lazy<Logger>(() => LogManager.GetCurrentClassLogger());
        private static readonly string s_directoryPath = DllUtils.GetDirectoryPath<SwAddin>();
        private static SldWorks s_swApplication = null;
        private static int s_swCookie = 0;

        #region Getters & Setters
        internal static string DirectoryPath
        {
            get
            {
                if (s_directoryPath == string.Empty)
                {
                    int exceptionId = (int)ExceptionId_e.SWAddin1;
                    s_logger.Value.Error(exceptionId);
                    ForsedStop(exceptionId);
                }
                return s_directoryPath;
            }
        }

        internal static SldWorks SWApplication
        {
            get 
            {
                if (s_swApplication == null)
                {
                    int exceptionId = (int)ExceptionId_e.SWAddin2;
                    s_logger.Value.Error(exceptionId);
                    ForsedStop(exceptionId);
                }
                return s_swApplication; 
            }
        }

        internal static int SWCookie
        {
            get 
            {
                if (s_swCookie <= 0)
                {
                    int exceptionId = (int)ExceptionId_e.SWAddin3;
                    s_logger.Value.Error(exceptionId);
                    ForsedStop(exceptionId);
                }
                return s_swCookie;
            }
        }
        #endregion

        #region Add-In COM (Un)Registration
        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            if(!WinRegistryHelper.RegisterSwAddIn(t.GUID.ToString(), TITLE, DESCRIPTION, LOAD_AT_STARTUP))
            { 
                s_logger.Value.Error(ExceptionId_e.SWAddin4);
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            if (!WinRegistryHelper.UnregisterSwAddIn(t.GUID.ToString()))
            {
                s_logger.Value.Error(ExceptionId_e.SWAddin5);
            }
        }
        #endregion

        public bool ConnectToSW(object thisSW, int cookie)
        {
            s_swApplication = (SldWorks)thisSW;
            s_swCookie = cookie;

            if (!LoadInterfacesLanguage())
            {
                string errorMessage = $"Upload interface language for {TITLE} failed!"; // hardcode (wait lang)
                SWApplication.SendMsgToUser2(errorMessage, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
                UnloadAddIn();
                return false;
            }

            if(!IsSupportedSWVersion((ushort)s_swApplication.GetLatestSupportedFileVersion()))
            {
                string errorMessage = $"{TITLE} не поддерживает текущую версию SolidWorks!"; // hardcode (wait lang)
                SWApplication.SendMsgToUser2(errorMessage, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
                UnloadAddIn();
                return false;
            }

            if (!SwAddinUI.LoadUI())
            {
                string errorMessage = $"Ошибка загрузки интерфейса для {TITLE}!"; // hardcode (wait lang)
                SWApplication.SendMsgToUser2(errorMessage, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
                UnloadAddIn();
                return false;
            }

            //var ok = SWApplication.SetAddinCallbackInfo2(0, this, SWCookie);  // set callback

            return true;
        }

        public bool DisconnectFromSW()
        {
            return UnloadAddIn();
        }

        private static bool UnloadAddIn()
        {
            bool unloadResult = true;

            if (!SwAddinUI.UnloadUI())
            {
               unloadResult = false;
            }

            if (!UnloadSW())
            {
                unloadResult = false;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return unloadResult;
        }

        private static bool UnloadSW()
        {
            if (s_swApplication == null)
            {
                return true;
            }

            try
            {
                Marshal.ReleaseComObject(s_swApplication);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is NullReferenceException)
            {
                s_swApplication = null;
                var logger = s_logger.Value;
                logger.Debug(ex);
                logger.Error(ExceptionId_e.SWAddin6);
                return false;
            }
                
            s_swApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
       
            return true;
        }

        internal static void ForsedStop(in int exitCode)
        {
            UnloadAddIn();
            System.Environment.Exit(exitCode);
        }

        private static bool LoadInterfacesLanguage()
        {
            // load dll with lang
            return true;
        }

        private static bool IsSupportedSWVersion(in ushort version)
        {
            if (Enum.IsDefined(typeof(swSupportedApplicationVersion_e), version))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
