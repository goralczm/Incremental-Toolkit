namespace Core.Utility
{
    public static class FloatExtensions
    {
        public static string LimitDecimalPoints(this float value, int decimals)
        {
            if (value % 1 == 0)
                return value.ToString();

            return value.ToString($"n{decimals}");
        }
    }
}