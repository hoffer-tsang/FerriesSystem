<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ferry.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Login Page</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" />
</head>
<body style="font-family: Arial, Helvetica, sans-serif; font-size: small">
   <form id="form1" runat="server">
   <div class ="container">
          <div class="row justify-content-center">
              <h1 >Log In</h1>
          </div>
                  <hr />
         <div class="row justify-content-center">
             <asp:PlaceHolder runat="server" ID="LoginStatus" Visible="false">
            <p>
               <asp:Literal runat="server" ID="StatusText" />
            </p>
         </asp:PlaceHolder>
         </div >
         <asp:PlaceHolder runat="server" ID="LoginForm" Visible="false">
            <div class="row justify-content-center" style="margin-bottom: 10px">
               <div>
                  <asp:TextBox runat="server" ID="UserName" CssClass="form-control" placeholder = "Username" />
               </div>
            </div>
            <div class="row justify-content-center" style="margin-bottom: 10px">
               <div>
                  <asp:TextBox runat="server" ID="Password" TextMode="Password" class="form-control"  placeholder = "Password"/>
               </div>
            </div>
            <div class="row justify-content-center" style="margin-bottom: 10px">
               <div>
                  <asp:Button runat="server" OnClick="SignIn" Text="Log in" CssClass="btn btn-outline-primary" />
               </div>
            </div>
         </asp:PlaceHolder>
         <asp:PlaceHolder runat="server" ID="SignUpLink" Visible="false">
            <div>
               <div class="row justify-content-center">
                  Need an account? <asp:LinkButton runat="server" OnClick="SignUp" Text="Sign Up" />
               </div>
            </div>
         </asp:PlaceHolder>
      </div>   
   </form>
</body>
</html>
