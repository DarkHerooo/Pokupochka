using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLib
{
    public static class AppPath
    {
        public static string Path = Get();
        private static string Get()
        {
            string path = Assembly.GetExecutingAssembly().Location;

            int countSlashes = 0;
            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '/' || path[i] == '\\') countSlashes++;

                if (countSlashes == 4)
                {
                    path = path.Remove(i + 1);
                    break;
                }
            }

            return path;
        }
    }
}
