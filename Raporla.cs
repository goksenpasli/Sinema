using FastReport;
using FastReport.Utils;
using System;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Sinema
{
    public class Raporla
    {

        public Raporla(string streamname, string param, object value)
        {
            try
            {
                System.Windows.Forms.IWin32Window win32Window = new NativeWindow();
                ((NativeWindow)win32Window).AssignHandle(new WindowInteropHelper(System.Windows.Application.Current.Windows[0] as MainWindow).Handle);
                using (var sinemarapor = GetType().Assembly.GetManifestResourceStream(streamname))
                {
                    using (var language = GetType().Assembly.GetManifestResourceStream("Sinema.Rapor.Turkish.frl"))
                    {
                        using (var report = new Report())
                        {
                            Res.LoadLocale(language);
                            report.Load(sinemarapor);
                            report.SetParameterValue("Parameter",
                                SinemaModel.entities.Database.Connection.ConnectionString);
                            report.SetParameterValue(param, value);
                            Config.ReportSettings.ShowPerformance = false;
                            Config.PreviewSettings.Buttons = PreviewButtons.All & ~PreviewButtons.Edit;
                            report.Show(true, win32Window);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                System.Windows.MessageBox.Show(Ex.Message,"Sinema", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
        }

        public Raporla(string streamname)
        {
            try
            {
                System.Windows.Forms.IWin32Window win32Window = new NativeWindow();
                ((NativeWindow)win32Window).AssignHandle(new WindowInteropHelper(System.Windows.Application.Current.Windows[0] as MainWindow).Handle);
                using (var sinemarapor = GetType().Assembly.GetManifestResourceStream(streamname))
                {
                    using (var language = GetType().Assembly.GetManifestResourceStream("Sinema.Rapor.Turkish.frl"))
                    {                    
                        using (var report = new Report())
                        {
                            Res.LoadLocale(language);
                            report.Load(sinemarapor);
                            report.SetParameterValue("Parameter",
                                SinemaModel.entities.Database.Connection.ConnectionString);
                            Config.ReportSettings.ShowPerformance = false;
                            Config.PreviewSettings.Buttons = PreviewButtons.All & ~PreviewButtons.Edit;
                            report.Show(true,win32Window);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                System.Windows.MessageBox.Show(Ex.Message,"Sinema", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
        }
    }
}