using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomKeyboard
{
    /// <summary>
    /// NumberBoardControl.xaml 的交互逻辑
    /// </summary>
    public partial class NumberBoardControl : UserControl
    {
        public event Action CustomClosed;
        public NumberBoardControl()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CustomClosed?.Invoke();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is KeyboardKey)
            {
                var button = e.OriginalSource as KeyboardKey;

                if (button.Tag!=null &&string.IsNullOrEmpty(button.Tag.ToString())) return;

                byte b = Convert.ToByte(button.Tag.ToString());

                KeyHelper.OnKeyPress(b);
            }
        }
    }
}
