namespace PRJ_MazeWinForms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbl_settingPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btn_back = new System.Windows.Forms.Button();
            this.lbl_settings = new System.Windows.Forms.Label();
            this.btn_generate = new System.Windows.Forms.Button();
            this.btn_advSettings = new System.Windows.Forms.Button();
            this.tbl_settingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_settingPanel
            // 
            this.tbl_settingPanel.ColumnCount = 3;
            this.tbl_settingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbl_settingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbl_settingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbl_settingPanel.Controls.Add(this.btn_back, 2, 2);
            this.tbl_settingPanel.Controls.Add(this.lbl_settings, 1, 0);
            this.tbl_settingPanel.Controls.Add(this.btn_generate, 1, 2);
            this.tbl_settingPanel.Controls.Add(this.btn_advSettings, 0, 2);
            this.tbl_settingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_settingPanel.Location = new System.Drawing.Point(0, 0);
            this.tbl_settingPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbl_settingPanel.Name = "tbl_settingPanel";
            this.tbl_settingPanel.RowCount = 3;
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tbl_settingPanel.Size = new System.Drawing.Size(1200, 692);
            this.tbl_settingPanel.TabIndex = 0;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btn_back.Location = new System.Drawing.Point(845, 565);
            this.btn_back.Margin = new System.Windows.Forms.Padding(45, 46, 45, 46);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(310, 81);
            this.btn_back.TabIndex = 5;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            // 
            // lbl_settings
            // 
            this.lbl_settings.AutoSize = true;
            this.lbl_settings.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lbl_settings.Location = new System.Drawing.Point(404, 0);
            this.lbl_settings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_settings.Name = "lbl_settings";
            this.lbl_settings.Size = new System.Drawing.Size(392, 69);
            this.lbl_settings.TabIndex = 0;
            this.lbl_settings.Text = "Settings";
            this.lbl_settings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_generate
            // 
            this.btn_generate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.btn_generate.Location = new System.Drawing.Point(430, 550);
            this.btn_generate.Margin = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.btn_generate.Size = new System.Drawing.Size(340, 111);
            this.btn_generate.TabIndex = 1;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = true;
            // 
            // btn_advSettings
            // 
            this.btn_advSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_advSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btn_advSettings.Location = new System.Drawing.Point(45, 565);
            this.btn_advSettings.Margin = new System.Windows.Forms.Padding(45, 46, 45, 46);
            this.btn_advSettings.Name = "btn_advSettings";
            this.btn_advSettings.Size = new System.Drawing.Size(310, 81);
            this.btn_advSettings.TabIndex = 4;
            this.btn_advSettings.Text = "Advanced Settings";
            this.btn_advSettings.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.tbl_settingPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsForm";
            this.Text = "Maze Game - Select Maze Settings";
            this.tbl_settingPanel.ResumeLayout(false);
            this.tbl_settingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_settingPanel;
        private System.Windows.Forms.Label lbl_settings;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Button btn_advSettings;
        private System.Windows.Forms.Button btn_back;
    }
}