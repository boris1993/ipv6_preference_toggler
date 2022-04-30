using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Management.Automation;
using System.Net.NetworkInformation;
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

        private readonly ResourceManager _chineseResourceManager;
        private readonly ResourceManager _englishResourceManager;
        private ResourceManager _resourceManager;

        private int? _registryKeyValue = null;
        private readonly Dictionary<string, NetworkInterface> _networkInterfaceNameAndIDLookup = new Dictionary<string, NetworkInterface>();

        public Form1()
        {
            _englishResourceManager = new ResourceManager("ipv6_preference_toggler.Strings", typeof(Strings).Assembly);
            _chineseResourceManager = new ResourceManager("ipv6_preference_toggler.Strings-zh", typeof(Strings_zh).Assembly);

            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            if (currentUICulture == ChineseCultureInfo)
            {
                _resourceManager = _chineseResourceManager;
            }
            else
            {
                _resourceManager = _englishResourceManager;
            }

            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var networks = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(networkInterface => networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Where(networkInterface => networkInterface.OperationalStatus == OperationalStatus.Up)
                .ToList();

            networks.ForEach(network => _networkInterfaceNameAndIDLookup.Add(network.Name, network));

            InitializeComponentState();
        }

        private void btn_switch_language_Click(object sender, EventArgs e)
        {
            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            if (currentUICulture == ChineseCultureInfo)
            {
                Thread.CurrentThread.CurrentUICulture = DefaultCultureInfo;
                _resourceManager = _englishResourceManager;
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = ChineseCultureInfo;
                _resourceManager = _chineseResourceManager;
            }

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

            var nullValueWarning = string.Join(Environment.NewLine, new string[] {
                _resourceManager.GetString("null_value_warning_part_1"),
                $"{RegistryKeyPath}\\{RegistryKeyName}",
                _resourceManager.GetString("null_value_warning_part_2")
            });

            _registryKeyValue = (int?)Registry.GetValue(RegistryKeyPath, RegistryKeyName, null);

            if (_registryKeyValue == null)
            {
                _registryKeyValue = 0b00000000;
                MessageBox.Show(nullValueWarning);
            }

            UpdateCurrentPreferenceAndValueLabel();

            cbNetworkAdapters.Items.AddRange(_networkInterfaceNameAndIDLookup.Keys.ToArray());
            cbNetworkAdapters.SelectedIndex = 0;
        }

        private void FlipPreferredIPv4Flag(object sender, EventArgs e)
        {
            _registryKeyValue ^= (1 << 5);
            UpdateCurrentPreferenceAndValueLabel();

            Registry.SetValue(RegistryKeyPath, RegistryKeyName, _registryKeyValue);

            MessageBox.Show(_resourceManager.GetString("value_updated"));
        }

        private void UpdateCurrentPreferenceAndValueLabel()
        {
            lbl_current_value.Text = Convert.ToString((int)_registryKeyValue, 2).PadLeft(8, '0');

            if ((_registryKeyValue & PreferIPv4BitMask) == PreferIPv4BitMask)
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

        private void cbNetworkAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            var networkInterfaceName = cbNetworkAdapters.SelectedItem.ToString();
            if (!_networkInterfaceNameAndIDLookup.TryGetValue(networkInterfaceName!, out var networkInterface))
            {
                return;
            }

            if (networkInterface.Supports(NetworkInterfaceComponent.IPv6))
            {
                btnEnableIPv6.Enabled = false;
                btnDisableIPv6.Enabled = true;
            }
            else
            {
                btnEnableIPv6.Enabled = true;
                btnDisableIPv6.Enabled = false;
            }
        }

        private void btnDisableIPv6_Click(object sender, EventArgs e)
        {
            var networkName = cbNetworkAdapters.SelectedItem.ToString();

            Cursor.Current = Cursors.WaitCursor;

            using (var powerShell = PowerShell.Create())
            {
                powerShell.AddScript($"Disable-NetAdapterBinding -Name '{networkName}' -ComponentID ms_tcpip6");
                powerShell.Invoke();

                if (powerShell.HadErrors)
                {
                    var errors = powerShell.Streams.Error.ToList();
                    string errorMessage = "";
                    errors.ForEach(error => errorMessage += ($"{error.ToString}\n"));

                    MessageBox.Show(errorMessage);
                }
            }

            Cursor.Current = Cursors.Default;

            btnDisableIPv6.Enabled = false;
            btnEnableIPv6.Enabled = true;
        }

        private void btnEnableIPv6_Click(object sender, EventArgs e)
        {
            var networkName = cbNetworkAdapters.SelectedItem.ToString();

            Cursor.Current = Cursors.WaitCursor;

            using (var powerShell = PowerShell.Create())
            {
                powerShell.AddScript($"Enable-NetAdapterBinding -Name '{networkName}' -ComponentID ms_tcpip6");
                powerShell.Invoke();

                if (powerShell.HadErrors)
                {
                    var errors = powerShell.Streams.Error.ToList();
                    string errorMessage = "";
                    errors.ForEach(error => errorMessage += ($"{error.ToString}\n"));

                    MessageBox.Show(errorMessage);
                }
            }

            Cursor.Current = Cursors.Default;

            btnDisableIPv6.Enabled = true;
            btnEnableIPv6.Enabled = false;
        }
    }
}