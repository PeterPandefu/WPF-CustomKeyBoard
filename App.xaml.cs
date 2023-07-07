using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
                                    try
                                    {
                                        var param = temp.Split(':');
                                        if (param != null && param.Length == 2)
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
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }


                });
                //window.Show();
            }
            else
            {
                Shutdown();
            }

            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("程序发生致命错误!\n");
            }
            sbEx.Append("捕获未处理异常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).StackTrace);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }

            MessageBox.Show(sbEx.ToString());
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获

            foreach (Exception item in e.Exception.InnerExceptions)
            {
                MessageBox.Show($"异常类型：{item.GetType()}{Environment.NewLine}来自：{item.Source}{Environment.NewLine}异常内容：{item.StackTrace}");
                e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出    
                MessageBox.Show($"捕获未处理异常:{e.Exception.StackTrace}", "UI线程未捕获异常处理");
            }
            catch (Exception ex)
            {
                //此时程序出现严重异常，将强制结束退出
                MessageBox.Show("程序发生致命错误!");
            }
        }
    }
}
