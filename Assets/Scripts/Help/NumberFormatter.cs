namespace TrapMaster
{
    public static class NumberFormatter
    {
        public static string Format(float number)
        {
            if (number >= 1_000_000)
                return (number / 1_000_000).ToString("0.##") + "m";

            if (number >= 1_000)
                return (number / 1_000).ToString("0.##") + "k";

            return number.ToString();
        }
    }
}