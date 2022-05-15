using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PokupochkaCompany.Classes
{
    public class ImageDialog
    {
        public byte[]? ImageBytes;
        public bool Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, *.PNG)|*.bmp;*.jpg;*.png;";

            if (dialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open))
                {
                    ImageBytes = new byte[fs.Length];
                    fs.Read(ImageBytes, 0, ImageBytes.Length);
                    return true;
                }
            }
            else return false;
        }
    }
}
