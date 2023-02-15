using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomKeyboard
{
    public class KeyboardKey : Button
    {
        static KeyboardKey()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyboardKey), new FrameworkPropertyMetadata(typeof(KeyboardKey)));
        }

        public static readonly DependencyProperty KeyboardKeyTypeProperty = DependencyProperty.Register("KeyboardKeyType", typeof(KeyType), typeof(KeyboardKey));

        [Category("Chemclin")]
        [Browsable(true)]
        [Description("键类型")]
        public KeyType KeyboardKeyType
        {
            get => (KeyType)GetValue(KeyboardKeyTypeProperty);
            set => SetValue(KeyboardKeyTypeProperty, value);
        }

        public static readonly DependencyProperty IsNumberKeyProperty = DependencyProperty.Register("IsNumberKey", typeof(bool), typeof(KeyboardKey));

        [Category("Chemclin")]
        [Browsable(true)]
        [Description("是否数字键盘按键")]
        public bool IsNumberKey
        {
            get => (bool)GetValue(IsNumberKeyProperty);
            set => SetValue(IsNumberKeyProperty, value);
        }



        public static readonly DependencyProperty ShiftKeyWordProperty = DependencyProperty.Register("ShiftKeyWord", typeof(string), typeof(KeyboardKey));

        [Category("Chemclin")]
        [Browsable(true)]
        [Description("按键Shift文本")]
        public string ShiftKeyWord
        {
            get => (string)GetValue(ShiftKeyWordProperty);
            set => SetValue(ShiftKeyWordProperty, value);
        }
    }

   

   
}
