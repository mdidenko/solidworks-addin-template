namespace CAPP.States
{
    internal static class ConfigData
    {
        internal static class AddIn
        {
            internal const string GUID = "E59DBFCD-DD9E-4DD7-9388-DBAF1874D17A";
            internal const string TITLE = "CAPP Add-In";
            internal const string DESCRIPTION = "Desc...";
            internal const bool LOAD_AT_STARTUP = true;
        }

        internal static class Graphic
        {
            internal static class Taskpane
            {
                internal const string TOOL_TIP_VALUE = "CAPP ToolTip!";
            }

            internal static class UIProgId
            {
                internal const string TASKPANE_UI = "TASKPANE_UI";
            }
        }
        
        internal static class Resources 
        {
            internal static class LogoRelativePath
            {
                internal const string IMAGE_20PX = @"Resources\Logo\Logo20px.png";
                internal const string IMAGE_32PX = @"Resources\Logo\Logo32px.png";
                internal const string IMAGE_40PX = @"Resources\Logo\Logo40px.png";
                internal const string IMAGE_64PX = @"Resources\Logo\Logo64px.png";
                internal const string IMAGE_96PX = @"Resources\Logo\Logo96px.png";
                internal const string IMAGE_128PX = @"Resources\Logo\Logo128px.png";
            }
        }
    }
}
