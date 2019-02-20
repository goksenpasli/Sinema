using C1.WPF;
using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class WebdenGetir : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public static string Translate(string text, string from, string to)
        {
            string page = null;
            try
            {
                if (BağlantıVarmı())
                {
                    var wc = new WebClient();
                    wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                    wc.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
                    wc.Encoding = Encoding.UTF8;
                    var url =
                        $"http://translate.google.com.tr/m?hl=en&sl={from}&tl={to}&ie=UTF-8&prev=_m&q={Uri.EscapeUriString(text)}";
                    page = wc.DownloadString(url);
                    page = page.Remove(0, page.IndexOf("<div dir=\"ltr\" class=\"t0\">"))
                        .Replace("<div dir=\"ltr\" class=\"t0\">", "");
                    page = page.Replace("&#39;", "'");
                    var last = page.IndexOf("</div>");
                    page = page.Remove(last, page.Length - last);
                    return page;
                }

                return text;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return text;
            }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
        }

        public void FilmBilgileri(object sender, PropertyChangedEventArgs<bool> e)
        {
            try
            {
                var Button = sender as C1DropDownButton;
                var FilmAdı = (Button.DataContext as Seanslar)?.Filmler.FilmAdi;
                var Webadress = $"http://www.omdbapi.com/?t={FilmAdı}&apikey=c30efe8&r=xml";
                var DatagridFilmDetayları = Button.FindName("DgMovieDetails") as DataGrid;
                (DatagridFilmDetayları.Resources["Film"] as XmlDataProvider).Source = new Uri(Webadress);
            }
            catch (Exception)
            {
            }
        }

        private static bool BağlantıVarmı()
        {
            try
            {
                var i = Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}