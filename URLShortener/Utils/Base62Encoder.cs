namespace URLShortener.Utils
{
    using System.Text;

    public static class Base62Encoder
    {
        private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Dictionary<char, int> CharMap = Alphabet
            .Select((c, i) => new { c, i })
            .ToDictionary(x => x.c, x => x.i);

        public static string Encode(long number)
        {
            if (number == 0) return "0";

            var result = new StringBuilder();
            while (number > 0)
            {
                result.Insert(0, Alphabet[(int)(number % 62)]);
                number /= 62;
            }
            return result.ToString();
        }

        public static long Decode(string input)
        {
            long result = 0;
            foreach (var c in input)
            {
                result = result * 62 + CharMap[c];
            }
            return result;
        }
    }
}
