using DbLib.DB.Entity;
using GeneralLib;
using StylesLib;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace WPFCounterpartyLib.View.Contracts
{
    public class ContractTemplate
    {
        private Border _brdContract = null!;
        private Contract _contract = null!;

        public Border BrdContract => _brdContract;
        public Contract Contract => _contract;

        public ContractTemplate(Contract contract)
        {
            _contract = contract;
            CreateTemplate();
        }

        public void CreateTemplate()
        {
            _brdContract = new();
            _brdContract.Style = DataStyles.BrdImage;
            _brdContract.Background = Brushes.White;
            _brdContract.Width = 200;
            _brdContract.Height = 220;
            {
                Grid grid = new();
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                _brdContract.Child = grid;
                {
                    Border brdImage = new();
                    brdImage.Style = DataStyles.BrdImage;
                    brdImage.BorderThickness = new Thickness(0);
                    string imgUri = _contract.Id != 0 ? "/Images/contract_template.png" : "/Images/new_contract_template.png";
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
                    tblNumber.Text = _contract.Id != 0 ? "#" + _contract.Number : "Новый";
                    Grid.SetRow(tblNumber, 1);
                    grid.Children.Add(tblNumber);

                    if (_contract.Id != 0)
                    {
                        TextBlock tblStatus = new();
                        tblStatus.FontSize = 20;
                        tblStatus.HorizontalAlignment = HorizontalAlignment.Center;
                        tblStatus.Text = _contract.Status!.Title;
                        tblStatus.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(_contract.Status!.Color!)!;
                        Grid.SetRow(tblStatus, 2);
                        grid.Children.Add(tblStatus);
                    }
                }

                _brdContract.MouseEnter += _brdContract_MouseEnter;
                _brdContract.MouseLeave += _brdContract_MouseLeave;
            }
        }

        private void _brdContract_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _brdContract.Background = Brushes.White;
        }

        private void _brdContract_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _brdContract.Background = Brushes.LightGreen;
        }
    }
}
