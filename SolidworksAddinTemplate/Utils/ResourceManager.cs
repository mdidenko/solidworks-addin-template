using System.IO;
using SolidworksAddinTemplate.States;

namespace SolidworksAddinTemplate.Utils;

internal static class ResourceManager
{
    public static string[] GetLogoImagePaths(in string addinRootDirectory)
    {
        string[] logoImagePaths = new string[6];
        try
        {
            logoImagePaths[0] = Path.Combine(addinRootDirectory, LogoPathData.IMAGE_20PX);
            logoImagePaths[1] = Path.Combine(addinRootDirectory, LogoPathData.IMAGE_32PX);
            logoImagePaths[2] = Path.Combine(addinRootDirectory, LogoPathData.IMAGE_40PX);
            logoImagePaths[3] = Path.Combine(addinRootDirectory, LogoPathData.IMAGE_64PX);
            logoImagePaths[4] = Path.Combine(addinRootDirectory, LogoPathData.IMAGE_96PX);
            logoImagePaths[5] = Path.Combine(addinRootDirectory, LogoPathData.IMAGE_128PX);
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when getting logo image paths.", ex)
                .AddParameter(nameof(addinRootDirectory), addinRootDirectory)
                .SetCode(104);
        }
        return logoImagePaths;
    }
}
