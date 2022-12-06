namespace PRJ_MazeWinForms
{
    partial class LoginForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.lbl_signedinas = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.username_field = new System.Windows.Forms.TextBox();
            this.password_field = new System.Windows.Forms.TextBox();
            this.confirmpass_field = new System.Windows.Forms.TextBox();
            this.btn_switchMode = new System.Windows.Forms.Button();
            this.lbl_username = new System.Windows.Forms.Label();
            this.lbl_pass = new System.Windows.Forms.Label();
            this.lbl_confirmPass = new System.Windows.Forms.Label();
            this.lbl_SwitchMode = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_title, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_confirm, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbl_signedinas, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_back, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.username_field, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.password_field, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.confirmpass_field, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_switchMode, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl_username, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_pass, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_confirmPass, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl_SwitchMode, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(110, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(104, 135);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "Login";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_confirm
            // 
            this.btn_confirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_confirm.Location = new System.Drawing.Point(112, 335);
            this.btn_confirm.Margin = new System.Windows.Forms.Padding(5, 20, 5, 20);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(100, 95);
            this.btn_confirm.TabIndex = 3;
            this.btn_confirm.Text = "Login";
            this.btn_confirm.UseVisualStyleBackColor = true;
            // 
            // lbl_signedinas
            // 
            this.lbl_signedinas.AutoSize = true;
            this.lbl_signedinas.Location = new System.Drawing.Point(220, 0);
            this.lbl_signedinas.Name = "lbl_signedinas";
            this.lbl_signedinas.Size = new System.Drawing.Size(68, 13);
            this.lbl_signedinas.TabIndex = 4;
            this.lbl_signedinas.Text = "Signed in as:";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_back.Location = new System.Drawing.Point(222, 365);
            this.btn_back.Margin = new System.Windows.Forms.Padding(5, 50, 5, 50);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(99, 35);
            this.btn_back.TabIndex = 5;
            this.btn_back.Text = "Back To Menu";
            this.btn_back.UseVisualStyleBackColor = true;
            // 
            // username_field
            // 
            this.username_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.username_field.Location = new System.Drawing.Point(110, 138);
            this.username_field.Name = "username_field";
            this.username_field.Size = new System.Drawing.Size(104, 20);
            this.username_field.TabIndex = 1;
            this.username_field.WordWrap = false;
            // 
            // password_field
            // 
            this.password_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.password_field.Location = new System.Drawing.Point(110, 183);
            this.password_field.Name = "password_field";
            this.password_field.Size = new System.Drawing.Size(104, 20);
            this.password_field.TabIndex = 2;
            this.password_field.WordWrap = false;
            // 
            // confirmpass_field
            // 
            this.confirmpass_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmpass_field.Enabled = false;
            this.confirmpass_field.Location = new System.Drawing.Point(110, 228);
            this.confirmpass_field.Name = "confirmpass_field";
            this.confirmpass_field.Size = new System.Drawing.Size(104, 20);
            this.confirmpass_field.TabIndex = 6;
            this.confirmpass_field.Visible = false;
            this.confirmpass_field.WordWrap = false;
            // 
            // btn_switchMode
            // 
            this.btn_switchMode.Location = new System.Drawing.Point(110, 273);
            this.btn_switchMode.Name = "btn_switchMode";
            this.btn_switchMode.Size = new System.Drawing.Size(75, 23);
            this.btn_switchMode.TabIndex = 7;
            this.btn_switchMode.Text = "Register";
            this.btn_switchMode.UseVisualStyleBackColor = true;
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(3, 135);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(55, 13);
            this.lbl_username.TabIndex = 8;
            this.lbl_username.Text = "Username";
            // 
            // lbl_pass
            // 
            this.lbl_pass.AutoSize = true;
            this.lbl_pass.Location = new System.Drawing.Point(3, 180);
            this.lbl_pass.Name = "lbl_pass";
            this.lbl_pass.Size = new System.Drawing.Size(53, 13);
            this.lbl_pass.TabIndex = 9;
            this.lbl_pass.Text = "Password";
            // 
            // lbl_confirmPass
            // 
            this.lbl_confirmPass.AutoSize = true;
            this.lbl_confirmPass.Location = new System.Drawing.Point(3, 225);
            this.lbl_confirmPass.Name = "lbl_confirmPass";
            this.lbl_confirmPass.Size = new System.Drawing.Size(91, 13);
            this.lbl_confirmPass.TabIndex = 10;
            this.lbl_confirmPass.Text = "Confirm Password";
            this.lbl_confirmPass.Visible = false;
            // 
            // lbl_SwitchMode
            // 
            this.lbl_SwitchMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SwitchMode.AutoSize = true;
            this.lbl_SwitchMode.Location = new System.Drawing.Point(25, 270);
            this.lbl_SwitchMode.Name = "lbl_SwitchMode";
            this.lbl_SwitchMode.Size = new System.Drawing.Size(79, 13);
            this.lbl_SwitchMode.TabIndex = 11;
            this.lbl_SwitchMode.Text = "Not signed up?";
            this.lbl_SwitchMode.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.TextBox username_field;
        private System.Windows.Forms.TextBox password_field;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Label lbl_signedinas;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.TextBox confirmpass_field;
        private System.Windows.Forms.Button btn_switchMode;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label lbl_pass;
        private System.Windows.Forms.Label lbl_confirmPass;
        private System.Windows.Forms.Label lbl_SwitchMode;
    }
}