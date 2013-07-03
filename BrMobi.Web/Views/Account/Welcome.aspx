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
            box-shadow: 0 0 10px #000 inset;
            height: 200px;
            margin-top: 50px;
            text-align: center;
            width: 100%;
        }

        #header h1
        {
            color: #CCC;
            font-family: "Segoe UI Semibold";
            font-size: 40pt;
            padding-top: 10px;
            margin-bottom: 20px;
            text-shadow: 1px 1px #000;
        }
        
        #header span
        {
            color: #AAA;
            font-family: "Segoe UI Semibold";
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
            box-shadow: 0px 0px 3px #444;
            box-sizing: border-box;
            color: #FFF;
            display: inline-block;
            font-family: "Segoe UI Light";
            font-size: 18pt;
            height: 100%;
            margin: 0;
            padding: 0;
            width: 467px;
        }
        
        #connectInfo .text
        {
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
            <div class="text">
                É seguro, <br />não precisa de cadastro, <br />e você pode compartilhar com os amigos.
            </div>
        </div>
    </div>
</body>
</html>