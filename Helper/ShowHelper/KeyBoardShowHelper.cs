using System;
using System.Collections.Generic;
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

namespace CustomKeyboard
{
    public class KeyBoardShowHelper
    {
        private static readonly Subject<Tuple<UIElement, bool>> FocusSubject = new Subject<Tuple<UIElement, bool>>();

        private static readonly Subject<bool> TabTipClosedSubject = new Subject<bool>();

        private static readonly List<Type> BindedUIElements = new List<Type>();
        public static bool IsEnable { get; set; } = true;
        public static bool IsNumberBoard { get; set; }
        public static double Width { get; set; } = -1d;
        public static double Top { get; set; } = -1d;
        public static double Left { get; set; } = -1d;

        private static readonly NumberBoardWindow _NumberBoardWindow;

        private static readonly KeyBoardWindow _KeyBoardWindow;

        private static readonly object _StateLock=new object();

        public static readonly KeyboardHook _keyboardHook;
        static KeyBoardShowHelper()
        {
            AutomateTabTipOpen(FocusSubject.AsObservable());

            AutomateTabTipClose(FocusSubject.AsObservable(), TabTipClosedSubject);

            _NumberBoardWindow = NumberBoardWindow.Instance;

            _KeyBoardWindow = KeyBoardWindow.Instance;

            _keyboardHook = new KeyboardHook();

            //_keyboardHook.SetHook();

            //_keyboardHook.SetOnKeyDownEvent(Win32_Keydown);
        }

        private static void Win32_Keydown(Key key)
        {
            _KeyBoardWindow.Hide();
            _NumberBoardWindow.Hide();
        }



        public static void Close()
        {
            if (!IsEnable) return;

            lock (_StateLock)
            {
                _KeyBoardWindow.Dispatcher.Invoke(() =>
                {
                    _NumberBoardWindow.Hide();

                    _KeyBoardWindow.Hide();

                    TabTipClosedSubject.OnNext(true);
                });

            }
        }


        public static void Open()
        {
            if (!IsEnable) return;
            lock (_StateLock)
            {
                _KeyBoardWindow.Dispatcher.Invoke(() =>
                {
                    _NumberBoardWindow.Hide();

                    _KeyBoardWindow.Hide();

                    if (IsNumberBoard)
                    {
                        if (Width == -1)
                        {
                            if (Top == -1 && Left == -1)
                            {
                                _NumberBoardWindow.SetParameter(350);
                                _NumberBoardWindow.Show();
                            }
                            else
                            {
                                _NumberBoardWindow.SetParameter(350, Top, Left);
                                _NumberBoardWindow.Show();
                            }
                        }
                        else
                        {
                            if (Top == -1 && Left == -1)
                            {
                                _NumberBoardWindow.SetParameter(Width);
                                _NumberBoardWindow.Show();
                            }
                            else
                            {
                                _NumberBoardWindow.SetParameter(Width, Top, Left);
                                _NumberBoardWindow.Show();
                            }
                        }
                    }
                    else
                    {
                        if (Width == -1)
                        {
                            if (Top == -1 && Left == -1)
                            {

                                _KeyBoardWindow.SetParameter(1400);
                                _KeyBoardWindow.Show();
                            }
                            else
                            {
                                _KeyBoardWindow.SetParameter(1400, Top, Left);
                                _KeyBoardWindow.Show();
                            }
                        }
                        else
                        {
                            if (Top == -1 && Left == -1)
                            {
                                _KeyBoardWindow.SetParameter(Width);
                                _KeyBoardWindow.Show();
                            }
                            else
                            {
                                _KeyBoardWindow.SetParameter(Width, Top, Left);
                                _KeyBoardWindow.Show();
                            }
                        }
                    }
                });

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

                        KeyBoardShowHelper.IsNumberBoard = args.Item1;
                        KeyBoardShowHelper.Width = args.Item2;
                        KeyBoardShowHelper.Top = args.Item3;
                        KeyBoardShowHelper.Left = args.Item4;
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
