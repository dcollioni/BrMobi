<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Welcome</title>
    <style type="text/css">
        body
        {
            background: url('../../Content/Images/welcome-bg2.png') no-repeat 0px -200px;
            margin: 0;
        }
        
        #header
        {
            background: url('../../Content/Images/header-transparent-bg.png');
            height: 200px;
            margin-top: 50px;
            text-align: center;
            width: 100%;
        }

        #header h1
        {
            color: #EEE;
            font-family: "Segoe UI Semibold";
            font-size: 40pt;
            padding-top: 10px;
            margin-bottom: 20px;
            text-shadow: 1px 1px #000;
        }
        
        #header span
        {
            color: #DDD;
            font-family: "Segoe UI Semibold";
            font-size: 20pt;
            text-shadow: 1px 1px #000;
        }
        
        #connect
        {
            background: url('../../Content/Images/connect-bg5.png');
            box-shadow: 0px 0px 3px #444;
            color: #FFF;
            display: inline-block;
            font-family: "Segoe UI";
            font-size: 18pt;
            height: 70px;
            margin: 20px 0 0 10px;
            padding: 25px 23px 23px 23px;
            width: 375px;
            text-align: center;
        }
        
        #connect .facebookButton
        {
            background: url('../../Content/Images/FacebookButton.png') no-repeat;
            display: block;
            height: 70px;
            width: 375px;
        }
        #connect .facebookButton:hover
        {
            background-position: 0px -81px;
        }
        
        #arrow
        {
            background: url('../../Content/Images/arrow-right.png') no-repeat -15px 32px;
            display: inline-block;
            height: 70px;
            width: 50px;
            margin: 20px 0 0 0;
            padding: 25px 23px 23px 23px;
        }
        
        #connectInfo
        {
            background: url('../../Content/Images/connect-bg4.png');
            box-shadow: 0px 0px 3px #444;
            color: #FFF;
            display: inline-block;
            font-family: "Segoe UI Light";
            font-size: 18pt;
            height: 70px;
            margin: 20px 0 0 0;
            padding: 25px 23px 23px 23px;
        }
        
        #connectInfo p
        {
            margin-top: -15px;
            padding: 0;
            /*(padding: 00px 20px;*/
            /*text-shadow: 1px 1px #666;*/
        }
        
        #connectPanel
        {
            margin: 0 auto;
            width: 1013px;
        }
    </style>
</head>
<body>
    <div id="header">
        <h1>BrMobi</h1>
        <span>Já pensou em oferecer e pedir caronas entre amigos?<br />Então entre e sinta-se à vontade.</span>
    </div>

    <div id="connectPanel">
        <div id="connect">
            <a href="#" class="facebookButton"></a>
        </div>

        <div id="arrow"></div>

        <div id="connectInfo">
            <p>É seguro, <br />não precisa de cadastro, <br />e você pode compartilhar com os amigos.</p>
        </div>
    </div>
</body>
</html>
