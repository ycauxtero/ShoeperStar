namespace ShoeperStar.Data.Services
{
    public static class StringExtensions
    {
        public static string ToSlug(this string s)
        {
            var stringArray = s.Split(' ');
            return string.Join('-', stringArray).ToLower();
        }
    }
}
