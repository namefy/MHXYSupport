using OpenCvSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MHXYWF.Utility;

public partial class WindowsApi
{
    #region 系统函数相关参数
    enum DWMWINDOWATTRIBUTE : uint
    {
        NCRenderingEnabled = 1,
        NCRenderingPolicy,
        TransitionsForceDisabled,
        AllowNCPaint,
        CaptionButtonBounds,
        NonClientRtlLayout,
        ForceIconicRepresentation,
        Flip3DPolicy,
        ExtendedFrameBounds,
        HasIconicBitmap,
        DisallowPeek,
        ExcludedFromPeek,
        Cloak,
        Cloaked,
        FreezeRepresentation
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }

    //移动鼠标 
    const int MOUSEEVENTF_MOVE = 0x0001;
    //模拟鼠标左键按下 
    const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    //模拟鼠标左键抬起 
    const int MOUSEEVENTF_LEFTUP = 0x0004;
    //模拟鼠标右键按下 
    const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    //模拟鼠标右键抬起 
    const int MOUSEEVENTF_RIGHTUP = 0x0010;
    //模拟鼠标中键按下 
    const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    //模拟鼠标中键抬起 
    const int MOUSEEVENTF_MIDDLEUP = 0x0040;
    //标示是否采用绝对坐标 
    const int MOUSEEVENTF_ABSOLUTE = 0x8000;
    //模拟鼠标滚轮滚动操作，必须配合dwData参数
    const int MOUSEEVENTF_WHEEL = 0x0800;
    #endregion

    #region bVk参数 常量定义

    public const byte vbKeyLButton = 0x1;    // 鼠标左键
    public const byte vbKeyRButton = 0x2;    // 鼠标右键
    public const byte vbKeyCancel = 0x3;     // CANCEL 键
    public const byte vbKeyMButton = 0x4;    // 鼠标中键
    public const byte vbKeyBack = 0x8;       // BACKSPACE 键
    public const byte vbKeyTab = 0x9;        // TAB 键
    public const byte vbKeyClear = 0xC;      // CLEAR 键
    public const byte vbKeyReturn = 0xD;     // ENTER 键
    public const byte vbKeyShift = 0x10;     // SHIFT 键
    public const byte vbKeyControl = 0x11;   // CTRL 键
    public const byte vbKeyAlt = 18;         // Alt 键  (键码18)
    public const byte vbKeyMenu = 0x12;      // MENU 键
    public const byte vbKeyPause = 0x13;     // PAUSE 键
    public const byte vbKeyCapital = 0x14;   // CAPS LOCK 键
    public const byte vbKeyEscape = 0x1B;    // ESC 键
    public const byte vbKeySpace = 0x20;     // SPACEBAR 键
    public const byte vbKeyPageUp = 0x21;    // PAGE UP 键
    public const byte vbKeyEnd = 0x23;       // End 键
    public const byte vbKeyHome = 0x24;      // HOME 键
    public const byte vbKeyLeft = 0x25;      // LEFT ARROW 键
    public const byte vbKeyUp = 0x26;        // UP ARROW 键
    public const byte vbKeyRight = 0x27;     // RIGHT ARROW 键
    public const byte vbKeyDown = 0x28;      // DOWN ARROW 键
    public const byte vbKeySelect = 0x29;    // Select 键
    public const byte vbKeyPrint = 0x2A;     // PRINT SCREEN 键
    public const byte vbKeyExecute = 0x2B;   // EXECUTE 键
    public const byte vbKeySnapshot = 0x2C;  // SNAPSHOT 键
    public const byte vbKeyDelete = 0x2E;    // Delete 键
    public const byte vbKeyHelp = 0x2F;      // HELP 键
    public const byte vbKeyNumlock = 0x90;   // NUM LOCK 键

    //常用键 字母键A到Z
    public const byte vbKeyA = 65;
    public const byte vbKeyB = 66;
    public const byte vbKeyC = 67;
    public const byte vbKeyD = 68;
    public const byte vbKeyE = 69;
    public const byte vbKeyF = 70;
    public const byte vbKeyG = 71;
    public const byte vbKeyH = 72;
    public const byte vbKeyI = 73;
    public const byte vbKeyJ = 74;
    public const byte vbKeyK = 75;
    public const byte vbKeyL = 76;
    public const byte vbKeyM = 77;
    public const byte vbKeyN = 78;
    public const byte vbKeyO = 79;
    public const byte vbKeyP = 80;
    public const byte vbKeyQ = 81;
    public const byte vbKeyR = 82;
    public const byte vbKeyS = 83;
    public const byte vbKeyT = 84;
    public const byte vbKeyU = 85;
    public const byte vbKeyV = 86;
    public const byte vbKeyW = 87;
    public const byte vbKeyX = 88;
    public const byte vbKeyY = 89;
    public const byte vbKeyZ = 90;

    //数字键盘0到9
    public const byte vbKey0 = 48;    // 0 键
    public const byte vbKey1 = 49;    // 1 键
    public const byte vbKey2 = 50;    // 2 键
    public const byte vbKey3 = 51;    // 3 键
    public const byte vbKey4 = 52;    // 4 键
    public const byte vbKey5 = 53;    // 5 键
    public const byte vbKey6 = 54;    // 6 键
    public const byte vbKey7 = 55;    // 7 键
    public const byte vbKey8 = 56;    // 8 键
    public const byte vbKey9 = 57;    // 9 键


