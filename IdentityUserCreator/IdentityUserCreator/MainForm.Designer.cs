namespace IdentityUserCreator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpAddRole = new System.Windows.Forms.GroupBox();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.lblRoleName = new System.Windows.Forms.Label();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.grpAddUser = new System.Windows.Forms.GroupBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.grpAddRole.SuspendLayout();
            this.grpAddUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAddRole
            // 
            this.grpAddRole.Controls.Add(this.btnAddRole);
            this.grpAddRole.Controls.Add(this.lblRoleName);
            this.grpAddRole.Controls.Add(this.txtRoleName);
            this.grpAddRole.Location = new System.Drawing.Point(13, 13);
            this.grpAddRole.Name = "grpAddRole";
            this.grpAddRole.Size = new System.Drawing.Size(304, 75);
            this.grpAddRole.TabIndex = 0;
            this.grpAddRole.TabStop = false;
            this.grpAddRole.Text = "New role";
            this.grpAddRole.Enter += new System.EventHandler(this.grpAddRole_Enter);
            // 
            // btnAddRole
            // 
            this.btnAddRole.Location = new System.Drawing.Point(218, 45);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(75, 23);
            this.btnAddRole.TabIndex = 2;
            this.btnAddRole.Text = "Add role";
            this.btnAddRole.UseVisualStyleBackColor = true;
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.Location = new System.Drawing.Point(6, 22);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(38, 13);
            this.lblRoleName.TabIndex = 0;
            this.lblRoleName.Text = "&Name:";
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(143, 19);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(150, 20);
            this.txtRoleName.TabIndex = 1;
            // 
            // grpAddUser
            // 
            this.grpAddUser.Controls.Add(this.btnAddUser);
            this.grpAddUser.Controls.Add(this.lblPassword);
            this.grpAddUser.Controls.Add(this.txtPassword);
            this.grpAddUser.Controls.Add(this.cmbRole);
            this.grpAddUser.Controls.Add(this.lblRole);
            this.grpAddUser.Controls.Add(this.txtUserName);
            this.grpAddUser.Controls.Add(this.lblUserName);
            this.grpAddUser.Location = new System.Drawing.Point(13, 94);
            this.grpAddUser.Name = "grpAddUser";
            this.grpAddUser.Size = new System.Drawing.Size(304, 134);
            this.grpAddUser.TabIndex = 1;
            this.grpAddUser.TabStop = false;
            this.grpAddUser.Text = "New user";
            this.grpAddUser.Enter += new System.EventHandler(this.grpAddUser_Enter);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(218, 99);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 6;
            this.btnAddUser.Text = "Add user";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 76);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "&Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(143, 73);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(143, 45);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(150, 21);
            this.cmbRole.TabIndex = 3;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(6, 48);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(32, 13);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "&Role:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(143, 19);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(150, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(6, 22);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "&User name:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 242);
            this.Controls.Add(this.grpAddUser);
            this.Controls.Add(this.grpAddRole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "ASP.NET Identity Roles and Users";
            this.grpAddRole.ResumeLayout(false);
            this.grpAddRole.PerformLayout();
            this.grpAddUser.ResumeLayout(false);
            this.grpAddUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAddRole;
        private System.Windows.Forms.Button btnAddRole;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.GroupBox grpAddUser;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.TextBox txtUserName;

    }
}

