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
            this.settingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btn_generate = new System.Windows.Forms.Button();
            this.lbl_settings = new System.Windows.Forms.Label();
            this.settingsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsLayout
            // 
            this.settingsLayout.ColumnCount = 5;
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.Controls.Add(this.btn_generate, 2, 4);
            this.settingsLayout.Controls.Add(this.lbl_settings, 2, 0);
            this.settingsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsLayout.Location = new System.Drawing.Point(0, 0);
            this.settingsLayout.Name = "settingsLayout";
            this.settingsLayout.RowCount = 5;
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsLayout.Size = new System.Drawing.Size(800, 450);
            this.settingsLayout.TabIndex = 0;
            // 
            // btn_generate
            // 
            this.btn_generate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btn_generate.Location = new System.Drawing.Point(330, 370);
            this.btn_generate.Margin = new System.Windows.Forms.Padding(10);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(140, 70);
            this.btn_generate.TabIndex = 0;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = true;
            // 
            // lbl_settings
            // 
            this.lbl_settings.AutoSize = true;
            this.lbl_settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lbl_settings.Location = new System.Drawing.Point(323, 0);
            this.lbl_settings.Name = "lbl_settings";
            this.lbl_settings.Size = new System.Drawing.Size(154, 90);
            this.lbl_settings.TabIndex = 1;
            this.lbl_settings.Text = "Settings";
            this.lbl_settings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.settingsLayout);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.settingsLayout.ResumeLayout(false);
            this.settingsLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel settingsLayout;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Label lbl_settings;
    }
}