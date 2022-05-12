using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PokupochkaCompany.Classes
{
    public class PhoneTextBox
    {
        private TextBox _tb;

        public PhoneTextBox(TextBox tb)
        {
            _tb = tb;
            _tb.PreviewTextInput += _tb_PreviewTextInput;
            _tb.PreviewKeyDown += _tb_PreviewKeyDown;
            _tb.TextChanged += _tb_TextChanged;

            SetPhone(tb.Text);
        }

        private void SetPhone(string phone)
        {
            string mask = "+_ (___) ___ __ __";
            string newPhone = "";

            int lastIndex = 0;
            foreach (char number in phone)
            {
                for (int i = lastIndex; i < mask.Length; i++)
                {
                    if (mask[i] == '_')
                    {
                        newPhone += number;
                        lastIndex = i + 1;
                        break;
                    }
                    else
                    {
                        newPhone += mask[i];
                        continue;
                    }
                }
            }

            if (mask.Length != newPhone.Length)
            {
                for (int i = newPhone.Length; i < mask.Length; i++)
                    newPhone += mask[i];
            }

            _tb.Text = newPhone;
            _tb.CaretIndex = lastIndex;
        }

        public string GetPhone()
        {
            string phone = "";
            foreach (char letter in _tb.Text)
            {
                if (char.IsDigit(letter))
                    phone += letter;
            }

            return phone;
        }

        private void _tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char keyChar = e.Text[0];

            if (char.IsDigit(keyChar)) 
            {
                string phone = GetPhone();
                phone += keyChar;
                SetPhone(phone);
            }

            e.Handled = true;
        }

        private void _tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                string phone = GetPhone();
                SetPhone(phone);
            }
        }

        private void _tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phone = GetPhone();
            SetPhone(phone);
        }
    }
}
