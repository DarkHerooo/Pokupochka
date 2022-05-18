using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GeneralLib
{
    public class FrameWithHistory
    {
        private List<Page> _frameHistory = new List<Page>();
        private Frame _frame = null!;

        public FrameWithHistory(Frame frame)
        {
            _frame = frame;
        }

        public void Navigate(Page page)
        {
            foreach (var fh in _frameHistory)
            {
                if (fh.Title == page.Title)
                {
                    _frame.Navigate(fh);
                    return;
                }
            }

            _frameHistory.Add(page);
            _frame.Navigate(page);
        }
    }
}
