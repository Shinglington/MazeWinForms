namespace PRJ_MazeWinForms
{
    partial class HighscoresForm
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
            this.tbl_highscorePanel = new System.Windows.Forms.TableLayoutPanel();
            this.highscoresGrid = new System.Windows.Forms.DataGridView();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_toggleGlobal = new System.Windows.Forms.Button();
            this.tbl_highscorePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.highscoresGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tbl_highscorePanel
            // 
            this.tbl_highscorePanel.ColumnCount = 2;
            this.tbl_highscorePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_highscorePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_highscorePanel.Controls.Add(this.btn_toggleGlobal, 0, 1);
            this.tbl_highscorePanel.Controls.Add(this.highscoresGrid, 0, 0);
            this.tbl_highscorePanel.Controls.Add(this.btn_back, 1, 1);
            this.tbl_highscorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_highscorePanel.Location = new System.Drawing.Point(0, 0);
            this.tbl_highscorePanel.Name = "tbl_highscorePanel";
            this.tbl_highscorePanel.RowCount = 2;
            this.tbl_highscorePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tbl_highscorePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbl_highscorePanel.Size = new System.Drawing.Size(389, 450);
            this.tbl_highscorePanel.TabIndex = 0;
            // 
            // highscoresGrid
            // 
            this.highscoresGrid.AllowUserToAddRows = false;
            this.highscoresGrid.AllowUserToDeleteRows = false;
            this.highscoresGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl_highscorePanel.SetColumnSpan(this.highscoresGrid, 2);
            this.highscoresGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.highscoresGrid.Location = new System.Drawing.Point(3, 3);
            this.highscoresGrid.Name = "highscoresGrid";
            this.highscoresGrid.ReadOnly = true;
            this.highscoresGrid.Size = new System.Drawing.Size(383, 331);
            this.highscoresGrid.TabIndex = 0;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_back.Location = new System.Drawing.Point(214, 357);
            this.btn_back.Margin = new System.Windows.Forms.Padding(20);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(155, 73);
            this.btn_back.TabIndex = 1;
            this.btn_back.Text = "Back To Menu";
            this.btn_back.UseVisualStyleBackColor = true;
            // 
            // btn_toggleGlobal
            // 
            this.btn_toggleGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_toggleGlobal.Location = new System.Drawing.Point(20, 357);
            this.btn_toggleGlobal.Margin = new System.Windows.Forms.Padding(20);
            this.btn_toggleGlobal.Name = "btn_toggleGlobal";
            this.btn_toggleGlobal.Size = new System.Drawing.Size(154, 73);
            this.btn_toggleGlobal.TabIndex = 2;
            this.btn_toggleGlobal.Text = "Show Personal Scores Only";
            this.btn_toggleGlobal.UseVisualStyleBackColor = true;
            // 
            // HighscoresForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 450);
            this.Controls.Add(this.tbl_highscorePanel);
            this.Name = "HighscoresForm";
            this.Text = "Highscores";
            this.tbl_highscorePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.highscoresGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_highscorePanel;
        private System.Windows.Forms.Button btn_toggleGlobal;
        private System.Windows.Forms.DataGridView highscoresGrid;
        private System.Windows.Forms.Button btn_back;
    }
}