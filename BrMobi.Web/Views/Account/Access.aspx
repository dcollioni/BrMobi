<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" ViewStateMode="Enabled" %>
<%@ Import Namespace="BrMobi.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LinksContent" runat="server">
    <link href="/Content/Logon.css" rel="stylesheet" type="text/css" />
    <link href="/Content/Register.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="fb-root"></div>
<script type="text/javascript">
    window.fbAsyncInit = function () {
        FB.init({
            appId: '597380940272164',
            status: true,
            cookie: true,
            xfbml: true
        });

        FB.Event.subscribe('auth.authResponseChange', function (response) {
            if (response.status === 'connected') {
                var uid = response.authResponse.userID;
                var accessToken = response.authResponse.accessToken;

                var form = document.createElement("form");
                form.setAttribute("method", 'post');
                form.setAttribute("action", '/FacebookLogin.ashx');

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'accessToken');
                field.setAttribute("value", accessToken);
                form.appendChild(field);

                document.body.appendChild(form);
                form.submit();
            } else if (response.status === 'not_authorized') {
            } else {
            }
        });
    };
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/pt_BR/all.js";
        ref.parentNode.insertBefore(js, ref);
    } (document));
</script>
<div class="fb-login-button" data-show-faces="true" data-width="400" data-max-rows="2"></div>

    <div id="logonForm">
        <div class="content">
            <form action="Entrar" method="post">
                <div class="label">
                    <label for="lEmail">E-mail</label>
                </div>
                <div class="input">
                    <input type="text" name="email" id="lEmail" maxlength="50" autofocus />
                </div>
                <div class="label">
                    <label for="lPassword">Senha</label>
                </div>
                <div class="input">
                    <input type="password" name="password" id="lPassword" maxlength="25" />
                </div>
                <div class="button">
                    <input type="submit" name="logon" value="Entrar" />
                </div>
            </form>
        </div>
    </div>
    
    <div id="registerForm">
        <div class="header">
            <h1>Crie sua conta</h1>
        </div>
        <div class="content">

            <%
                var name = string.Empty;
                var email = string.Empty;
                
                if (Model != null)
	            {
                    var model = (RegisterModel)Model;
                    
                    name = model.Name;
                    email = model.Email;
	            }
            %>

            <form action="Acesso" method="post" autocomplete="off">
                <div class="label">
                    <label for="name">Nome</label>
                </div>
                <div class="input">
                    <input type="text" name="name" id="name" maxlength="50" value="<%: name %>" />
                </div>
                <div class="label">
                    <label for="email">E-mail</label>
                </div>
                <div class="input">

                    <%
                        var emailClass = ViewData["EmailError"] != null ? "error" : string.Empty;
                        ViewData["EmailError"] = ViewData["EmailError"] ?? string.Empty;
                    %>

                    <input type="text" name="email" id="email" maxlength="50" value="<%: email %>" class="<%: emailClass %>" title="<%: ViewData["EmailError"] %>" />
                </div>
                <div class="label">
                    <label for="password">Senha</label>
                </div>
                <div class="input">

                    <%
                        var passwordClass = ViewData["PasswordError"] != null ? "error" : string.Empty;
                        ViewData["PasswordError"] = ViewData["PasswordError"] ?? string.Empty;
                    %>

                    <input type="password" name="password" id="password" maxlength="25" class="<%: passwordClass %>" title="<%: ViewData["PasswordError"] %>" />
                </div>
                <div class="button">
                    <input type="reset" name="clear" value="Limpar" />
                    <input type="submit" name="register" value="Criar conta" />
                </div>
            </form>
        </div>
    </div>
    <div id="visual">
        <img src="/Content/Images/rs_mobi_60n.png" alt="Visual" />
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/App/Register.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/App/Logon.js") %>" type="text/javascript"></script>
</asp:Content>