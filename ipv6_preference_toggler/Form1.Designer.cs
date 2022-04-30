namespace ipv6_preference_toggler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_prefer_ipv4 = new System.Windows.Forms.Button();
            this.btn_prefer_ipv6 = new System.Windows.Forms.Button();
            this.lbl_horizontal_line = new System.Windows.Forms.Label();
            this.lbl_title_current_preference = new System.Windows.Forms.Label();
            this.lbl_value_current_preference = new System.Windows.Forms.Label();
            this.btn_view_in_regedit = new System.Windows.Forms.Button();
            this.btn_switch_language = new System.Windows.Forms.Button();
            this.lbl_current_value = new System.Windows.Forms.Label();
            this.lbl_title_current_value = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEthernetAdapter = new System.Windows.Forms.Label();
            this.cbNetworkAdapters = new System.Windows.Forms.ComboBox();
            this.btnEnableIPv6 = new System.Windows.Forms.Button();
            this.btnDisableIPv6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_prefer_ipv4
            // 
            resources.ApplyResources(this.btn_prefer_ipv4, "btn_prefer_ipv4");
            this.btn_prefer_ipv4.Name = "btn_prefer_ipv4";
            this.btn_prefer_ipv4.UseVisualStyleBackColor = true;
            this.btn_prefer_ipv4.Click += new System.EventHandler(this.FlipPreferredIPv4Flag);
            // 
            // btn_prefer_ipv6
            // 
            resources.ApplyResources(this.btn_prefer_ipv6, "btn_prefer_ipv6");
            this.btn_prefer_ipv6.Name = "btn_prefer_ipv6";
            this.btn_prefer_ipv6.UseVisualStyleBackColor = true;
            this.btn_prefer_ipv6.Click += new System.EventHandler(this.FlipPreferredIPv4Flag);
            // 
            // lbl_horizontal_line
            // 
            this.lbl_horizontal_line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lbl_horizontal_line, "lbl_horizontal_line");
            this.lbl_horizontal_line.Name = "lbl_horizontal_line";
            // 
            // lbl_title_current_preference
            // 
            resources.ApplyResources(this.lbl_title_current_preference, "lbl_title_current_preference");
            this.lbl_title_current_preference.Name = "lbl_title_current_preference";
            // 
            // lbl_value_current_preference
            // 
            resources.ApplyResources(this.lbl_value_current_preference, "lbl_value_current_preference");
            this.lbl_value_current_preference.Name = "lbl_value_current_preference";
            // 
            // btn_view_in_regedit
            // 
            resources.ApplyResources(this.btn_view_in_regedit, "btn_view_in_regedit");
            this.btn_view_in_regedit.Name = "btn_view_in_regedit";
            this.btn_view_in_regedit.UseVisualStyleBackColor = true;
            this.btn_view_in_regedit.Click += new System.EventHandler(this.btn_view_in_regedit_Click);
            // 
            // btn_switch_language
            // 
            resources.ApplyResources(this.btn_switch_language, "btn_switch_language");
            this.btn_switch_language.Name = "btn_switch_language";
            this.btn_switch_language.UseVisualStyleBackColor = true;
            this.btn_switch_language.Click += new System.EventHandler(this.btn_switch_language_Click);
            // 
            // lbl_current_value
            // 
            resources.ApplyResources(this.lbl_current_value, "lbl_current_value");
            this.lbl_current_value.Name = "lbl_current_value";
            // 
            // lbl_title_current_value
            // 
            resources.ApplyResources(this.lbl_title_current_value, "lbl_title_current_value");
            this.lbl_title_current_value.Name = "lbl_title_current_value";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblEthernetAdapter
            // 
            resources.ApplyResources(this.lblEthernetAdapter, "lblEthernetAdapter");
            this.lblEthernetAdapter.Name = "lblEthernetAdapter";
            // 
            // cbNetworkAdapters
            // 
            this.cbNetworkAdapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNetworkAdapters.FormattingEnabled = true;
            resources.ApplyResources(this.cbNetworkAdapters, "cbNetworkAdapters");
            this.cbNetworkAdapters.Name = "cbNetworkAdapters";
            this.cbNetworkAdapters.SelectedIndexChanged += new System.EventHandler(this.cbNetworkAdapters_SelectedIndexChanged);
            // 
            // btnEnableIPv6
            // 
            resources.ApplyResources(this.btnEnableIPv6, "btnEnableIPv6");
            this.btnEnableIPv6.Name = "btnEnableIPv6";
            this.btnEnableIPv6.UseVisualStyleBackColor = true;
            this.btnEnableIPv6.Click += new System.EventHandler(this.btnEnableIPv6_Click);
            // 
            // btnDisableIPv6
            // 
            resources.ApplyResources(this.btnDisableIPv6, "btnDisableIPv6");
            this.btnDisableIPv6.Name = "btnDisableIPv6";
            this.btnDisableIPv6.UseVisualStyleBackColor = true;
            this.btnDisableIPv6.Click += new System.EventHandler(this.btnDisableIPv6_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDisableIPv6);
            this.Controls.Add(this.btnEnableIPv6);
            this.Controls.Add(this.cbNetworkAdapters);
            this.Controls.Add(this.lblEthernetAdapter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_current_value);
            this.Controls.Add(this.lbl_title_current_value);
            this.Controls.Add(this.btn_switch_language);
            this.Controls.Add(this.btn_view_in_regedit);
            this.Controls.Add(this.lbl_value_current_preference);
            this.Controls.Add(this.lbl_title_current_preference);
            this.Controls.Add(this.lbl_horizontal_line);
            this.Controls.Add(this.btn_prefer_ipv6);
            this.Controls.Add(this.btn_prefer_ipv4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_prefer_ipv4;
        private Button btn_prefer_ipv6;
        private Label lbl_horizontal_line;
        private Label lbl_title_current_preference;
        private Label lbl_value_current_preference;
        private Button btn_view_in_regedit;
        private Button btn_switch_language;
        private Label lbl_current_value;
        private Label lbl_title_current_value;
        private Label label1;
        private Label lblEthernetAdapter;
        private ComboBox cbNetworkAdapters;
        private Button btnEnableIPv6;
        private Button btnDisableIPv6;
    }
}