using System;
using System.IO;

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

        public static byte[] GetDefaultBytes()
        {
            byte[] bytes = GetBytes("Images/not_found.png");

            return bytes;
        }
    }
}
