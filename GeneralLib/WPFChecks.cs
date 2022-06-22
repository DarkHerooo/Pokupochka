using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLib
{
    public class WPFChecks
    {
        public static string CheckEmail(string email)
        {
            bool trueEmail = true;

            if (trueEmail)
                trueEmail = email.Contains('@');

            if (trueEmail)
                trueEmail = email.Length > 10;

            string message = "";
            if (!trueEmail)
                message = "Почта введена неверно!\n";

            return message;
        }
        public static string CheckPassword(string password)
        {
            bool truePassword = true;
            if (truePassword)
                truePassword = password.Length > 8;

            if (truePassword)
            {
                string passwordSymvols = "!@#$%^";
                foreach (var symvol in passwordSymvols)
                {
                    if (password.Contains(symvol))
                    {
                        truePassword = true;
                        break;
                    }
                    else truePassword = false;
                }
            }

            if (truePassword)
            {
                foreach (var symvol in password)
                {
                    if (char.IsDigit(symvol))
                    {
                        truePassword = true;
                        break;
                    }
                    else truePassword = false;
                }
            }

            if (truePassword)
            {
                foreach (var symvol in password)
                {
                    if (char.IsUpper(symvol))
                    {
                        truePassword = true;
                        break;
                    }
                    else truePassword = false;
                }
            }

            string message = "";
            if (!truePassword)
            {
                message = "Пароль должен отвечать следующим требованиям:\n" +
                    "Минимум 8 символов\n" +
                    "Минимум 1 прописная буква\n" +
                    "Минимум 1 цифра\n" +
                    "По крайней мере один из следующих символов: ! @ # $ % ^\n";
            }

            return message;
        }
    }
}
