<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="BrMobi.Core" %>
<%@ Import Namespace="BrMobi.Core.Entities" %>

<%
    if (Session["User"] != null)
    {
        var user = (User)Session["User"];

        var canEvaluate = (bool)Session["CanEvaluate"];
%>

    <div id="loggedUser">

        <%
        if (canEvaluate)
        {
        %>
            <a href="/Avaliacao" class="evaluate">Avalie o BrMobi</a>
        <%
        }
        %>

        <a href="/Perfil"><img class="picture" src="<%: user.Picture %>" alt="Foto" title="Ver perfil" /></a>
        <span class="name">
            <%: user.Name %>
        </span>
        <span class="logoff">
            <%: Html.ActionLink("Sair", "", "Sair") %>
        </span>
    </div>
<%
    }
%>