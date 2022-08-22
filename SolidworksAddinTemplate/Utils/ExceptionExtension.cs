using System.Reflection;

namespace SolidworksAddinTemplate.Utils;
#nullable enable

internal static class ExceptionExtension
{
    public static Exception AddParameter(this Exception exception, in string key, in string? value)
    {
        try
        {
            exception.Data.Add(key, value);
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception when adding parameter in exception.\nkey: {key}\nvalue: {value}", ex);
        }
        return exception;
    }

    public static Exception SetCode(this Exception exception, in int code)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        try
        {
            FieldInfo hresultFieldInfo = typeof(Exception).GetField("_HResult", flags);
            hresultFieldInfo.SetValue(exception, code);
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception when adding code (_Hresult) in exception.\nСode: {code}", ex);
        }
        return exception;
    }
}
