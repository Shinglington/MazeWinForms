namespace PRJ_MazeWinForms
{
    partial class MenuForm
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
            this.menuLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btn_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuLayout
            // 
            this.menuLayout.ColumnCount = 3;
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.menuLayout.Controls.Add(this.btn_start, 1, 1);
            this.menuLayout.Controls.Add(this.label1, 1, 0);
            this.menuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuLayout.Location = new System.Drawing.Point(0, 0);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.RowCount = 3;
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.Size = new System.Drawing.Size(800, 450);
            this.menuLayout.TabIndex = 0;
            // 
            // btn_start
            // 
            this.btn_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btn_start.Location = new System.Drawing.Point(286, 170);
            this.btn_start.Margin = new System.Windows.Forms.Padding(20);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(226, 110);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.label1.Location = new System.Drawing.Point(269, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 150);
            this.label1.TabIndex = 1;
            this.label1.Text = "Maze";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuLayout);
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.menuLayout.ResumeLayout(false);
            this.menuLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel menuLayout;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label1;
    }
}

