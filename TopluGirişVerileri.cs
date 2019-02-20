using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Sinema
{
    public class RegexDoğrula : ValidationRule
    {
        public string Expression { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(Expression)) return new ValidationResult(false, "HATALI GİRİŞ.");
            var match = new Regex(Expression).Match(value.ToString());
            return match == Match.Empty ? new ValidationResult(false, "HATALI GİRİŞ.") : ValidationResult.ValidResult;
        }
    }

    public class TopluGirişVerileri

    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Yaş { get; set; }

        public static List<TopluGirişVerileri> Liste(string metin)
        {
            try
            {
                var regex = new Regex(
                    @"^([A-Z,\u011e,\u0130,\u00d6,\u00dc,\u015e,\u00c7]{1,})[ ]([A-Z,\u011e,\u0130,\u00d6,\u00dc,\u015e,\u00c7]{1,})[ ](\d*)$");
                var veriler = new List<TopluGirişVerileri>();
                foreach (var i in metin.Trim()
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!regex.IsMatch(i)) return null;
                    veriler.Add(new TopluGirişVerileri
                    {
                        Ad = i.Split(' ').ElementAtOrDefault(0),
                        Soyad = i.Split(' ').ElementAtOrDefault(1),
                        Yaş = Convert.ToInt32(i.Split(' ').ElementAtOrDefault(2))
                    });
                }

                return veriler;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}