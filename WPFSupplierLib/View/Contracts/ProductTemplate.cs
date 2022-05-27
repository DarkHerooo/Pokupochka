using GeneralLib;
using StylesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private byte[] _imageBytes = null!;
        private TextBox _tbName = null!;
        private TextBox _tbPrice = null!;

        public Border BrdProduct => _brdProduct;
        public byte[] ImageBytes => _imageBytes;
        private TextBox TbName => _tbName;
        private TextBox TbPrice => _tbPrice;

        public ProductTemplate()
        {
            _imageBytes = ImageReader.GetDefaultBytes();

            CreateTemplate();
        }

        /// <summary>
        /// Создаёт шаблон
        /// </summary>
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
                grProductTemplate.Margin = new Thickness(5);

                Border brdImage = new Border();
                brdImage.Style = DataStyles.BrdImage;

                BitmapImage bitmap = new();
                using (var ms = new System.IO.MemoryStream(_imageBytes))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                }

                brdImage.Background = new ImageBrush(bitmap);
                brdImage.Width = 128;
                brdImage.Height = 128;
                brdImage.Cursor = Cursors.Hand;
                brdImage.MouseDown += BrdImage_MouseDown;

                Grid grData = new();
                grData.RowDefinitions.Add(new RowDefinition());
                grData.RowDefinitions.Add(new RowDefinition());
                Grid.SetColumn(grData, 1);
                {
                    Grid grProductName = CreateGridData("Название продукции", _tbName);
                    Grid.SetRow(grProductName, 0);
                    grData.Children.Add(grProductName);

                    Grid grProductPrice = CreateGridData("Название продукции", _tbPrice);
                    Grid.SetRow(grProductPrice, 1);
                    grData.Children.Add(grProductPrice);
                }
                grProductTemplate.Children.Add(grData);

                _brdProduct.Child = grProductTemplate;
            }
        }

        private Grid CreateGridData(string title, TextBox tbData)
        {
            Grid grData = new();
            grData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            grData.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblProductName = new();
                tblProductName.Margin = new Thickness(5, 0, 0, 0);
                tblProductName.Text = title;
                Grid.SetRow(tblProductName, 0);
                grData.Children.Add(tblProductName);

                tbData = new();
                tbData.Margin = new Thickness(5);
                Grid.SetRow(tbData, 1);
                grData.Children.Add(tbData);
            }

            return grData;
        }


        private void BrdImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
