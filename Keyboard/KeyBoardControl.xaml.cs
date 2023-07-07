﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// KeyBoardControl.xaml 的交互逻辑
    /// </summary>
    public partial class KeyBoardControl : UserControl
    {
        public event Action CustomClosed;
        private bool Flag = false;
        public KeyBoardControl()
        {
            InitializeComponent();

        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {

            if (e.OriginalSource is KeyboardKey)
            {
                var button = e.OriginalSource as KeyboardKey;

                string tag = button.Tag.ToString();

                if (string.IsNullOrEmpty(tag)) return;

                byte b = Convert.ToByte(tag);

                KeyHelper.OnKeyPress(b);
            }

        }

        private void ToggonBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggonKeyboardKey btn = sender as ToggonKeyboardKey;

            string tag = btn.Tag.ToString();

            byte b = Convert.ToByte(tag);

            if (!(bool)btn.IsChecked)
            {
                KeyHelper.OnKeyUp(b);
            }
            else
            {
                KeyHelper.OnKeyDown(b);
            }
        }

        private void CapsLock_Click(object sender, RoutedEventArgs e)
        {

            var btns = FindVisualChild<KeyboardKey>(this.boardGrid);

            foreach (var btn in btns)
            {
                if (btn.Content == null) continue;

                if (btn.Content.ToString().Length != 1) continue;

                btn.Content = (bool)this.btn_CapsLock.IsChecked ? btn.Content.ToString().ToUpper() : btn.Content.ToString().ToLower();

            }

            //this.RefreshCapsText();

            var button = e.OriginalSource as ToggonKeyboardKey;

            string tag = button.Tag.ToString();

            byte b = Convert.ToByte(tag);

            KeyHelper.OnKeyPress(b);
        }

        void RefreshCapsText()
        {
            if ((bool)this.btn_CapsLock.IsChecked)
            {
                this.btn_CapsLock.ShiftKeyWord = "大写";
            }
            else
            {
                this.btn_CapsLock.ShiftKeyWord = "小写";
            }
        }

        List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            List<T> collecion = new List<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                    collecion.Add((T)child);
                else
                {
                    collecion.AddRange(FindVisualChild<T>(child));
                }
            }

            return collecion;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                foreach (var toggon in FindVisualChild<ToggonKeyboardKey>(this.boardGrid))
                {
                    string tag = toggon.Tag.ToString();

                    if (string.IsNullOrEmpty(tag)) continue;

                    byte b = Convert.ToByte(tag);

                    if ((bool)toggon.IsChecked)
                    {
                        KeyHelper.OnKeyUp(b);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            CustomClosed?.Invoke();
        }

        /// <summary>
        /// 中英文切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchEnOrCh_Click(object sender, RoutedEventArgs e)
        {
            if (Flag)
            {
                InputMethodHelper.SetInputLanguage(InputMethodHelper.EN_US);
            }
            else
            {
                InputMethodHelper.SetInputLanguage(InputMethodHelper.ZH_CN);
            }

            Flag = !Flag;

            this.switchEnOrCh.IsChecked = Flag;
        }

        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextLine_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Refresh()
        {
            this.btn_CapsLock.IsChecked = Console.CapsLock;

            //若当前输入法为中文  IsChecked=true
            Flag = InputMethodHelper.GetKeyboardLayoutName().Contains("zh");

            switchEnOrCh.IsChecked = Flag;
        }
    }
}
