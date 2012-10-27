<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="BrMobi.Web.Models" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="styleContent" ContentPlaceHolderID="LinksContent" runat="server">
    <link href="/Content/Logon.css" rel="stylesheet" type="text/css" />
    <link href="/Content/Register.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <center>

    <%
        var logonError = ViewData["LogonError"] != null;
        
        if (logonError)
        {
    %>
            <div id="logonError">
                <ul>
                    <li><%: ViewData["LogonError"]%></li>
                </ul>
            </div>
    <%
        }
    %>

    <div id="logonForm2" align="left">
        <div class="header">
            <h1>Entre</h1>
        </div>
        <div class="content">

            <%
                var email = string.Empty;

                if (Model != null)
                {
                    var model = (LogOnModel)Model;
                    email = model.Email;
                }
            %>

            <form action="Entrar" method="post" autocomplete="off" novalidate>
                <div class="label">
                    <label for="email">E-mail</label>
                </div>
                <div class="input">
                    <input type="email" name="email" maxlength="50" value="<%: email %>" required autofocus />
                </div>
                <div class="label">
                    <label for="password">Senha</label>
                </div>
                <div class="input">
                    <input type="password" name="password" maxlength="25" minlength="6" required />
                </div>
                <div class="button">
                    <input type="submit" name="logon" value="Entrar" />
                </div>
            </form>
        </div>
    </div>
    </center>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/App/Logon.js") %>" type="text/javascript"></script>
</asp:Content>