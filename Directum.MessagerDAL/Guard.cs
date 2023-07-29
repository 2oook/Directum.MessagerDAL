namespace Directum.MessagerDAL
{
    internal class Guard
    {
        public static void IsNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static void IsNull(string value)
        {
            if (value == null) throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}