<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="BrMobi.Core" %>
<%@ Import Namespace="BrMobi.Core.Entities.Evaluation" %>
<%@ Import Namespace="BrMobi.Core.Evaluation" %>
<%@ Import Namespace="BrMobi.Core.ViewModels" %>
<%@ Import Namespace="BrMobi.Core.Enums.Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BrMobi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LinksContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%
            var answers = ViewBag.UserAnswers as IList<UserAnswer>;

            if (answers != null)
            {
                foreach (var answer in answers.OrderBy(a => a.AnsweredBy.Id))
                {
                %>
                    <div>
                        Usuário: <%: answer.AnsweredBy.Name %><br />
                        Pergunta: <%: answer.Question.Description %><br />
                        Resposta: <%: answer.Value %>
                    </div>
                    <br />
                <%
                }
            }
        %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>