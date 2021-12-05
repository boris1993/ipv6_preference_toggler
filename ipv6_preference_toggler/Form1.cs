using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace ipv6_preference_toggler
{
    public partial class Form1 : Form
    {
        private const string LastKeyRegistryPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";
        private const string LastKeyKeyName = "LastKey";

        private const string RegistryKeyPath = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip6\\Parameters";
        private const string RegistryKeyName = "DisabledComponents";
        private const int PreferIPv4BitMask = 0b00100000;
        private const string PreferringIPv4 = "IPv4";
        private const string PreferringIPv6 = "IPv6";

        private readonly CultureInfo DefaultCultureInfo = CultureInfo.GetCultureInfo("en-US");
        private readonly CultureInfo ChineseCultureInfo = CultureInfo.GetCultureInfo("zh");

        private ResourceManager _resourceManager;
        private string NullValueWarning;

        private int? RegistryKeyValue = null;

        public Form1()
        {
            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            if (currentUICulture == ChineseCultureInfo)
            {
                _resourceManager = new ResourceManager("ipv6_preference_toggler.Strings-zh", typeof(Strings_zh).Assembly);
            }
            else
            {
                _resourceManager = new ResourceManager("ipv6_preference_toggler.Strings", typeof(Strings).Assembly);
            }

            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            RegistryKeyValue = (int?)Registry.GetValue(RegistryKeyPath, RegistryKeyName, null);
            InitializeComponentState();
        }

        private void btn_switch_language_Click(object sender, EventArgs e)
        {
            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            if (currentUICulture == ChineseCultureInfo)
            {
                Thread.CurrentThread.CurrentUICulture = DefaultCultureInfo;
                _resourceManager = new ResourceManager("ipv6_preference_toggler.Strings", typeof(Strings).Assembly);
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = ChineseCultureInfo;
                _resourceManager = new ResourceManager("ipv6_preference_toggler.Strings-zh", typeof(Strings_zh).Assembly);
            }

            NullValueWarning = string.Join(Environment.NewLine, new string[] {
                _resourceManager.GetString("null_value_warning_part_1"),
                $"{RegistryKeyPath}\\{RegistryKeyName}",
                _resourceManager.GetString("null_value_warning_part_2")
            });

            Controls.Clear();
            InitializeComponent();
            InitializeComponentState();
        }

        private void InitializeComponentState()
        {
            btn_prefer_ipv4.Enabled = false;
            btn_prefer_ipv6.Enabled = false;
            lbl_value_current_preference.Text = "";
            lbl_current_value.Text = "";

            NullValueWarning = string.Join(Environment.NewLine, new string[] {
                _resourceManager.GetString("null_value_warning_part_1"),
                $"{RegistryKeyPath}\\{RegistryKeyName}",
                _resourceManager.GetString("null_value_warning_part_2")
            });

            if (RegistryKeyValue == null)
            {
                RegistryKeyValue = 0b00000000;
                MessageBox.Show(NullValueWarning);
            }

            UpdateCurrentPreferenceAndValueLabel();
        }

        private void FlipPreferredIPv4Flag(object sender, EventArgs e)
        {
            RegistryKeyValue ^= (1 << 5);
            UpdateCurrentPreferenceAndValueLabel();

            Registry.SetValue(RegistryKeyPath, RegistryKeyName, RegistryKeyValue);

            MessageBox.Show(_resourceManager.GetString("value_updated"));
        }

        private void UpdateCurrentPreferenceAndValueLabel()
        {
            lbl_current_value.Text = Convert.ToString((int)RegistryKeyValue, 2).PadLeft(8, '0');

            if ((RegistryKeyValue & PreferIPv4BitMask) == PreferIPv4BitMask)
            {
                btn_prefer_ipv4.Enabled = false;
                btn_prefer_ipv6.Enabled = true;
                lbl_value_current_preference.Text = PreferringIPv4;
            }
            else
            {
                btn_prefer_ipv4.Enabled = true;
                btn_prefer_ipv6.Enabled = false;
                lbl_value_current_preference.Text = PreferringIPv6;
            }
        }

        private void btn_view_in_regedit_Click(object sender, EventArgs e)
        {
            // I'm doing it in a little tricky way here
            // The regedit determines the default location by the value of "LastKey"
            // So I have to point the last key to our RegistryKeyPath and then launch the regedit
            Registry.SetValue(LastKeyRegistryPath, LastKeyKeyName, RegistryKeyPath);
            Process.Start("regedit.exe");
        }
    }
}