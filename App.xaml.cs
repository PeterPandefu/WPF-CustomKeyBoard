using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomKeyboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            KeyBoardShowHelper.IsEnable = true;
            KeyBoardShowHelper.BindTo<TextBox>();
            MainWindow window = new MainWindow();
            if (e.Args.Length > 0)
            {
                window.ShowInTaskbar = false;
                window.Visibility = Visibility.Hidden;
                _ = Task.Run(() =>
                {

                    if (e.Args.Length > 0)
                    {
                        using (PipeStream pipeClient =
                            new AnonymousPipeClientStream(PipeDirection.In, e.Args[0]))
                        {
                            using (StreamReader sr = new StreamReader(pipeClient))
                            {
                                string temp;
                                while ((temp = sr.ReadLine()) != null)
                                {
                                    var param = temp.Split(':');
                                    if (param.Length == 2)
                                    {
                                        //格式 OPEN:0,1000,-1,-1
                                        if (param[0].ToUpper() == "OPEN")
                                        {
                                            var boardParam = param[1].Split(',');
                                            KeyBoardShowHelper.IsNumberBoard = bool.Parse(boardParam[0]);
                                            KeyBoardShowHelper.Width = double.Parse(boardParam[1]);
                                            KeyBoardShowHelper.Top = double.Parse(boardParam[2]);
                                            KeyBoardShowHelper.Left = double.Parse(boardParam[3]);
                                            KeyBoardShowHelper.Open();
                                        }
                                        else if (param[0].ToUpper() == "CLOSE")
                                        {
                                            KeyBoardShowHelper.Close();
                                        }
                                        else if (param[0].ToUpper() == "SHUTDOWN")
                                        {
                                            this.Dispatcher.Invoke(() => { Shutdown(); });
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                });
            }
            else
            {
                window.Show();
            }
        }
    }
}
