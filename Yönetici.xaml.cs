using System.Windows;

namespace Sinema
{
    /// <summary>
    ///     Interaction logic for Yönetici.xaml
    /// </summary>
    public partial class Yönetici : Window
    {
        public Yönetici() => InitializeComponent();

        private void OnCloseExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => Close();

    }

}