using System.Runtime.InteropServices;
public static class DMH
{
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
    {
        if (IsWindows10OrGreater(17763))
        {
            var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
            int useImmersiveDarkMode = enabled ? 1 : 0;
            return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
        }

        return false;
    }

    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

    [Flags]
    public enum DwmWindowAttribute : uint
    {
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_MICA_EFFECT = 1029
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }

    [DllImport("dwmapi.dll", PreserveSig = true)]
    static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

    public static void EnableMica(IntPtr source)
    {
        int trueValue = 0x01;
        DwmSetWindowAttribute(source, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
    }

    public static void EnableWindowDarkTheme(bool enable, bool mica = false)
    {
        foreach (Form c in Application.OpenForms)
        {
            UseImmersiveDarkMode(c.Handle, enable);
            if (enable)
            {
                SetDarkModeForControl(c);
            }
            else
            {
                SetLightModeForControl(c);
            }
        }
            
    }

    public static void SetDarkModeForControl(Control c)
    {
        SetWindowTheme(c.Handle, "DarkMode_Explorer", null);
        foreach (Control ct in c.Controls)
        {
            SetDarkModeForControl(ct);
        }
    }

    public static void SetLightModeForControl(Control c)
    {
        SetWindowTheme(c.Handle, "", null);
        foreach (Control ct in c.Controls)
        {
            SetLightModeForControl(ct);
        }
    }

    public static int buttonDark(IntPtr btn)
    {
        return SetWindowTheme(btn, "DarkMode_Explorer", null);
    }
    public static int buttonDarkAlt(IntPtr btn)
    {
        return SetWindowTheme(btn, "DarkMode", null);
    }

    public static int buttonDarkStr(IntPtr btn, string theme)
    {
        return SetWindowTheme(btn, theme, null);
    }

    [DllImport("uxtheme.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    private static bool IsWindows10OrGreater(int build = -1)
    {
        return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
    }
}