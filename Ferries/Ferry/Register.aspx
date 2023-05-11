<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Ferry.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Sign Up</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" />

</head>
<body style="font-family: Arial, Helvetica, sans-serif; font-size: small">
    <form id="form1" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <h1>Sign Up</h1>
        </div>
        
        <hr />
        <p class="row justify-content-center">
            <asp:Literal runat="server" ID="StatusMessage" />
        </p>                
        <div class="row justify-content-center" style="margin-bottom:10px">
            <div>
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control"  placeholder = "Username" />                
            </div>
        </div>
        <div class="row justify-content-center" style="margin-bottom:10px">
            <div>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control"  placeholder = "Password"/>                
            </div>
        </div>
        <div class="row justify-content-center" style="margin-bottom:10px">
            <div>
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control"  placeholder = "ConfirmPassword"/>                
            </div>
        </div>
        <div>
            <div class="row justify-content-center">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Sign Up" cssClass="btn btn-outline-primary"/>
            </div>
        </div>
        <div>
            <div class="row justify-content-center mt-3">
                Already a user? <asp:LinkButton runat="server" OnClick="Login" Text="Login" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
