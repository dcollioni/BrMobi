<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="mapActions" class="unselectable" oncontextmenu="return false;">
    <ul>
        <%--<li class="bus">
            <span class="mark">&nbsp;</span>
            <span class="text">Marcar ponto de ônibus</span>
        </li>--%>
        <li class="rideOffer">
            <span class="mark">&nbsp;</span>
            <span class="text">Marcar carona oferecida</span>
        </li>
        <li class="rideRequest">
            <span class="mark">&nbsp;</span>
            <span class="text">Marcar pedido de carona</span>
        </li>
        <li class="help">
            <span class="mark">&nbsp;</span>
            <span class="text">Marcar dúvida</span>
        </li>
    </ul>
</div>