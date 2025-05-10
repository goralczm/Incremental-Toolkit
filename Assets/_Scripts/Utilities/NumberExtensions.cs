using System;

namespace Core.Utility
{
    public static class NumberExtensions
    {
        public static string LimitDecimalPoints(this float value, int decimals)
        {
            if (value % 1 == 0)
                return value.ToString();

            return value.ToString($"n{decimals}");
        }

        public static string FormatToLowestNumber(this double value)
        {
            if (value >= 1000000000)
            {
                value = value / 1000000000d;
                return $"{Convert.ToSingle(value).LimitDecimalPoints(2)}G";
            }

            if (value >= 1000000)
            {
                value = value / 1000000d;
                return $"{Convert.ToSingle(value).LimitDecimalPoints(2)}M";
            }

            if (value >= 1000)
            {
                value = value / 1000d;
                return $"{Convert.ToSingle(value).LimitDecimalPoints(2)}K";
            }

            return $"{Convert.ToSingle(value).LimitDecimalPoints(2)}";
        }

        public static string FormatToLowestNumber(this float value)
        {
            if (value >= 1000000)
            {
                value = value / 1000000f;
                return $"{value.LimitDecimalPoints(2)}M";
            }

            if (value >= 1000)
            {
                value = value / 1000f;
                return $"{value.LimitDecimalPoints(2)}K";
            }

            return $"{value.LimitDecimalPoints(2)}";
        }
    }
}