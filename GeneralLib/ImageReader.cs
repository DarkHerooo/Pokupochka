using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GeneralLib
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
    }
}
