<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="BrMobi.Core" %>

<%
    if (Session["User"] != null)
    {
        var user = (User)Session["User"];
        var picture = !string.IsNullOrEmpty(user.Picture) ? string.Format("data:image/jpg;base64,{0}", user.Picture) : "Content/Images/person.png";
%>
    <div id="loggedUser">
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