    public const byte vbKeyNumpad0 = 0x60;    //0 键
    public const byte vbKeyNumpad1 = 0x61;    //1 键
    public const byte vbKeyNumpad2 = 0x62;    //2 键
    public const byte vbKeyNumpad3 = 0x63;    //3 键
    public const byte vbKeyNumpad4 = 0x64;    //4 键
    public const byte vbKeyNumpad5 = 0x65;    //5 键
    public const byte vbKeyNumpad6 = 0x66;    //6 键
    public const byte vbKeyNumpad7 = 0x67;    //7 键
    public const byte vbKeyNumpad8 = 0x68;    //8 键
    public const byte vbKeyNumpad9 = 0x69;    //9 键
    public const byte vbKeyMultiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
    public const byte vbKeyAdd = 0x6B;        // PLUS SIGN(+) 键
    public const byte vbKeySeparator = 0x6C;  // ENTER 键
    public const byte vbKeySubtract = 0x6D;   // MINUS SIGN(-) 键
    public const byte vbKeyDecimal = 0x6E;    // DECIMAL POINT(.) 键
    public const byte vbKeyDivide = 0x6F;     // DIVISION SIGN(/) 键


    //F1到F12按键
    public const byte vbKeyF1 = 0x70;   //F1 键
    public const byte vbKeyF2 = 0x71;   //F2 键
    public const byte vbKeyF3 = 0x72;   //F3 键
    public const byte vbKeyF4 = 0x73;   //F4 键
    public const byte vbKeyF5 = 0x74;   //F5 键
    public const byte vbKeyF6 = 0x75;   //F6 键
    public const byte vbKeyF7 = 0x76;   //F7 键
    public const byte vbKeyF8 = 0x77;   //F8 键
    public const byte vbKeyF9 = 0x78;   //F9 键
    public const byte vbKeyF10 = 0x79;  //F10 键
    public const byte vbKeyF11 = 0x7A;  //F11 键
    public const byte vbKeyF12 = 0x7B;  //F12 键

    #endregion

    #region 系统函数
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetForegroundWindow(IntPtr hWnd);

    [LibraryImport("dwmapi.dll")]
    private static partial int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out RECT pvAttribute, int cbAttribute);

    [LibraryImport("user32.dll")]
    private static partial int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int w, int h, int flag);
    [DllImport("user32.dll")]
    private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    #endregion

    /// <summary>
    /// 进程截屏
    /// </summary>
    /// <param name="processName">进程名称</param>
    /// <param name="imgPath">图片路径</param>
    /// <param name="imageFormat">图片格式</param>
    public static void Screenshot(Process process, string imgPath, ImageFormat imageFormat)
    {
        if (SetForegroundWindow(process.MainWindowHandle))
        {
            Thread.Sleep(500);
            RECT srcRect;
            if (!process.MainWindowHandle.Equals(IntPtr.Zero))
            {
                //SetWindowPos(process.MainWindowHandle, 0, -6, 0, 0, 0, 0x0001);
                Thread.Sleep(200);
                //MoveWindow(proc.MainWindowHandle, -10, 0, 1024, 768, true);
                //Thread.Sleep(200);

                DwmGetWindowAttribute(process.MainWindowHandle, DWMWINDOWATTRIBUTE.ExtendedFrameBounds, out srcRect, Marshal.SizeOf(typeof(RECT)));

                Bitmap bmp = new(srcRect.Width, srcRect.Height);
                Graphics screenG = Graphics.FromImage(bmp);

                try
                {
                    screenG.CopyFromScreen(srcRect.Left, srcRect.Top,
                            0, 0, srcRect.Size,
                            CopyPixelOperation.SourceCopy);

                    bmp.Save(imgPath, imageFormat);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    screenG.Dispose();
                    bmp.Dispose();
                }
            }
        }
    }

    /// <summary>
    /// 进程截屏
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
    public static RECT GetPrecessRect(Process process)
    {
        RECT rect;
        DwmGetWindowAttribute(process.MainWindowHandle, DWMWINDOWATTRIBUTE.ExtendedFrameBounds, out rect, Marshal.SizeOf(typeof(RECT)));
        return rect;
    }

    /// <summary>
    /// 鼠标移动
    /// </summary>
    /// <param name="point">坐标</param>
    public static void MouseMove(OpenCvSharp.Point point)
    {
        _ = mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, point.X * 65536 / 3440, point.Y * 65536 / 1440, 0, 0);
    }

    /// <summary>
    /// 鼠标左键单击
    /// </summary>
    public static void MouseLeftClick()
    {
        _ = mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Thread.Sleep(500);
        _ = mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// 鼠标左键单击
    /// </summary>
    /// <param name="point">坐标</param>
    public static void MouseLeftClick(OpenCvSharp.Point point)
    {
        MouseMove(point);
        MouseLeftClick();
    }

    public static void MouseLeftClick(OpenCvSharp.Point2f point)
    {
        MouseLeftClick(point.ToPoint());
    }

    /// <summary>
    /// 鼠标左键双击击
    /// </summary>
    public static void MouseLeftDoubleClick()
    {
        _ = mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        _ = mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// 鼠标右键单击
    /// </summary>
    public static void MouseRightClick()
    {
        _ = mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// 鼠标右键双击击
    /// </summary>
    public static void MouseRightDoubleClick()
    {
        _ = mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        _ = mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// 键盘按键
    /// </summary>
    /// <param name="hold">是否同时按住</param>
    /// <param name="keys">键位</param>
    public static void KeybordClick(bool hold, params byte[] keys)
    {
        if (hold)
        {
            foreach (byte key in keys)
            {
                keybd_event(key, 0, 0, 0);
                Thread.Sleep(200);
            }
            Thread.Sleep(200);
            foreach (byte key in keys)
            {
                keybd_event(key, 0, 2, 0);
                Thread.Sleep(200);
            }
        }
        else
        {
            foreach (byte key in keys)
            {
                keybd_event(key, 0, 0, 0);
                keybd_event(key, 0, 2, 0);
            }
        }
    }
}
