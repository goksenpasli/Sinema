using System.Windows;

namespace Sinema
{
    /// <summary>
    ///     Interaction logic for GirişWindow.xaml
    /// </summary>
    public partial class GirişWindow : Window
    {
        public static double Kalite { get; set; } = 65;

        public GirişWindow()
        {

    
                InitializeComponent();
        }

        private void ImgClose_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => Close();
    }
}