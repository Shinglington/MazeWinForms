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
            this.lbl_settings = new System.Windows.Forms.Label();
            this.btn_generate = new System.Windows.Forms.Button();
            this.tbl_basicSettings = new System.Windows.Forms.TableLayoutPanel();
            this.optn_hard = new System.Windows.Forms.RadioButton();
            this.optn_medium = new System.Windows.Forms.RadioButton();
            this.lbl_hard = new System.Windows.Forms.Label();
            this.lbl_medium = new System.Windows.Forms.Label();
            this.lbl_Difficulty = new System.Windows.Forms.Label();
            this.lbl_easy = new System.Windows.Forms.Label();
            this.optn_easy = new System.Windows.Forms.RadioButton();
            this.btn_advSettings = new System.Windows.Forms.Button();
            this.tbl_settingPanel.SuspendLayout();
            this.tbl_basicSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_settingPanel
            // 
            this.tbl_settingPanel.ColumnCount = 3;
            this.tbl_settingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbl_settingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbl_settingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbl_settingPanel.Controls.Add(this.lbl_settings, 1, 0);
            this.tbl_settingPanel.Controls.Add(this.btn_generate, 1, 2);
            this.tbl_settingPanel.Controls.Add(this.tbl_basicSettings, 0, 1);
            this.tbl_settingPanel.Controls.Add(this.btn_advSettings, 2, 2);
            this.tbl_settingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_settingPanel.Location = new System.Drawing.Point(0, 0);
            this.tbl_settingPanel.Name = "tbl_settingPanel";
            this.tbl_settingPanel.RowCount = 3;
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_settingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbl_settingPanel.Size = new System.Drawing.Size(800, 450);
            this.tbl_settingPanel.TabIndex = 0;
            // 
            // lbl_settings
            // 
            this.lbl_settings.AutoSize = true;
            this.lbl_settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lbl_settings.Location = new System.Drawing.Point(269, 0);
            this.lbl_settings.Name = "lbl_settings";
            this.lbl_settings.Size = new System.Drawing.Size(260, 112);
            this.lbl_settings.TabIndex = 0;
            this.lbl_settings.Text = "Settings";
            this.lbl_settings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_generate
            // 
            this.btn_generate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.btn_generate.Location = new System.Drawing.Point(286, 357);
            this.btn_generate.Margin = new System.Windows.Forms.Padding(20);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Padding = new System.Windows.Forms.Padding(10);
            this.btn_generate.Size = new System.Drawing.Size(226, 73);
            this.btn_generate.TabIndex = 1;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = true;
            // 
            // tbl_basicSettings
            // 
            this.tbl_basicSettings.ColumnCount = 2;
            this.tbl_basicSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tbl_basicSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tbl_basicSettings.Controls.Add(this.optn_hard, 1, 3);
            this.tbl_basicSettings.Controls.Add(this.optn_medium, 1, 2);
            this.tbl_basicSettings.Controls.Add(this.lbl_hard, 0, 3);
            this.tbl_basicSettings.Controls.Add(this.lbl_medium, 0, 2);
            this.tbl_basicSettings.Controls.Add(this.lbl_Difficulty, 0, 0);
            this.tbl_basicSettings.Controls.Add(this.lbl_easy, 0, 1);
            this.tbl_basicSettings.Controls.Add(this.optn_easy, 1, 1);
            this.tbl_basicSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_basicSettings.Location = new System.Drawing.Point(3, 115);
            this.tbl_basicSettings.Name = "tbl_basicSettings";
            this.tbl_basicSettings.RowCount = 4;
            this.tbl_basicSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tbl_basicSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tbl_basicSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tbl_basicSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tbl_basicSettings.Size = new System.Drawing.Size(260, 219);
            this.tbl_basicSettings.TabIndex = 3;
            // 
            // optn_hard
            // 
            this.optn_hard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.optn_hard.AutoSize = true;
            this.optn_hard.Location = new System.Drawing.Point(240, 181);
            this.optn_hard.Name = "optn_hard";
            this.optn_hard.Size = new System.Drawing.Size(14, 13);
            this.optn_hard.TabIndex = 6;
            this.optn_hard.TabStop = true;
            this.optn_hard.UseVisualStyleBackColor = true;
            // 
            // optn_medium
            // 
            this.optn_medium.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.optn_medium.AutoSize = true;
            this.optn_medium.Location = new System.Drawing.Point(240, 120);
            this.optn_medium.Name = "optn_medium";
            this.optn_medium.Size = new System.Drawing.Size(14, 13);
            this.optn_medium.TabIndex = 5;
            this.optn_medium.TabStop = true;
            this.optn_medium.UseVisualStyleBackColor = true;
            // 
            // lbl_hard
            // 
            this.lbl_hard.AutoSize = true;
            this.lbl_hard.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_hard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_hard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbl_hard.Location = new System.Drawing.Point(10, 167);
            this.lbl_hard.Margin = new System.Windows.Forms.Padding(10);
            this.lbl_hard.Name = "lbl_hard";
            this.lbl_hard.Size = new System.Drawing.Size(214, 42);
            this.lbl_hard.TabIndex = 3;
            this.lbl_hard.Text = "Hard";
            this.lbl_hard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_medium
            // 
            this.lbl_medium.AutoSize = true;
            this.lbl_medium.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_medium.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_medium.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbl_medium.Location = new System.Drawing.Point(10, 106);
            this.lbl_medium.Margin = new System.Windows.Forms.Padding(10);
            this.lbl_medium.Name = "lbl_medium";
            this.lbl_medium.Size = new System.Drawing.Size(214, 41);
            this.lbl_medium.TabIndex = 2;
            this.lbl_medium.Text = "Medium";
            this.lbl_medium.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Difficulty
            // 
            this.lbl_Difficulty.AutoSize = true;
            this.lbl_Difficulty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Difficulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbl_Difficulty.Location = new System.Drawing.Point(3, 0);
            this.lbl_Difficulty.Name = "lbl_Difficulty";
            this.lbl_Difficulty.Size = new System.Drawing.Size(228, 35);
            this.lbl_Difficulty.TabIndex = 0;
            this.lbl_Difficulty.Text = "Difficulty";
            this.lbl_Difficulty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_easy
            // 
            this.lbl_easy.AutoSize = true;
            this.lbl_easy.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_easy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_easy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbl_easy.Location = new System.Drawing.Point(10, 45);
            this.lbl_easy.Margin = new System.Windows.Forms.Padding(10);
            this.lbl_easy.Name = "lbl_easy";
            this.lbl_easy.Size = new System.Drawing.Size(214, 41);
            this.lbl_easy.TabIndex = 1;
            this.lbl_easy.Text = "Easy";
            this.lbl_easy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optn_easy
            // 
            this.optn_easy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.optn_easy.AutoSize = true;
            this.optn_easy.Location = new System.Drawing.Point(240, 59);
            this.optn_easy.Name = "optn_easy";
            this.optn_easy.Size = new System.Drawing.Size(14, 13);
            this.optn_easy.TabIndex = 4;
            this.optn_easy.TabStop = true;
            this.optn_easy.UseVisualStyleBackColor = true;
            // 
            // btn_advSettings
            // 
            this.btn_advSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_advSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btn_advSettings.Location = new System.Drawing.Point(562, 367);
            this.btn_advSettings.Margin = new System.Windows.Forms.Padding(30);
            this.btn_advSettings.Name = "btn_advSettings";
            this.btn_advSettings.Size = new System.Drawing.Size(208, 53);
            this.btn_advSettings.TabIndex = 4;
            this.btn_advSettings.Text = "Advanced Settings";
            this.btn_advSettings.UseVisualStyleBackColor = true;
            this.btn_advSettings.Click += new System.EventHandler(this.btn_advSettings_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbl_settingPanel);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.tbl_settingPanel.ResumeLayout(false);
            this.tbl_settingPanel.PerformLayout();
            this.tbl_basicSettings.ResumeLayout(false);
            this.tbl_basicSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_settingPanel;
        private System.Windows.Forms.Label lbl_settings;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.TableLayoutPanel tbl_basicSettings;
        private System.Windows.Forms.RadioButton optn_hard;
        private System.Windows.Forms.RadioButton optn_medium;
        private System.Windows.Forms.Label lbl_hard;
        private System.Windows.Forms.Label lbl_medium;
        private System.Windows.Forms.Label lbl_Difficulty;
        private System.Windows.Forms.Label lbl_easy;
        private System.Windows.Forms.RadioButton optn_easy;
        private System.Windows.Forms.Button btn_advSettings;
    }
}