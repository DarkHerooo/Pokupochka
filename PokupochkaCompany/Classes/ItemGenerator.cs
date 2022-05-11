using DbLib;
using PokupochkaCompany.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokupochkaCompany.Classes
{
    public class ItemGenerator
    {
        public StackPanel GenerateItem(string? image, string? text)
        {
            StackPanel sp = new();
            sp.Orientation = Orientation.Horizontal;

            Border brd = new();
            brd.BorderThickness = new Thickness(2);
            brd.BorderBrush = Brushes.Black;
            brd.CornerRadius = new CornerRadius(5);
            brd.Width = 32;
            brd.Height = 32;
            brd.Background = new ImageBrush(new BitmapImage(new Uri(AppPath.Path + image, UriKind.RelativeOrAbsolute)));
            sp.Children.Add(brd);

            TextBlock tb = new();
            tb.Margin = new Thickness(5, 0, 0, 0);
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Text = text;
            sp.Children.Add(tb);

            return sp;
        }

        public string GetText(StackPanel sp)
        {          
            string title = "";
            foreach (var child in sp.Children)
            {
                if (child is TextBlock)
                {
                    TextBlock tb = (TextBlock)child;
                    title = tb.Text;
                    break;
                }
            }

            return title;
        }

        public object GetItemDependingText(ItemCollection items, string text)
        {
            foreach (var item in items)
            {
                StackPanel sp = (StackPanel)item;
                string title = GetText(sp);
                if (title == text)
                {
                    return item;
                }
            }

            return null!;
        }
    }
}
