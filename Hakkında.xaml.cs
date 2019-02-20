using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    /// <summary>
    ///     Interaction logic for Hakkında.xaml
    /// </summary>
    public partial class Hakkında : Window
    {
        public Hakkında() => InitializeComponent();

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            Process.Start("https://www.facebook.com/profile.php?id=100002547964778");
        }
    }
}