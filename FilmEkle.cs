using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sinema
{
    public class FilmEkle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.FilmEkle.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Form = parameter as MainWindow;
                if (string.IsNullOrWhiteSpace(Form?.TxFilmAdi.Text))
                {
                    MessageBox.Show("Film İsmi Girmediniz.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                int.TryParse(Form?.TxSüre.Text, out var süre);
                var film = new Filmler
                {
                    FilmAdi = Form?.TxFilmAdi.Text,
                    Sure = süre,
                    Oyuncular = Form?.TxOyuncular.Text,
                    Yonetmeni = Form?.TxYönetmen.Text,
                    FilmTipi = Form?.CbFilmTipi.SelectionBoxItem?.ToString(),
                    CekimTipi = Form?.CbFilmÇekimTipi.SelectionBoxItem?.ToString(),
                    Dili = Form?.CbFilmDili.SelectionBoxItem.ToString(),
                    VideoYolu = Form?.C1VideoYolu.SelectedFile?.FullName,
                    ResimYolu = Form?.C1ResimYolu.SelectedFile?.FullName
                };

                var db = SinemaModel.entities;
                db.Filmler.Add(film);
                db.SaveChanges();
                Form?.UnifGridFilmEkle.Children.OfType<TextBox>().ToList().ForEach(z => z.Clear());
                Form?.UnifGridFilmEkle.Children.OfType<ComboBox>().ToList().ForEach(z => z.SelectedIndex = -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public partial class Filmler
    {
        public ICommand Ekle => new FilmEkle();
        public ICommand VideoResimGüncelle => new FilmVideoResimGüncelle();
    }
}