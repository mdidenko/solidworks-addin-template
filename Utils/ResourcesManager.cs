using NLog;
using System;
using System.IO;
using CAPP.States;

namespace CAPP.Utils
{
    internal static class ResourcesManager
    {
        private static readonly Lazy<Logger> s_logger = new Lazy<Logger>(() => LogManager.GetCurrentClassLogger());

        internal static string[] GetLogoImagePaths(in string addinRootDirectory)
        {
            string[] logoImagePaths = new string[6];
            try
            {
                logoImagePaths[0] = Path.Combine(addinRootDirectory, ConfigData.Resources.LogoRelativePath.IMAGE_20PX);
                logoImagePaths[1] = Path.Combine(addinRootDirectory, ConfigData.Resources.LogoRelativePath.IMAGE_32PX);
                logoImagePaths[2] = Path.Combine(addinRootDirectory, ConfigData.Resources.LogoRelativePath.IMAGE_40PX);
                logoImagePaths[3] = Path.Combine(addinRootDirectory, ConfigData.Resources.LogoRelativePath.IMAGE_64PX);
                logoImagePaths[4] = Path.Combine(addinRootDirectory, ConfigData.Resources.LogoRelativePath.IMAGE_96PX);
                logoImagePaths[5] = Path.Combine(addinRootDirectory, ConfigData.Resources.LogoRelativePath.IMAGE_128PX);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                s_logger.Value.Debug(ex);
                return default;
            }
            return logoImagePaths;
        }
    }
}
