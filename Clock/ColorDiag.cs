namespace Clock
{
    public partial class ColorDiag : Form
    {
        public Color Color = Color.White;

        bool du = false;

        public ColorDiag()
        {
            InitializeComponent();
            BackColor = Properties.Settings.Default.BackgroundColor;
            ForeColor = Properties.Settings.Default.ForegroundColor;
            
        }

        private void ColorDiag_Load(object sender, EventArgs e)
        {
            DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
            du = false;
            
            tb1.Value = Color.R;
            tb2.Value = Color.G;
            tb3.Value = Color.B;
            label4.Text = ColToHex(Color);
            du = true;
            panel1.BackColor = Color;
        }

        private void tb1_ValueChanged(object sender, EventArgs e)
        {
            if (!du) { return; }
            Color = Color.FromArgb(tb1.Value, tb2.Value, tb3.Value);
            panel1.BackColor = Color;
            label4.Text = ColToHex(Color);
        }

        public string ColToHex(Color c)
        {
            return "R: " + c.R.ToString() + " G: " + c.G.ToString() + " B: " + c.B.ToString();
        }

        private void tb2_ValueChanged(object sender, EventArgs e)
        {
            if (!du) { return; }
            Color = Color.FromArgb(tb1.Value, tb2.Value, tb3.Value);
            panel1.BackColor = Color;
            label4.Text = ColToHex(Color);
        }

        private void tb3_ValueChanged(object sender, EventArgs e)
        {
            if (!du) { return; }
            Color = Color.FromArgb(tb1.Value, tb2.Value, tb3.Value);
            panel1.BackColor = Color;
            label4.Text = ColToHex(Color);
        }
    }
}
