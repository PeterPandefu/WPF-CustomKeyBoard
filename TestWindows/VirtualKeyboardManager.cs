using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace TestWindows
{
    public class VirtualKeyboardManager
    {
        private static readonly Subject<Tuple<UIElement, bool>> FocusSubject = new Subject<Tuple<UIElement, bool>>();

        private static readonly Subject<bool> TabTipClosedSubject = new Subject<bool>();

        private static readonly List<Type> BindedUIElements = new List<Type>();
        public static bool IsEnable { get; set; } = true;
        public static bool IsNumberBoard { get; set; }
        public static double Width { get; set; } = -1d;
        public static double Top { get; set; } = -1d;
        public static double Left { get; set; } = -1d;

        private static AnonymousPipeServerStream _pipeServer;

        private static StreamWriter _pipeStreamWriter;


        private static Process pipeClient;
        static VirtualKeyboardManager()
        {
            AutomateTabTipOpen(FocusSubject.AsObservable());

            AutomateTabTipClose(FocusSubject.AsObservable(), TabTipClosedSubject);

            pipeClient = new Process();

            pipeClient.StartInfo.FileName = @"C:\Project\SVN\ThirdPartLib\CustomKeyboard\bin\Debug\net5.0-windows\CustomKeyboard.exe";

            _pipeServer = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);

            pipeClient.StartInfo.Arguments = _pipeServer.GetClientHandleAsString();

            pipeClient.StartInfo.UseShellExecute = false;

            pipeClient.Start();

            //_pipeServer.DisposeLocalCopyOfClientHandle();

            _pipeStreamWriter = new StreamWriter(_pipeServer);

            _pipeStreamWriter.AutoFlush = true;
        }

        public static void ShutDown()
        {
            try
            {
                _pipeStreamWriter.WriteLine($"SHUTDOWN:,,,");
            }
            catch (IOException e)
            {
            }
        }

        public static void Close()
        {
            try
            {
                if (!IsEnable) return;
                _pipeStreamWriter.WriteLine($"CLOSE:{IsNumberBoard},{Width},{Top},{Left}");
            }
            catch (IOException e)
            {
            }
        }


        public static void Open()
        {
            try
            {
                if (!IsEnable) return;
                _pipeStreamWriter.WriteLine($"OPEN:{IsNumberBoard},{Width},{Top},{Left}");
            }
            catch (IOException e)
            {
            }
        }

        private static void AutomateTabTipClose(IObservable<Tuple<UIElement, bool>> focusObservable, Subject<bool> tabTipClosedSubject)
        {
            focusObservable
                .ObserveOn(Scheduler.Default)
                .Where(tuple => tuple.Item2 == false)
                .Do(_ => Close())
                .Subscribe(_ => tabTipClosedSubject.OnNext(true));

            tabTipClosedSubject
                .Subscribe(_ => { });
        }

        private static void AutomateTabTipOpen(IObservable<Tuple<UIElement, bool>> focusObservable)
        {
            focusObservable
                .ObserveOn(Scheduler.Default)
                .Where(tuple => tuple.Item2 == true)
                .Do(_ => Open())
                .Subscribe(_ => { });
        }

        public static void BindTo<T>() where T : UIElement
        {
            if (BindedUIElements.Contains(typeof(T)))
                return;

            EventManager.RegisterClassHandler(
                classType: typeof(T),
                routedEvent: UIElement.TouchDownEvent,
                handler: new RoutedEventHandler((s, e) =>
                {
                    if (((UIElement)s).IsFocused)
                    {
                        FocusSubject.OnNext(new Tuple<UIElement, bool>((UIElement)s, true));
                    }
                }),
                handledEventsToo: true);

            EventManager.RegisterClassHandler(
                classType: typeof(T),
                routedEvent: UIElement.GotFocusEvent,
                handler: new RoutedEventHandler((s, e) =>
                {
                    if (s is TextBox)
                    {
                        var args = (Tuple<bool, double, double, double>)((TextBox)s).GetValue(TextBox.TagProperty);

                        VirtualKeyboardManager.IsNumberBoard = args.Item1;
                        VirtualKeyboardManager.Width = args.Item2;
                        VirtualKeyboardManager.Top = args.Item3;
                        VirtualKeyboardManager.Left = args.Item4;
                    }

                    FocusSubject.OnNext(new Tuple<UIElement, bool>((UIElement)s, true));
                }),
                handledEventsToo: true);

            EventManager.RegisterClassHandler(
                classType: typeof(T),
                routedEvent: UIElement.LostFocusEvent,
                handler: new RoutedEventHandler((s, e) =>
                {
                    FocusSubject.OnNext(new Tuple<UIElement, bool>((UIElement)s, false));
                }),
                handledEventsToo: true);

            EventManager.RegisterClassHandler(
                typeof(UIElement),
                UIElement.MouseLeftButtonDownEvent,
                new MouseButtonEventHandler(MouseDown),
                handledEventsToo: false);

            EventManager.RegisterClassHandler(
               typeof(UIElement),
               UIElement.KeyDownEvent,
               new KeyEventHandler(KeyDown),
               handledEventsToo: false);

            BindedUIElements.Add(typeof(T));
        }

        private static void KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsEnable) return;

            if (e.Key == Key.Enter)
            {
                var element = sender as UIElement;
                element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private static void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnable) return;

            var parent = e.OriginalSource as DependencyObject;
            if (parent is UIElement element && !element.Focusable)
            {
                element.Focusable = true;
                element.Focus();
                element.Focusable = false;
            }
        }
    }
}
