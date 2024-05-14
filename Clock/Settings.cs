namespace Clock
{
    public partial class Settings : Form
    {

        ColorDiag colorDialog1 = new ColorDiag();

        Color DarkBg = Color.FromArgb(25, 25, 25);
        Color DarkFg = Color.FromArgb(230,230,230);
        Color DarkDt = Color.DarkGray;

        Color LightBg = Color.White;
        Color LightFg = Color.Black;
        Color LightDt = Color.Gray;
        public Settings()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://droc101.ddns.net");
        }

        private void Settings_Load(object sender, EventArgs e)
        {

            DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
            r();
        }

        public void r(bool radios = true)
        {
            Properties.Settings.Default.Save();
            linkLabel1.LinkColor = Properties.Settings.Default.DetailColor;
            BackColor = Properties.Settings.Default.BackgroundColor;
            ForeColor = Properties.Settings.Default.ForegroundColor;
            bgColor.BackColor = Properties.Settings.Default.BackgroundColor;
            fgColor.BackColor = Properties.Settings.Default.ForegroundColor;
            detailColor.BackColor = Properties.Settings.Default.DetailColor;
            button1.ForeColor = SystemColors.ControlText;
            changeBgColor.ForeColor = SystemColors.ControlText;
            changeFgColor.ForeColor = SystemColors.ControlText;
            changeDetailColor.ForeColor = SystemColors.ControlText;
            if (radios)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton6.Checked = false;
                radioButton5.Checked = false;
                if (Properties.Settings.Default.AutoTheme == 0)
                {
                    radioButton1.Checked = true;
                }
                else if (Properties.Settings.Default.AutoTheme == 1)
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }
                if (!Properties.Settings.Default.CustomThemeColor)
                {
                    radioButton6.Checked = true;
                } else
                {
                    radioButton5.Checked = true;
                }
                
            }
            changeBgColor.Enabled = radioButton3.Checked;
            changeFgColor.Enabled = radioButton3.Checked;
            changeDetailColor.Enabled = radioButton3.Checked;
            radioButton5.Enabled = radioButton3.Checked;
            radioButton6.Enabled = radioButton3.Checked;

        }

        private void changeBgColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.BackgroundColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackgroundColor = colorDialog1.Color;
            }
            r();
        }

        private void changeFgColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.ForegroundColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ForegroundColor = colorDialog1.Color;
            }
            r();
        }

        private void changeDetailColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.DetailColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DetailColor = colorDialog1.Color;
            }
            r();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            r();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                Properties.Settings.Default.AutoTheme = 2;
                r(false);
                if (!Properties.Settings.Default.CustomThemeColor)
                {
                    radioButton6.Checked = true;
                }
                else
                {
                    radioButton5.Checked = true;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Properties.Settings.Default.BackgroundColor = DarkBg;
                Properties.Settings.Default.ForegroundColor = DarkFg;
                Properties.Settings.Default.DetailColor = DarkDt;
                Properties.Settings.Default.CustomThemeColor = true;
                Properties.Settings.Default.AutoTheme = 1;
                DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
                r(false);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Properties.Settings.Default.BackgroundColor = LightBg;
                Properties.Settings.Default.ForegroundColor = LightFg;
                Properties.Settings.Default.DetailColor = LightDt;
                Properties.Settings.Default.CustomThemeColor = false;
                Properties.Settings.Default.AutoTheme = 0;
                DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
                r(false);
            }
            
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                Properties.Settings.Default.CustomThemeColor = false;
                DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
                r(false);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                Properties.Settings.Default.CustomThemeColor = true;
                DMH.EnableWindowDarkTheme(Properties.Settings.Default.CustomThemeColor);
                r(false);
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
