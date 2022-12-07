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
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_highscores = new System.Windows.Forms.Button();
            this.lbl_signedinas = new System.Windows.Forms.Label();
            this.menuLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuLayout
            // 
            this.menuLayout.ColumnCount = 3;
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.menuLayout.Controls.Add(this.btn_login, 1, 2);
            this.menuLayout.Controls.Add(this.btn_start, 1, 1);
            this.menuLayout.Controls.Add(this.label1, 1, 0);
            this.menuLayout.Controls.Add(this.btn_highscores, 0, 0);
            this.menuLayout.Controls.Add(this.lbl_signedinas, 2, 0);
            this.menuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuLayout.Location = new System.Drawing.Point(0, 0);
            this.menuLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.RowCount = 3;
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.menuLayout.Size = new System.Drawing.Size(1200, 692);
            this.menuLayout.TabIndex = 0;
            // 
            // btn_login
            // 
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btn_login.Location = new System.Drawing.Point(429, 491);
            this.btn_login.Margin = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(340, 170);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            // 
            // btn_start
            // 
            this.btn_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btn_start.Location = new System.Drawing.Point(429, 261);
            this.btn_start.Margin = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(340, 168);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.label1.Location = new System.Drawing.Point(403, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 230);
            this.label1.TabIndex = 1;
            this.label1.Text = "Maze";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_highscores
            // 
            this.btn_highscores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_highscores.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_highscores.Location = new System.Drawing.Point(30, 31);
            this.btn_highscores.Margin = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.btn_highscores.Name = "btn_highscores";
            this.btn_highscores.Size = new System.Drawing.Size(339, 168);
            this.btn_highscores.TabIndex = 3;
            this.btn_highscores.Text = "Highscores";
            this.btn_highscores.UseVisualStyleBackColor = true;
            // 
            // lbl_signedinas
            // 
            this.lbl_signedinas.AutoSize = true;
            this.lbl_signedinas.Location = new System.Drawing.Point(803, 0);
            this.lbl_signedinas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_signedinas.Name = "lbl_signedinas";
            this.lbl_signedinas.Size = new System.Drawing.Size(100, 20);
            this.lbl_signedinas.TabIndex = 5;
            this.lbl_signedinas.Text = "Signed in as:";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.menuLayout);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MenuForm";
            this.Text = "Maze Game - Menu";
            this.menuLayout.ResumeLayout(false);
            this.menuLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel menuLayout;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_highscores;
        private System.Windows.Forms.Label lbl_signedinas;
    }
}

