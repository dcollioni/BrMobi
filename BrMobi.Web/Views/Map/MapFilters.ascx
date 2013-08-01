<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="mapFilters">
    <%--<span class="button bus active" title="Pontos de ônibus">
        <img src="/Content/Images/busIcon.png" alt="Ônibus" />
    </span>--%>
    <span class="button rideOffer active" title="Caronas oferecidas">
        <img src="/Content/Images/rideOfferIcon.png" alt="Caronas oferecidas" />
    </span>
    <span class="button rideRequest active" title="Pedidos de carona">
        <img src="/Content/Images/rideRequestIcon.png" alt="Pedidos de carona" />
    </span>
    <span class="button help active" title="Dúvidas">
        <img src="/Content/Images/helpIcon.png" alt="Dúvida" />
    </span>
    <div></div>
    <span class="tip">
        <img src="/Content/Images/up-arrow.png" alt="Seta" title="" />
    </span>
    <span class="tip">
        Filtre pelos
    </span>
    <span class="tip">
        pontos.
    </span>
</div>