<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LinksContent" runat="server">
    <link href="/Content/Register.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="registerForm">
        <div class="header">
            <h1>Crie sua conta</h1>
        </div>
        <div class="content">
            <form action="Cadastro" method="post">
                <div class="label">
                    <label for="name">Nome</label>
                </div>
                <div class="input">
                    <input type="text" name="name" id="name" autofocus />
                </div>
                <div class="label">
                    <label for="email">E-mail</label>
                </div>
                <div class="input">
                    <input type="text" name="email" id="email" class="required" />
                </div>
                <div class="label">
                    <label for="password">Senha</label>
                </div>
                <div class="input">
                    <input type="password" name="password" id="password" class="required" />
                </div>
                <div class="button">
                    <input type="reset" name="clear" value="Limpar" />
                    <input type="submit" name="register" value="Criar conta" />
                </div>
            </form>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Register.js") %>" type="text/javascript"></script>
</asp:Content>