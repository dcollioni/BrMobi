<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>BrMobi</title>
    <% Html.RenderPartial("GoogleAnalytics"); %>
    <link href="http://fonts.googleapis.com/css?family=Ropa+Sans" rel="stylesheet" type="text/css">
    <link rel="icon" type="image/vnd.microsoft.icon" href="../../Content/Images/favicon.ico" />
	<link rel="shortcut icon" type="image/vnd.microsoft.icon" href="../../Content/Images/favicon.ico" />
    <script src="../../Scripts/Default/jquery-1.5.1.min.js" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/Welcome.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/FacebookButton.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="page">
        <div id="header" class="unselectable">
            <h1>BrMobi</h1>
            <span>Já pensou em oferecer e pedir caronas entre amigos?<br />Então entre e sinta-se à vontade.</span>
        </div>

        <div id="connectPanel" class="unselectable">
            <div id="connect">
                <a href="#" class="facebookButton"></a>
                <%--<div id="facebook_button" class="centered unselectable fb-login-button">
                    <div id="facebook_logo">f</div>
                    <div id="facebook_text">Entre pelo Facebook</div>
                </div>--%>
            </div>

            <div id="arrow"></div>
        
            <div id="connectInfo">
                <div class="text">
                    É seguro, <br />não precisa de cadastro, <br />e você pode compartilhar com os amigos.
                </div>
            </div>
        </div>

        <div id="mapPanel">
            <div class="map"></div>
        </div>

        <div id="footer" class="unselectable">
            A sua rede de mobilidade urbana. Conheça-nos em <a href="http://facebook.com/BrMobi">facebook.com/BrMobi</a>.
        </div>
    </div>

    <div id="mask"></div>

    <div id="registration">
        <div class="title">
            Ficamos contentes em saber que você tem interesse pelo BrMobi!<br />
            No momento estamos trabalhando para deixá-lo pronto o mais breve possível.
        </div>
        <div class="description">
            Se deseja ser avisado quando ele sair do forno, deixe seu e-mail conosco.<br />
            (Prometemos não enviar <i>spam</i>)
        </div>
        <div class="form">
            <form action="Account/RegisterEmail" method="post">
                <div class="fields">
                    <input type="text" name="email" class="email" placeholder="Seu e-mail" />
                </div>
                <div class="buttons">
                    <input type="submit" name="send" class="send" value="Desejo ser avisado." />
                    <input type="button" name="cancel" class="cancel" value="Não, obrigado." />
                </div>
            </form>
        </div>
        <div class="success">
            <p>Obrigado por deixar seu e-mail!<br />
            Você será avisado com exclusividade quando o BrMobi estiver disponível.</p>
        </div>
    </div>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/App/Account.js") %>"></script>
</body>
</html>