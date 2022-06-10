namespace GameBlog.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
