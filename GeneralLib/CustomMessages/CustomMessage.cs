using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace GeneralLib.CustomMessages
{
    public class CustomMessage
    {
        private void SetImgAndBrush(MessageType type, ref string imgUri, ref Brush brush)
        {
            switch (type)
            {
                case MessageType.Error:
                    imgUri += "error.png";
                    brush = Brushes.Red;
                    break;
                case MessageType.Loading:
                    imgUri += "loading.gif";
                    brush = Brushes.Blue;
                    break;
            }
        }

        public async Task ShowMessage(StackPanel sp, MessageType type, string text)
        {
            string imgUri = "/Images/Messages/";
            Brush brush = null!;
            SetImgAndBrush(type, ref imgUri, ref brush);

            if (sp.Children.Count > 0)
                sp.Children.Clear();

            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imgUri, UriKind.Relative);
            bitmap.EndInit();

            Image img = new();
            ImageBehavior.SetAnimatedSource(img, bitmap);
            img.Width = 20;
            img.Height = 20;
            sp.Dispatcher.Invoke(() => sp.Children.Add(img));

            TextBlock tbl = new TextBlock();
            tbl.VerticalAlignment = VerticalAlignment.Center;
            tbl.Margin = new Thickness(5, 0, 0, 0);
            tbl.Foreground = brush;
            tbl.FontWeight = FontWeights.Bold;
            tbl.FontSize = 15;
            tbl.Text = text;
            sp.Dispatcher.Invoke(() => sp.Children.Add(tbl));

            await Task.Delay(2000);
        }
    }
}
