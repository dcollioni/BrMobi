<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="BrMobi.Core" %>
<%@ Import Namespace="BrMobi.Core.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LinksContent" runat="server">
    <link href="/Content/Profile.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="profileContent">
    <% 
    
        if (Session["User"] != null)
        {
            var loggedUser = (User)Session["User"];
            var user = (UserViewModel)ViewBag.User;

    %>

    <div class="picture">
        <% if (loggedUser.Id != user.Id)
           {
        %>
            <img src="data:image/jpg;base64,<%: user.Picture %>" alt="Imagem do usuário" class="userPicture" />
        <% }
           else
           {
        %>
            <img src="data:image/jpg;base64,<%: user.Picture %>" alt="Imagem do usuário" class="userPicture changePicture" title="Alterar imagem" />
        <%
           }
        %>
    </div>

    <div class="mainInfo">
        <% if (loggedUser.Id != user.Id)
           {
        %>
        <h1><%: user.Name %></h1>
        <p>
            <% if (user.Age > 0)
               { %>

               <%: user.Age %> anos

            <% } %>
        </p>
        <p>
            <% if (!string.IsNullOrEmpty(user.Gender))
               { %>

               <%: user.Gender %>

            <% } %>
        </p>
        <p>
            <% if (!string.IsNullOrEmpty(user.CityName))
               { %>

               <%: user.CityName %>, <%: user.StateCode %>

            <% } %>
        </p>
        <p>
            <% if (!string.IsNullOrEmpty(user.FacebookLink))
               { %>

               <a href="<%: user.FacebookLink %>" target="_blank"><%: user.FacebookLink %></a>

            <% } %>
        </p>
        <% }
           else
           {
        %>
        <form action="Profile/Update" method="post">
            <input type="text" name="name" value="<%: user.Name %>" placeholder="Nome" />

            <input type="text" name="birthDate" value="<%: user.BirthDate.HasValue ? user.BirthDate.Value.ToString("d") : "" %>" placeholder="Data de nascimento" />
            
            <label><input type="radio" name="gender" value="1" <%: user.Gender == "Masculino" ? "checked" : "" %> /> Masculino</label>
            <label><input type="radio" name="gender" value="2" <%: user.Gender == "Feminino" ? "checked" : "" %> /> Feminino</label>

            <br />

            <select name="stateId">
                <option value="0">UF</option>
                <% var states = (IList<State>)ViewBag.States;
                   
                   foreach (var state in states)
                   {
                       %>
                       <option value="<%: state.Id %>" <%: user.StateId == state.Id ? "selected" : "" %>><%: state.Code %></option>
                       <%
                   }
                %>
            </select>
            
            <select name="cityId">
                <option value="0">Cidade</option>
                <% var cities = ViewBag.Cities as IList<City>;

                   if (cities != null)
                   {
                       foreach (var city in cities)
                       {
                           %>
                           <option value="<%: city.Id %>" <%: user.CityId == city.Id ? "selected" : "" %>><%: city.Name %></option>
                           <%
                       }
                   }
                %>
            </select>
            
            <input type="text" name="facebookLink" value="<%: user.FacebookLink %>" placeholder="Link do Facebook" />

            <input type="submit" name="update" value="Atualizar" />
        </form>
        <%
           }
        %>
    </div>

    <div class="messagesPanel">
        <h2>Mural de mensagens</h2>
        <div class="post">
            <textarea name="message" rows="2" cols="50" placeholder="Deixe sua mensagem (ctrl + Enter para enviar)"></textarea>
        </div>
        <div class="list">
            <div class="item">
                <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem do usuário" title="Douglas Collioni" /></a>
                <p>Olá, obrigado pela carona! Me quebrou um galho! Abraços. Virou um grande amigo. Você é o cara, muito prestativo e simpático.</p>
                <p class="date">02/11/2012 11:15</p>
            </div>
            <div class="item">
                <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem do usuário" title="Douglas Collioni" /></a>
                <p>mmmmmm mmmmmmmm mmmmm mmmm mmmmm mmm mmmmmmm mmmmmm mmmmm mmmmmmmmmmm mmmmm mmmmm mmm mmm mmmm mmmm mmmmm mmmmm mmm mmm mmmm</p>
                <p class="date">02/11/2012 09:37</p>
            </div>
            <div class="item">
                <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem do usuário" title="Douglas Collioni" /></a>
                <p>Olá, obrigado pela carona! Me quebrou um galho! Abraços. Virou um grande amigo. Você é o cara, muito prestativo e simpático.</p>
                <p class="date">31/10/2012 17:56</p>
            </div>
            <div class="item">
                <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem do usuário" title="Douglas Collioni" /></a>
            </div>
            <div class="item">
                <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem do usuário" title="Douglas Collioni" /></a>
            </div>
        </div>
    </div>
    
    <div class="relationship">
        <h2>Últimas conexões</h2>
        <div>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
            <a href="/Perfil/{id}"><img src="data:image/jpg;base64,<%: loggedUser.Picture %>" alt="Imagem da conexão" class="relationshipPicture" title="Douglas Collioni" /></a>
        </div>
    </div>

    <%  } %>
</div>

</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Default/jquery.maskedinput-1.3.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/App/Profile.js") %>" type="text/javascript"></script>
</asp:Content>