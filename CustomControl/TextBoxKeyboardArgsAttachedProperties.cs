 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomKeyboard
{
    public class TextBoxKeyboardArgsAttachedProperties
    {

        private static Tuple<bool,double, double, double> Args;
        public static string GetKeyboardArgs(DependencyObject obj)
        {
            return (string)obj.GetValue(KeyboardArgsProperty);
        }
        public static void SetKeyboardArgs(DependencyObject obj, string value)
        {
            obj.SetValue(KeyboardArgsProperty, value);
        }
        /// <summary>
        /// 逗号分割的字符串,四个值对应键盘的,是否数字键盘,Width,Top,Left
        /// </summary>
        public static readonly DependencyProperty KeyboardArgsProperty =
            DependencyProperty.RegisterAttached("KeyboardArgs", typeof(string), typeof(TextBox), new PropertyMetadata(string.Empty, (s,e) => {
                if (s is TextBox textBox)
                {
                    if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()))
                    {
                        var arr = e.NewValue.ToString().Split(',');
                        if (arr.Length == 4)
                        {
                            Args=  new Tuple<bool,double, double, double>(bool.Parse(arr[0].ToString()),double.Parse(arr[1].ToString()), double.Parse(arr[2].ToString()), double.Parse(arr[3].ToString()));
                            textBox.SetValue(TextBox.TagProperty, Args);
                        }
                    }
                   
                }
            }));
    }
}
