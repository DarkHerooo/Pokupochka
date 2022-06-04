using DbLib.DB.Entity;
using GeneralLib;
using StylesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFAgentLib.View.Requests.Supplier
{
    public class RequestTemplate
    {
        private Border _brdRequest = null!;
        private Request _request = null!;

        public Border BrdRequest => _brdRequest;
        public Request Request => _request;

        public RequestTemplate(Request request)
        {
            _request = request;
            CreateTemplate();
        }

        public void CreateTemplate()
        {
            _brdRequest = new();
            _brdRequest.Style = DataStyles.BrdImage;
            _brdRequest.Background = Brushes.White;
            _brdRequest.Width = 200;
            _brdRequest.Height = 220;
            {
                Grid grid = new();
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                _brdRequest.Child = grid;
                {
                    Border brdImage = new();
                    brdImage.Style = DataStyles.BrdImage;
                    brdImage.BorderThickness = new Thickness(0);
                    string imgUri = _request.Id != 0 ? "/Images/request_template.png" : "/Images/new_template.png";
                    BitmapImage bitmap = new BitmapImage(new Uri(AppPath.Path + imgUri));
                    brdImage.Background = new ImageBrush(bitmap);
                    brdImage.Width = 128;
                    brdImage.Height = 128;
                    brdImage.Margin = new Thickness(5);
                    Grid.SetRow(brdImage, 0);
                    grid.Children.Add(brdImage);

                    TextBlock tblNumber = new();
                    tblNumber.FontSize = 20;
                    tblNumber.HorizontalAlignment = HorizontalAlignment.Center;
                    tblNumber.FontWeight = FontWeights.Bold;
                    tblNumber.Text = _request.Id != 0 ? "#" + _request.Number : "Новый";
                    Grid.SetRow(tblNumber, 1);
                    grid.Children.Add(tblNumber);

                    if (_request.Id != 0)
                    {
                        TextBlock tblStatus = new();
                        tblStatus.FontSize = 20;
                        tblStatus.HorizontalAlignment = HorizontalAlignment.Center;
                        tblStatus.Text = _request.Status!.Title;
                        tblStatus.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(_request.Status!.Color!)!;
                        Grid.SetRow(tblStatus, 3);
                        grid.Children.Add(tblStatus);
                    }
                }

                _brdRequest.MouseEnter += _brdContract_MouseEnter;
                _brdRequest.MouseLeave += _brdContract_MouseLeave;
            }
        }

        private void _brdContract_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _brdRequest.Background = Brushes.White;
        }

        private void _brdContract_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _brdRequest.Background = Brushes.LightGreen;
        }
    }
}
