using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TemplateEngine.Docx;

namespace WPFAgentLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для ShowContractPage.xaml
    /// </summary>
    public partial class SupShowContractPage : Page
    {
        Contract _contract;
        public SupShowContractPage(Contract contract)
        {
            InitializeComponent();

            _contract = contract;
            SetPageSettings();
        }

        public void SetPageSettings()
        {
            if (_contract.StatusId == (int)StatusKey.Considered)
            {
                BtnAccept.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;
            }
            else if (_contract.StatusId == (int)StatusKey.Active)
            {
                BtnAccept.Visibility = Visibility.Hidden;
                Grid.SetColumn(BtnCancel, 1);
                Grid.SetColumnSpan(BtnCancel, 2);
            }
            else if (_contract.StatusId == (int)StatusKey.Cancel)
            {
                BtnCancel.Visibility = Visibility.Hidden;
                Grid.SetColumn(BtnAccept, 1);
                Grid.SetColumnSpan(BtnAccept, 2);
            }
            else
            {
                BtnAccept.Visibility = Visibility.Hidden;
                BtnCancel.Visibility = Visibility.Hidden;
            }

            DgProducts.ItemsSource = _contract.Products;
            DataContext = _contract;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _contract.StatusId = (int)StatusKey.Cancel;
            _contract.AddOrChange();

            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            _contract.StatusId = (int)StatusKey.Active;
            _contract.AddOrChange();

            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "ContractTemplate.docx";
            File.Copy(AppPath.Path + "/Docs/" + fileName, fileName, true);

            TableContent productTable = new("Products");
            foreach (var product in _contract.Products)
            {
                productTable.AddRow(
                    new FieldContent("ProductName", product.Title),
                    new FieldContent("ProductPrice", product.Price.ToString())
                    );
            }

            var content = new Content(
                    new FieldContent("Number", _contract.Number.ToString()),
                    new FieldContent("Address", _contract.Counterparty!.Address),
                    new FieldContent("DateStart", _contract.ShortDateStart),
                    new FieldContent("SupCompany", _contract.Counterparty!.Company),
                    new FieldContent("SupFIO", _contract.Counterparty!.FIO),
                    new FieldContent("CliCompany", "ООО Покупочка"),
                    new FieldContent("CliFIO", "Коршунов Артём Леонидович"),
                    productTable,
                    new FieldContent("DateOver", _contract.ShortDateOver)
                    );

            using (var doc = new TemplateProcessor(fileName).SetRemoveContentControls(true))
            {
                doc.FillContent(content);
                doc.SaveChanges();
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }
        }
    }
}
