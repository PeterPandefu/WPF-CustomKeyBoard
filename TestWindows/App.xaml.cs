using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestWindows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            KillProcess("CustomKeyboard");
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }
        private void KillProcess(string processName)
        {

            //var ttt=      Process.GetProcesses().OrderBy(t=>t.ProcessName);
            foreach (var process in Process.GetProcessesByName(processName))
            {
                try
                {
                    // 杀掉这个进程。
                    process.Kill();

                    // 等待进程被杀掉。你也可以在这里加上一个超时时间（毫秒整数）。
                    process.WaitForExit();
                }
                catch (Win32Exception ex)
                {
                    // 无法结束进程，可能有很多原因。
                    // 建议记录这个异常，如果你的程序能够处理这里的某种特定异常了，那么就需要在这里补充处理。
                    // Log.Error(ex);
                }
                catch (InvalidOperationException)
                {
                    // 进程已经退出，无法继续退出。既然已经退了，那这里也算是退出成功了。
                    // 于是这里其实什么代码也不需要执行。
                }
            }
        }
    }
}
