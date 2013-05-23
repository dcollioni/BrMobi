<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Welcome</title>
    <style type="text/css">
        body
        {
            background: url('../../Content/Images/welcome-bg2.png') no-repeat -300px -400px;
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
            color: #DDD;
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
    </style>
</head>
<body>
    <div id="header">
        <h1>BrMobi</h1>
        <span>Já pensou em poder compartilhar suas opções de transporte, oferecer e pedir caronas entre amigos?<br />Então entre e sinta-se à vontade.</span>
    </div>
</body>
</html>
