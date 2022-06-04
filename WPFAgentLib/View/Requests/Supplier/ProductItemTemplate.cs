using DbLib.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using StylesLib;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WPFAgentLib.View.Requests.Supplier
{
    public class ProductItemTemplate
    {
        private Grid _gridProductItemTemplate = null!;
        private Product _product = null!;
        private TextBox _tbCount = null!;
        private TextBlock _tblPrice = null!;
        private decimal _price;

        public Grid GridProductItemTemplate => _gridProductItemTemplate!;
        public Product Product => _product;
        public TextBox TbCount => _tbCount;
        public decimal Price => _price;

        public ProductItemTemplate(Product product)
        {
            _product = product;
            CreateGridProductItemTemplate();
        }

        private void CreateGridProductItemTemplate()
        {
            _gridProductItemTemplate = new();
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(64) });
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition());
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

            Border brdImage = new();
            brdImage.Style = DataStyles.BrdImage;
            brdImage.Width = 64;
            brdImage.Height = 64;
            brdImage.Background = GetImageBrush();
            Grid.SetColumn(brdImage, 0);
            _gridProductItemTemplate.Children.Add(brdImage);

            TextBlock tblName = new();
            tblName.VerticalAlignment = VerticalAlignment.Center;
            tblName.Margin = new Thickness(5);
            tblName.FontSize = 20;
            tblName.FontWeight = FontWeights.Bold;
            tblName.Text = _product.Title;
            Grid.SetColumn(tblName, 1);
            _gridProductItemTemplate.Children.Add(tblName);

            Grid gridData = new();
            gridData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
            gridData.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblCount = new();
                tblCount.Text = "Кол-во";
                tblCount.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(tblCount, 0);
                gridData.Children.Add(tblCount);

                _tbCount = new();
                _tbCount.Style = DataStyles.TbData;
                _tbCount.Margin = new Thickness(5);
                _tbCount.MaxLength = 5;
                _tbCount.Text = "1";
                _tbCount.PreviewTextInput += _tbCount_PreviewTextInput;
                _tbCount.TextChanged += _tbCount_TextChanged;
                Grid.SetRow(_tbCount, 1);
                gridData.Children.Add(_tbCount);
            }
            Grid.SetColumn(gridData, 2);
            _gridProductItemTemplate.Children.Add(gridData);

            _tblPrice = new();
            _tblPrice.HorizontalAlignment = HorizontalAlignment.Center;
            _tblPrice.VerticalAlignment = VerticalAlignment.Center;
            _tblPrice.FontSize = 20;
            _tblPrice.FontWeight = FontWeights.Bold;
            _tbCount_TextChanged(null!, null!);
            Grid.SetColumn(_tblPrice, 3);
            _gridProductItemTemplate.Children.Add(_tblPrice);
        }

        private ImageBrush GetImageBrush()
        {
            BitmapImage bitmap = new();
            using (var mem = new MemoryStream(_product.Image!))
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

        private void _tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count;
            if (!int.TryParse(_tbCount.Text, out _)) count = 1;
            else if (int.Parse(_tbCount.Text) <= 0) count = 1;
            else count = int.Parse(_tbCount.Text);

            _price = _product.Price * count;
            _tblPrice.Text = _price + "₽";
        }

        private void _tbCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char letter = e.Text[0];
            if (!char.IsDigit(letter))
                e.Handled = true;
        }
    }
}
