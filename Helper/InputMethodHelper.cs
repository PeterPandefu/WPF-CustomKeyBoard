using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomKeyboard
{
    public class InputMethodHelper
    {
        private const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        private const int HWND_BROADCAST = 0xffff;

        //英文（美国）：0x0409
        public const int EN_US = 0x0409;
        //中文（简体中文）：0x0804
        public const int ZH_CN = 0x0804;
        //中文（繁体中文-台湾）：0x0404
        public const int ZH_TW = 0x0404;
        //日文：0x0411
        public const int JA_JP = 0x0411;




        [DllImport("user32.dll")]
        private static extern bool GetKeyboardLayoutName([Out] StringBuilder pwszKLID);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern uint GetKeyboardLayoutList(uint nBuff, [Out] IntPtr[] lpList);

        public static bool SetInputLanguage(int language)
        {
            // 获取系统语言列表
            IntPtr[] langList = GetKeyboardLayoutList();

            // 遍历语言列表，找到需要设置的语言
            foreach (IntPtr langPtr in langList)
            {
                int langId = langPtr.ToInt32() & 0xFFFF;  // 获取语言ID
                if (langId == language)
                {
                    // 设置默认输入法为指定语言的输入法
                    SendMessage(new IntPtr(HWND_BROADCAST), WM_INPUTLANGCHANGEREQUEST, 0, langPtr.ToInt32());
                    return true;
                }
            }

            // 若遍历完语言列表未找到指定语言，则返回False
            return false;
        }

        private static IntPtr[] GetKeyboardLayoutList()
        {
            IntPtr hklList = IntPtr.Zero;
            try
            {
                // 获取语言列表
                uint count = GetKeyboardLayoutList(0, null);

                hklList = Marshal.AllocHGlobal((int)(count * IntPtr.Size));
                // 将语言列表转换为IntPtr数组
                IntPtr[] result = new IntPtr[count];

                GetKeyboardLayoutList(count, result);

                return result;
            }
            finally
            {
                if (hklList != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(hklList);
                }
            }
        }



        public static string GetKeyboardLayoutName()
        {
            var res = string.Empty;
            StringBuilder sb = new StringBuilder(9);
            if (GetKeyboardLayoutName(sb))
            {
                var val = int.Parse(sb.ToString(), System.Globalization.NumberStyles.HexNumber);
                switch (val)
                {
                    case EN_US:
                        res = "en-US";
                        break;
                    case ZH_CN:
                        res = "zh-CN";
                        break;
                    case ZH_TW:
                        res = "zh-TW";
                        break;
                    case JA_JP:
                        res = "en-US";
                        break;
                }
            }

            return res;
        }
    }
}
