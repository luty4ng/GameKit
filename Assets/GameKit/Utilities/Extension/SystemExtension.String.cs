namespace GameKit
{
    public static partial class SystemExtension
    {
        public static string Correction(this string str)
        {
            return str.Trim().Replace('\u200B'.ToString(), "");
        }

        public static string RemoveBracket(this string str)
        {
            return str.Replace("(", "").Replace(")", "");
        }
    }
}
