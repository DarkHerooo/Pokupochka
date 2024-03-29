﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB
{
    public static class AdditionalFields
    {
        public static string GetFIO(string secondName, string firstName, string? patronymic)
        {
            string corSecondName = "";
            for (int i = 0; i < secondName.Length; i++)
            {
                corSecondName += i == 0 ? char.ToUpper(secondName[i]) : char.ToLower(secondName[i]);
            }
            corSecondName = secondName.Trim();

            string corFirstName = "";
            for (int i = 0; i < firstName.Length; i++)
            {
                corFirstName += i == 0 ? char.ToUpper(firstName[i]) : char.ToLower(firstName[i]);
            }
            corFirstName = firstName.Trim();

            string corPatronymicName = "";
            if (patronymic != null)
            {
                for (int i = 0; i < patronymic.Length; i++)
                {
                    corPatronymicName += i == 0 ? char.ToUpper(patronymic[i]) : char.ToLower(patronymic[i]);
                }
                corPatronymicName = patronymic.Trim();
            }

            return corSecondName + " " + corFirstName + " " + corPatronymicName;
        }

        public static string GetClearPhone(string phone)
        {
            string clearPhone = "";
            foreach (var letter in phone)
            {
                if (char.IsDigit(letter))
                    clearPhone += letter;
            }

            return clearPhone;
        }
    }
}
