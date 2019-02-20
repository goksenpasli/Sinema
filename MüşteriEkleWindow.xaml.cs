using System.Windows;

namespace Sinema
{
    /// <summary>
    ///     Interaction logic for MüşteriEkleWindow.xaml
    /// </summary>
    public partial class MüşteriEkleWindow : Window
    {
        public MüşteriEkleWindow() => InitializeComponent();

        private void OnCloseExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => Close();
    }
}