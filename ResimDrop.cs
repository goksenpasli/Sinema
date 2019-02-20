using C1.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class ResimDrop : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Drop(object sender, DragEventArgs e)
        {
            try
            {
                var Extension = new List<string> { ".jpg", ".webp", ".bmp", ".png", ".tif" };
                var FilePicker = sender as C1FilePicker;
                var Dosyalar = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Extension.Contains(Path.GetExtension(Dosyalar[0]).ToLower()))
                    FilePicker.SelectedFile = new FileInfo(Dosyalar[0]);
            }
            catch (Exception)
            {
            }
        }

        public void Execute(object parameter)
        {
        }
    }
}