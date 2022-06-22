using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GeneralLib
{
    public class ImageDialog
    {
        public byte[]? ImageBytes;
        public bool Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, *.JPEG, *.PNG)|*.bmp;*.jpg;*.jpeg;*.png;";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open))
                    {
                        ImageBytes = new byte[fs.Length];
                        fs.Read(ImageBytes, 0, ImageBytes.Length);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else return false;
        }
    }
}
