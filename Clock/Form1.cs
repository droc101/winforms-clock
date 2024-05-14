using System.Runtime.InteropServices;

namespace Clock
{
    public partial class Form1 : Form
    {

        Bitmap c = new Bitmap(256, 256);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        private int SYSMENU_ABOUT_ID = 0x1;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);
            DMH.buttonDarkAlt(hSysMenu);
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&Settings");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                new Settings().ShowDialog();
            }
        }

        public Form1()
        {
            InitializeComponent();
            DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
        }

        static float map(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
        {
            return targetFrom + (source - sourceFrom) * (targetTo - targetFrom) / (sourceTo - sourceFrom);
        }

        public static Point trg(float angle, float len, Point center)
        {
            return new Point((int)(Math.Cos(angle) * len) + center.X, (int)(Math.Sin(angle) * len) + center.Y);
        }

        public static Point getHand(float val, float max, float len, float sizeMul)
        {
            float rad = (float)(map(val, 0.0f, max, 0.0f, (float)Math.Tau));
            return trg(rad - (float)(Math.PI/2f), len, new Point((int)(128 * sizeMul), (int)(128 * sizeMul)));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Properties.Settings.Default.BackgroundColor;
            c.Dispose();
            c = renderClock(1f);
            pictureBox1.Image = c;
            DestroyIcon(this.Icon.Handle);
            Bitmap i = renderClock(0.1f);
            Icon = ToIcon(i, true, Properties.Settings.Default.BackgroundColor, 32);
            i.Dispose();
            
        }

        public Bitmap renderClock(float sizeMul)
        {
            Bitmap clock = new Bitmap((int)(256 * sizeMul), (int)(256 * sizeMul));
            DateTime now = DateTime.Now;
            Graphics graphics = Graphics.FromImage(clock);
            graphics.Clear(Properties.Settings.Default.BackgroundColor);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            Pen fg = new Pen(Properties.Settings.Default.ForegroundColor);
            Pen SH = new Pen(Properties.Settings.Default.DetailColor);
            graphics.DrawEllipse(fg, new Rectangle(new Point((int)(5 * sizeMul), (int)(5 * sizeMul)), clock.Size - new Size((int)(11 * sizeMul), (int)(11 * sizeMul))));
            int ix = 12;
            for (int i = 0; i < 60; i++)
            {
                Point DP = getHand(i, 60f, 115f * sizeMul, sizeMul);
                if (i % 5 == 0)
                {
                    Point TP = getHand(i, 60f, 100f * sizeMul, sizeMul);
                    graphics.FillEllipse(fg.Brush, new Rectangle(new Point(DP.X - (int)(4 * sizeMul), DP.Y - (int)(4 * sizeMul)), new Size((int)(8 * sizeMul), (int)(8 * sizeMul))));
                    if (ix == 12) { ix = 0; }
                    ix++;
                }
                else
                {
                    graphics.FillEllipse(SH.Brush, new Rectangle(new Point(DP.X - (int)(2 * sizeMul), DP.Y - (int)(2 * sizeMul)), new Size((int)(4 * sizeMul), (int)(4 * sizeMul))));
                }

            }
            Point SP = getHand((float)now.Millisecond + (now.Second * 1000), 60000f, 90f * sizeMul, sizeMul);
            graphics.DrawLine(SH, new Point((int)(128 * sizeMul), (int)(128 * sizeMul)), SP);
            Point MP = getHand((float)now.Second + (now.Minute * 60), 3600f, 100f * sizeMul, sizeMul);
            graphics.DrawLine(fg, new Point((int)(128 * sizeMul), (int)(128 * sizeMul)), MP);
            Point HP = getHand((float)now.Minute + ((now.Hour % 12) * 60), 720f, 80f * sizeMul, sizeMul);
            graphics.DrawLine(fg, new Point((int)(128 * sizeMul), (int)(128 * sizeMul)), HP);
            graphics.FillEllipse(fg.Brush, new Rectangle(new Point((int)(124 * sizeMul), (int)(124 * sizeMul)), new Size((int)(8 * sizeMul), (int)(8 * sizeMul))));
            graphics.Dispose();
            return clock;
        }

        public Icon ToIcon(Bitmap img, bool makeTransparent, Color colorToMakeTransparent, int width)
        {
            img.SetResolution(width, width);
            if (makeTransparent)
            {
                img.MakeTransparent(colorToMakeTransparent);
            }
            var iconHandle = img.GetHicon();
            return Icon.FromHandle(iconHandle);
        }
    }
}