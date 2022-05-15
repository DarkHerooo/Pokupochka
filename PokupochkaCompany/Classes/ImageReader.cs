using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PokupochkaCompany.Classes
{
    public class ImageReader
    {
        public static byte[] GetBytes(string imgUri)
        {
            using (FileStream fs = new FileStream(imgUri, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        /*public static Size GetSize(byte[] bytes)
        {
            Image image = new();
            using (var stream = new MemoryStream(bytes))
            {
                BitmapImage bitmap = new();
                
                stream.Position = 0;
                bitmap.BeginInit();
                bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = null;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                image.Source = bitmap;
            }

            Size size = new();
            size.Width = image.Width;
            size.Height = image.Height;
            return size;
        }*/
    }
}
