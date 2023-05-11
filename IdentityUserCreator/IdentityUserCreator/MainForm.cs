/*==============================================================================
 *
 * A form that allows roles and users to be created in the database.
 *
 * Copyright © Dorset Software, 2015-2017
 *
 *============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityUserCreator
{
    /// <summary>
    /// A form that allows roles and users to be created in the database.
    /// </summary>
    public partial class MainForm :
        Form
    {
        private IdentityDbContext _Context;
        private RoleStore<IdentityRole> _RoleStore;
        private RoleManager<IdentityRole> _RoleManager;
        private UserManager<IdentityUser> _UserManager;
        private IUserStore<IdentityUser> _UserStore;

        /// <summary>
        /// Initialises a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this._Context = new IdentityDbContext();
            this._RoleStore = new RoleStore<IdentityRole>(this._Context);
            this._RoleManager = new RoleManager<IdentityRole>(this._RoleStore);
            this._UserStore = new UserStore<IdentityUser>(this._Context);
            this._UserManager = new UserManager<IdentityUser>(this._UserStore);
            this.LoadRoles();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> if managed resources should be disposed; otherwise,
        /// <c>false</c>.
        /// </param>
        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }

                this._UserManager.Dispose();
                this._UserStore.Dispose();
                this._RoleManager.Dispose();
                this._RoleStore.Dispose();
                this._Context.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Displays a message box summarising the specified result.
        /// </summary>
        private static void ShowResult(
            IdentityResult result)
        {
            var message = result.Succeeded
                    ? "Success"
                    : string.Join("\n", result.Errors);
            var icon = result.Succeeded
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Warning;

            ShowMessage(message, icon);
        }

        /// <summary>
        /// Displays a message box with the specified warning.
        /// </summary>
        private static void ShowWarning(
            string message,
            Control control = null)
        {
            ShowMessage(message, MessageBoxIcon.Warning);

            if (control != null)
            {
                control.Focus();
            }
        }

        /// <summary>
        /// Displays a message box.
        /// </summary>
        private static void ShowMessage(
            string message,
            MessageBoxIcon icon)
        {
            MessageBox.Show(
                    message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    icon);
        }

        /// <summary>
        /// Populates the drop-down list of user roles.
        /// </summary>
        private void LoadRoles()
        {
            var items = new List<string>();

            items.Add("(Please select)");
            items.AddRange(
                from r in this._RoleManager.Roles
                orderby r.Name
                select r.Name);

            cmbRole.DataSource = items;
        }

        /// <summary>
        /// Makes the "Add role" button the default.
        /// </summary>
        private void grpAddRole_Enter(
            object sender,
            EventArgs e)
        {
            this.AcceptButton = this.btnAddRole;
        }

        /// <summary>
        /// Makes the "Add user" button the default.
        /// </summary>
        private void grpAddUser_Enter(
            object sender,
            EventArgs e)
        {
            this.AcceptButton = this.btnAddUser;
        }

        /// <summary>
        /// Attempts to add a role to the database.
        /// </summary>
        private void btnAddRole_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoleName.Text))
            {
                ShowWarning("Please enter a role name.", this.txtRoleName);
            }
            else
            {
                var result = this._RoleManager.Create(
                        new IdentityRole(txtRoleName.Text));

                ShowResult(result);

                if (result.Succeeded)
                {
                    this.txtRoleName.Clear();
                    this.LoadRoles();
                }
            }
        }

        /// <summary>
        /// Attempts to add a user to the database.
        /// </summary>
        private void btnAddUser_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                ShowWarning("Please enter a user name.", this.txtUserName);
            }
            else if (cmbRole.SelectedIndex == 0)
            {
                ShowWarning("Please select a role for the user.", this.cmbRole);
            }
            else
            {
                var user = new IdentityUser(txtUserName.Text);
                var result = this._UserManager.Create(user, txtPassword.Text);

                if (result.Succeeded)
                {
                    result = this._UserManager.AddToRole(
                            userId: user.Id,
                            role: (string)cmbRole.SelectedItem);
                }

                ShowResult(result);

                if (result.Succeeded)
                {
                    this.txtUserName.Clear();
                    this.cmbRole.SelectedIndex = 0;
                    this.txtPassword.Clear();
                }
            }
        }
    }
}
