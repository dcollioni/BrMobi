<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LinksContent" runat="server">
    <link href="/Content/Map.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("../Map/MapHeader"); %>
    <% Html.RenderPartial("../Map/MapFilters"); %>
    <% Html.RenderPartial("../Map/MapActions"); %>
    <div id="mapCanvas"></div>

    <div id="mapMask">
    </div>

    <div id="maskText" class="unselectable">
        <img src="/Content/Images/loading.gif" alt="Carregando" title="" />
        <span>Carregando...</span>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBjtNU_pegJ40QknPN-MBJOnanhf9Bykqs&sensor=true" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/Default/jquery.maskedinput-1.3.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/Default/dateFormat.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/App/Map.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/App/MapFilters.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/App/MapActions.js") %>" type="text/javascript"></script>
</asp:Content>