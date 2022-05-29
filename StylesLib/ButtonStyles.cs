using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StylesLib
{
    public class ButtonStyles
    {
        public static Style? ButtonTemplate = null!;
        public static Style? RedButton = null!;
        public static Style? YellowButton = null!;
        public static Style? GreenButton = null!;
        public static Style? BlueButton = null!;
        public static Style? BackButton = null!;

        public static void SetStyles()
        {
            ResourceDictionary resource = new();
            resource.Source = new Uri("/Styles/ButtonStyles.xaml", UriKind.Relative);

            ButtonTemplate = resource["ButtonTemplate"] as Style;
            RedButton = resource["RedButton"] as Style;
            YellowButton = resource["YellowButton"] as Style;
            GreenButton = resource["GreenButton"] as Style;
            BlueButton = resource["BlueButton"] as Style;
            BackButton = resource["BackButton"] as Style;
        }
    }
}
