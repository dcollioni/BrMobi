<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>BrMobi</title>

    <link rel="icon" type="image/vnd.microsoft.icon" href="../../Content/Images/favicon.ico" />
	<link rel="shortcut icon" type="image/vnd.microsoft.icon" href="../../Content/Images/favicon.ico" />

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
</body>
</html>