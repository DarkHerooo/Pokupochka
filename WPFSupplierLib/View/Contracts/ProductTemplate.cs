using GeneralLib;
using StylesLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFSupplierLib.View.Contracts
{
    public class ProductTemplate
    {
        private Border _brdProduct = null!;
        private TextBox _tbName = null!;
        private TextBox _tbPrice = null!;
        private Border _brdImage = null!;
        private Button _btnDetete = null!;
        private byte[] _imageBytes = null!;

        public Border BrdProduct => _brdProduct;
        public TextBox TbName => _tbName;
        public TextBox TbPrice => _tbPrice;
        public Button BtnDelete => _btnDetete;
        public byte[] ImageBytes => _imageBytes;

        public ProductTemplate()
        {
            _imageBytes = ImageReader.GetDefaultBytes();

            CreateTemplate();
        }

        private void CreateTemplate()
        {
            _brdProduct = new();
            _brdProduct.BorderBrush = Brushes.Black;
            _brdProduct.BorderThickness = new Thickness(2);
            _brdProduct.Margin = new Thickness(5);
            _brdProduct.Height = 150;
            {
                Grid grProductTemplate = new();
                grProductTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
                grProductTemplate.ColumnDefinitions.Add(new ColumnDefinition());
                grProductTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
                grProductTemplate.Margin = new Thickness(5);
                _brdProduct.Child = grProductTemplate;
                {
                    _brdImage = CreateImgBorder();
                    Grid.SetColumn(_brdImage, 0);
                    grProductTemplate.Children.Add(_brdImage);

                    Grid grData = new();
                    grData.RowDefinitions.Add(new RowDefinition());
                    grData.RowDefinitions.Add(new RowDefinition());
                    Grid.SetColumn(grData, 1);
                    {
                        _tbName = new();
                        Grid grProductName = CreateGridData("Название:", _tbName);
                        Grid.SetRow(grProductName, 0);
                        grData.Children.Add(grProductName);

                        _tbPrice = new();
                        Grid grProductPrice = CreateGridData("Цена:", _tbPrice);
                        _tbPrice.PreviewTextInput += _tbPrice_PreviewTextInput;
                        Grid.SetRow(grProductPrice, 1);
                        grData.Children.Add(grProductPrice);

                        grProductTemplate.Children.Add(grData);
                    }

                    _btnDetete = new();
                    _btnDetete.Margin = new Thickness(5);
                    _btnDetete.Style = ButtonStyles.RedButton;
                    _btnDetete.Content = "X";
                    _btnDetete.FontSize = 40;
                    Grid.SetColumn(_btnDetete, 2);
                    grProductTemplate.Children.Add(_btnDetete);
                }
            }
        }

        private Border CreateImgBorder()
        {
            Border brdImage = new();
            brdImage.Style = DataStyles.BrdImage;
            brdImage.Width = 128;
            brdImage.Height = 128;
            brdImage.Cursor = Cursors.Hand;
            brdImage.MouseDown += BrdImage_MouseDown;
            brdImage.Background = GetImageBrush();

            return brdImage;
        }

        private Grid CreateGridData(string title, TextBox tbData)
        {
            Grid grData = new();
            grData.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            grData.ColumnDefinitions.Add(new ColumnDefinition());
            {
                TextBlock tblProductName = new();
                tblProductName.Style = DataStyles.TblData;
                tblProductName.Text = title;
                Grid.SetColumn(tblProductName, 0);
                grData.Children.Add(tblProductName);

                tbData.Style = DataStyles.TbData;
                tbData.Margin = new Thickness(5);
                Grid.SetColumn(tbData, 1);
                grData.Children.Add(tbData);
            }

            return grData;
        }

        private ImageBrush GetImageBrush()
        {
            BitmapImage bitmap = new();
            using (var mem = new MemoryStream(_imageBytes))
            {
                mem.Position = 0;
                bitmap.BeginInit();
                bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = null;
                bitmap.StreamSource = mem;
                bitmap.EndInit();
            }
            bitmap.Freeze();

            return new ImageBrush(bitmap);
        }

        public bool CheckData()
        {
            bool trueData = true;

            if (String.IsNullOrEmpty(TbName.Text) || String.IsNullOrWhiteSpace(TbName.Text))
                trueData = false;

            if (!double.TryParse(TbPrice.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                trueData = false;
            else if (double.Parse(TbPrice.Text, NumberStyles.Any, CultureInfo.InvariantCulture) < 0)
                trueData = false;

            return trueData;
        }

        private void BrdImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageDialog dialog = new();

            if (dialog.Open())
            {
                _imageBytes = dialog.ImageBytes!;
                _brdImage.Background = GetImageBrush();
            }
        }

        private void _tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = e.Text;
            Regex regex = new Regex("[^0-9.]+");

            if (regex.IsMatch(text))
                e.Handled = true;
        }
    }
}
