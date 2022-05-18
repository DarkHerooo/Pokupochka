using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GeneralLib
{
    public class ButtonSelector
    {
        private Button[] _buttons;
        private Button _selectedButton = null!;
        private Style? _defaultStyle = null!;
        private Style? _selectStyle = null!;

        public ButtonSelector(Button[] buttons, Style? defaultStyle, Style? selectStyle)
        {
            _buttons = buttons;
            _defaultStyle = defaultStyle;
            _selectStyle = selectStyle;
            _selectedButton = _buttons.First();

            foreach (var button in _buttons)
            {
                button.Style = _defaultStyle;
                button.Click += Button_Click;
            }

            SelectButton(_selectedButton);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectButton((Button)sender);
        }

        public void SelectButton(Button button)
        {
            Button? selectedButton = _buttons.FirstOrDefault(but => but == button);

            if (selectedButton != null)
            {
                _selectedButton.Style = _defaultStyle;
                _selectedButton = button;
                _selectedButton.Style = _selectStyle;
            }
        }
    }
}
