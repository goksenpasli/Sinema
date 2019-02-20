using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sinema
{
    public static class Doğrula
    {
        public static readonly MainWindow Form = Application.Current.Windows[0] as MainWindow;

        public static bool Geçerli(this DependencyObject parent)
        {
            if (Validation.GetHasError(parent)) return false;

            for (var i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (!Geçerli(child)) return false;
            }

            return true;
        }
    }
}