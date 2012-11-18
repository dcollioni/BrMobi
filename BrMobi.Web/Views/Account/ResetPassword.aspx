<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="styleContent" ContentPlaceHolderID="LinksContent" runat="server">
    <link href="/Content/Register.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="resetPassword">
    <h1>Redefinição de Senha</h1>

    <div>

    <%
        var success = ViewBag.Success as bool?;

        if (success.HasValue && success.Value)
        {
        %>
            <p>Você receberá um e-mail com a sua nova senha.</p>
        <%
        }
        else if (success.HasValue)
        {
        %>
            <p>Ocorreu um erro ao atualizar sua senha. Possivelmente seu e-mail cadastrado não está correto.</p>
        <%
        }
        else
        {
        %>
            <br />
            <form action="/Account/ResetPassword" method="post">
                <input type="text" name="email" value="" placeholder="Informe o e-mail cadastrado" autofocus />
                <input type="submit" name="send" value="Enviar" />
            </form>
        <%
        }
    %>

        <p><a href="/">Voltar à página inicial</a></p>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>