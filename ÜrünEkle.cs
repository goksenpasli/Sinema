using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public partial class Urunler : IDataErrorInfo
    {
        public string Error => "";

        public ICommand ÜrünEkle => new ÜrünEkle();

        public ICommand ÜrünGüncelle => new ÜrünGüncelle();

        public ICommand ÜrünSil => new ÜrünSil();

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "UrunAdi")
                {
                    if (string.IsNullOrWhiteSpace(UrunAdi))
                        result = "Ürün Adını Yazın.";
                }

                return result;
            }
        }
    }

    public class ÜrünEkle : ICommand
    {
        private MainWindow Form;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ÜrünEkle.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                Form = Application.Current.Windows[0] as MainWindow;
                if (string.IsNullOrWhiteSpace(Form.TxUrunAdi.Text)
                    || string.IsNullOrWhiteSpace(Form.C1UrunFiyati.Value.ToString()))
                {
                    MessageBox.Show("Hatalı Girişleri Düzeltin.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                var db = SinemaModel.entities;
                if (db.Set<Urunler>().Any(z => z.UrunAdi == Form.TxUrunAdi.Text))
                {
                    MessageBox.Show("Bu İsimde Ürün Oluşturulmuştur. Başka Bir İsim Girin.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var ürün = new Urunler
                {
                    UrunAdi = Form.TxUrunAdi.Text,
                    BirimFiyati = Form.C1UrunFiyati.Value,
                    UrunSatilabilirmi = true
                };

                db.Urunler.Add(ürün);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}