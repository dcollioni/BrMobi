<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="BrMobi.Core" %>

<%
    if (Session["User"] != null)
    {
        var user = (User)Session["User"];
        var picture = !string.IsNullOrEmpty(user.Picture) ? string.Format("data:image/jpg;base64,{0}", user.Picture) : "Content/Images/person.png";

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

        <a href="/Perfil"><img class="picture" src="<%: picture %>" alt="Foto" title="Ver perfil" /></a>
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