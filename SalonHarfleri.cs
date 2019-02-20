using System;
using System.Collections.Generic;

namespace Sinema
{
    public partial class Salonlar
    {
        public static IEnumerable<string> SalonHarfleri()
        {
            for (var digits = 1; ; digits++)
            {
                var iterations = (int)Math.Pow(26, digits);
                for (var n = 0; n < iterations; n++)
                {
                    var değer = HarfeDöndür(n);
                    yield return değer.PadLeft(digits, 'A');
                }
            }
        }

        private static string HarfeDöndür(int counter)
        {
            var stack = new Stack<char>();
            while (counter > 0)
            {
                stack.Push(RakamdanHarfe(counter % 26));
                counter /= 26;
            }

            return new string(stack.ToArray());
        }

        private static char RakamdanHarfe(int digit) => (char)('A' + digit);
    }
}