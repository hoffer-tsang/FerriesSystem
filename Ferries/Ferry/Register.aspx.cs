using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Text.RegularExpressions;

namespace Ferry
{
    public partial class Register : System.Web.UI.Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var password = Password.Text;
            if (password.Length < 12 || !IsAllPresent(password))
            {
                StatusMessage.Text = "Password has to be longer then 12 characters, including Capital, letter, number and special character.";
            }
            if (password != ConfirmPassword.Text)
            {
                StatusMessage.Text = "Password does not match.";
            }
            else
            {
                // Default UserStore constructor uses the default connection string named: DefaultConnection
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var user = new IdentityUser() { UserName = UserName.Text };
                IdentityResult result = manager.Create(user, Password.Text);
                if (result.Succeeded)
                {
                    result = manager.AddToRole(
                            userId: user.Id,
                            role: "Customer");
                }
                if (result.Succeeded)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                    Response.Redirect("~/Customer/Home");
                }
                else
                {
                    StatusMessage.Text = result.Errors.FirstOrDefault();
                }
            }
        }
        protected void Login(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
        private bool IsAllPresent(string str)
        {
            string regex = "^(?=.*[a-z])(?=."
                        + "*[A-Z])(?=.*\\d)"
                        + "(?=.*[-+_!@#$%^&*., ?]).+$";

            Regex p = new Regex(regex);

            if (str == null)
            {
                return false;
            }
            Match m = p.Match(str);

            // Print Yes if string
            // matches ReGex
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
