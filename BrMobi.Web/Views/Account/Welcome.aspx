﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>BrMobi</title>

    <link rel="icon" type="image/vnd.microsoft.icon" href="../../Content/Images/favicon.ico" />
	<link rel="shortcut icon" type="image/vnd.microsoft.icon" href="../../Content/Images/favicon.ico" />
    <script src="../../Scripts/Default/jquery-1.5.1.min.js" type="text/javascript"></script>

    <style type="text/css">
        @font-face
        {
            font-family: SegoeUI;
            font-style: normal;
            font-weight: normal;
            src: url(../../Content/Fonts/seguisb.ttf), url('../../Content/Fonts/seguisb.eot');
        }

        @font-face
        {
            font-family: SegoeUILight;
            font-style: normal;
            font-weight: normal;
            src: url(../../Content/Fonts/segoeuil.ttf), url('../../Content/Fonts/segoeuil.eot');
        }
        
        body
        {
            background: url('../../Content/Images/welcome-bg2.png') no-repeat 0px -200px;
            margin: 0;
            -webkit-text-stroke-width: 0.1px;
        }
        
        .unselectable
        {
            -webkit-user-select: none !important;
            -khtml-user-select: none !important;
            -moz-user-select: none !important;
            -o-user-select: none !important;
            user-select: none !important;
        }
        
        #header
        {
            background: url('../../Content/Images/header-transparent-bg.png');
            box-shadow: 0 0 10px #000 inset;
            cursor: default;
            height: 200px;
            margin-top: 0;
            text-align: center;
            width: 100%;
        }

        #header h1
        {
            color: #CCC;
            font-family: SegoeUI;
            font-size: 40pt;
            padding-top: 10px;
            margin-bottom: 20px;
            margin-top: 0;
            text-shadow: 1px 1px #000;
        }
        
        #header span
        {
            color: #AAA;
            font-family: SegoeUI;
            font-size: 20pt;
            text-shadow: 1px 1px #000;
        }
        
        #connect
        {
            background: url('../../Content/Images/connect-bg5.png');
            box-shadow: 0px 0px 3px #444;
            box-sizing: border-box;
            display: inline-block;
            margin: 0;
            height: 100%;
            width: 420px;
        }
        
        #connect .facebookButton
        {
            background: url('../../Content/Images/FacebookButton.png') no-repeat;
            display: block;
            height: 70px;
            margin: auto;
            margin-top: 42px;
            width: 375px;
        }
        #connect .facebookButton:hover
        {
            background-position: 0px -81px;
        }
        
        #arrow
        {
            background: url('../../Content/Images/arrow-right.png') no-repeat -21px 45px;
            box-sizing: border-box;
            display: inline-block;
            height: 100%;
            width: 90px;
            margin: 0;
        }
        
        #connectInfo
        {
            background: url('../../Content/Images/connect-bg4.png');
            box-shadow: 0px 0px 3px #222;
            box-sizing: border-box;
            color: #FFF;
            display: inline-block;
            font-family: SegoeUILight;
            font-size: 18pt;
            height: 100%;
            margin: 0;
            padding: 0;
            width: 467px;
        }
        
        #connectInfo .text
        {
            cursor: default;
            display: block;
            height: 100px;
            margin: auto;
            margin-top: 25px;
            margin-left: 22px;
            padding: 0;
            position: absolute;
            width: 425px;
        }
        
        #connectPanel
        {
            box-sizing: border-box;
            height: 150px;
            margin: 20px auto;
            width: 985px;
        }
        
        #page
        {
            height: 582px;
            margin-top: -291px;
            position: absolute;
            top: 50%;
            width: 100%;
        }
        
        #mapPanel
        {
            height: 150px;
        }
        
        #mapPanel .map
        {
            background: url('../../Content/Images/map-bg.png') 0 0;
            box-shadow: 0px 0px 3px #222;
            height: 100%;
            margin: 0 auto;
            opacity: .8;
            width: 100%;
            
            transition: opacity .4s;
        }
        
        #mapPanel .map:hover
        {
            opacity: 1;
        }
        
        #footer
        {
            color: #DDD;
            cursor: default;
            font-family: SegoeUI;
            font-size: 12pt;
            margin-top: 20px;
            text-align: center;
            text-shadow: 1px 1px #222;
        }
        
        #footer a
        {
            color: #BCF;
        }
        
        #mask
        {
            background: url('../../Content/Images/header-transparent-bg.png');
            display: none;
            height: 100%;
            left: 0;
            position: fixed;
            top: 0;
            width: 100%;
        }
        
        #registration
        {
            background-color: #DDD;
            box-shadow: 0 0 20px #000;
            display: none;
            font-family: SegoeUILight;
            margin-top: -165px;
            margin-left: -450px;
            position: absolute;
            width: 900px;
            top: 50%;
            left: 50%;
        }
        
        #registration .title
        {
            color: #444;
            font-size: 18pt;
            margin-top: 20px;
            text-align: center;
            text-shadow: 1px 1px #CCC;
        }
        
        #registration .description
        {
            color: #458;
            font-size: 16pt;
            margin-top: 20px;
            text-align: center;
            text-shadow: 1px 1px #CCC;
        }
        
        #registration .form
        {
            margin-top: 20px;
            margin-bottom: 25px;
            text-align: center;
        }
        
        #registration .form input[type=text]
        {
            background-color: #F5F5F5;
            background-image: url('../../Content/Images/message_outline-26.png');
            background-repeat: no-repeat;
            background-position: 514px 6px;
            border: 1px solid #AAA;
            font-family: SegoeUILight;
            font-size: 16pt;
            padding: 5px;
            outline: none;
            width: 60%;
        }
        
        #registration .form input[type=button]
        {
            border: 1px solid #AAA;
            font-family: SegoeUILight;
            font-size: 14pt;
            margin-right: 10px;
            padding: 10px;
        }

        #registration .form .buttons
        {
            margin-top: 20px;
        }
        
        #registration .form .buttons .send
        {
            background-color: #79F;
            border-color: #468;
        }
        
        #registration .form .buttons .cancel
        {
            background-color: #EEE;
        }
    </style>
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
            <form action="RegisterEmail" method="post">
                <div>
                    <input type="text" name="email" class="email" placeholder="Seu e-mail" />
                </div>
                <div class="buttons">
                    <input type="button" name="send" class="send" value="Desejo ser avisado." />
                    <input type="button" name="cancel" class="cancel" value="Não, obrigado." />
                </div>
            </form>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        $(function () {
            $('.facebookButton').click(function () {
                openRegistration();
            });

            $('#mask').click(function () {
                closeRegistration();
            });

            $('#registration .cancel').click(function () {
                closeRegistration();
            });

            $(document).keyup(function (e) {
                if (e.keyCode == 27) {
                    closeRegistration();
                }
            });

            function openRegistration() {
                $('#mask, #registration').fadeIn();
                $('#registration .email').focus();
            }

            function closeRegistration() {
                $('#mask, #registration').fadeOut();
            }
        });
    </script>
</body>
</html>