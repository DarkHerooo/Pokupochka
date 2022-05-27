using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StylesLib
{
    public static class UserStyles
    {
        public static Style? WindowSyle;
        public static Style? DefaultButtonStyle;
        public static Style? SelectButtonStyle;

        public static void SetUserStyles(string xamlFile)
        {
            string xamlUri = "/UserStyles/" + xamlFile;

            ResourceDictionary resource = new();
            resource.Source = new Uri(xamlUri, UriKind.Relative);

            WindowSyle = resource["WindowStyle"] as Style;
            DefaultButtonStyle = resource["DefaultButtonStyle"] as Style;
            SelectButtonStyle = resource["SelectButtonStyle"] as Style;
        }
    }
}
