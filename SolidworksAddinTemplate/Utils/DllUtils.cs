using System.IO;

namespace SolidworksAddinTemplate.Utils;

internal static class DllUtils
{
    public static string GetDirectoryPath<TClass>()
    {
        try
        {
            return Path.GetDirectoryName(typeof(TClass).Assembly.CodeBase + string.Empty).Replace(@"file:\", "");
        }
        catch (Exception ex)
        {
            throw new Exception("Exception when taking assembly path.", ex)
                .AddParameter(nameof(TClass), typeof(TClass).Name)
                .SetCode(101);
        }
    }
}
