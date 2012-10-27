<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (Session["User"] != null)
    {
%>
    <div id="changePicture" class="modal">
        <form action="Account/ChangePicture" method="post" enctype="multipart/form-data">
            <input type="text" name="fake" readonly/>
            <label for="picture">Escolha a foto</label>
            <input type="file" name="picture" id="picture" />
            <input type="submit" name="change" value="Enviar" />
        </form>
    </div>
<%
    }
%>