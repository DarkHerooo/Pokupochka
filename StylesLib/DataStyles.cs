using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StylesLib
{
    public static class DataStyles
    {
        public static Style? TblData = null!;
        public static Style? TbData = null!;
        public static Style? PbData = null!;
        public static Style? CbData = null!;
        public static Style? BrdImage = null!;

        public static void SetStyles()
        {
            ResourceDictionary resource = new();
            resource.Source = new Uri("/Styles/DataStyles.xaml", UriKind.Relative);

            TblData = resource["TblData"] as Style;
            TbData = resource["TbData"] as Style;
            PbData = resource["PbData"] as Style;
            CbData = resource["CbData"] as Style;
            BrdImage = resource["BrdImage"] as Style;
        }
    }
}
