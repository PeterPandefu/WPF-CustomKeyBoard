using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomKeyboard
{
    /// <summary>
    /// NumberBoardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NumberBoardWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nindex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int index);

        public bool IsShowing { get; set; }

        private static NumberBoardWindow instance = null;
        public static NumberBoardWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NumberBoardWindow();
                }

                return instance;
            }
        }
        public NumberBoardWindow()
        {
            InitializeComponent();

            nbc.CustomClosed += Hide;

            Loaded += NumberBoardWindow_Loaded;
        }

        /// <summary>
        /// 指定长,宽,默认位置
        /// </summary>
        public void SetParameter(double width)
        {
            //得到屏幕整体宽度
            double x1 = SystemParameters.PrimaryScreenWidth;
            //得到屏幕整体高度
            double y1 = SystemParameters.PrimaryScreenHeight;

            double param = y1 / 3;

            this.Width = width;//设置窗体宽度

            this.Height = width * 6d /7d;//设置窗体高度

            this.Top = y1 - this.Height;

            this.Left = (x1 - this.Width) / 2;
        }

        /// <summary>
        /// 指定长,宽,和位置
        /// </summary>
        public void SetParameter(double width, double top, double left)
        {
            //double x = SystemParameters.WorkArea.Width;//得到屏幕工作区域宽度
            //double y = SystemParameters.WorkArea.Height;//得到屏幕工作区域高度
            //double screeHeight = SystemParameters.FullPrimaryScreenHeight;
            //double screeWidth = SystemParameters.FullPrimaryScreenWidth;

            //得到屏幕整体宽度
            double x1 = SystemParameters.PrimaryScreenWidth;
            //得到屏幕整体高度
            double y1 = SystemParameters.PrimaryScreenHeight;

            double param = y1 / 3;

            this.Width = width;//设置窗体宽度

            this.Height = width * 6d / 7d;//设置窗体高度

            this.Top = top;

            this.Left = left;
        }

        private void NumberBoardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);

            IntPtr intPtr = windowInteropHelper.Handle;

            int value = -20;

            SetWindowLong(intPtr, value, (IntPtr)0x8000000);
        }

        public new void Hide()
        {
            base.Hide();
            IsShowing = false;
        }

        public new void Show()
        {
            base.Show();
            IsShowing = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
