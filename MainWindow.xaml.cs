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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
           
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            KeyBoardShowHelper._keyboardHook.UnHook();
            App.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var width = double.Parse(tBoxWidth.Text);
            KeyBoardWindow.Instance.SetParameter(width);
            KeyBoardWindow.Instance.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var width = double.Parse(numberTBoxWidth.Text);
            NumberBoardWindow.Instance.SetParameter(width);
            NumberBoardWindow.Instance.Show();
        }
    }
}
