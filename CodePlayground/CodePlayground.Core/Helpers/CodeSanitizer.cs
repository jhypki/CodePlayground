using System.Text;

namespace CodePlayground.Core.Helpers;

public static class CodeSanitizer
{
    public static string Sanitize(string code)
    {
        return code
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "");
    }

    public static string ToBase64(string code)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
    }
}