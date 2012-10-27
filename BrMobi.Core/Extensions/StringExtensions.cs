namespace BrMobi.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SetParam(this string current, string paramName, string paramValue)
        {
            paramName = string.Concat("{", paramName, "}");

            return current.Replace(paramName, paramValue);
        }
    }
}