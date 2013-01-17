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
    <link href="/Content/Evaluation.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="evaluationContent">
        <%
            var answered = ViewBag.Answered as bool?;

            if (answered.HasValue && answered.Value)
            {
            %>
                <div class="answered">
                    <p>Obrigado por participar da avaliação do BrMobi! Suas respostas serão muito úteis para nós.
                    <b>:)</b></p>
                    <p><a href="/">Continue curtindo...</a></p>
                </div>
            <%
            }
            else
            {
        %>

        <h1>Avaliação</h1>
            
        <p>Sua opinião é muito importante para avaliarmos a experiência proporcionada pelo BrMobi.<br />
        Isso não levará nem <b>10 minutos</b>, prometemos!</p>

        <div class="warning">
            <p>É importante que você responda todas as perguntas antes de enviar sua avaliação.</p>
        </div>

        <%
                var questions = ViewBag.Questions as IList<Question>;

                if (questions != null)
                {
        %>

        <form action="/Evaluation/Send">
        <div class="fieldset">
            <h2>Sobre você</h2>
            <div class="questions">
            <%
                    var profileQuestions = questions.Where(q => q.Category == QuestionCategory.Profile).ToList();

                    foreach (var question in profileQuestions)
                    {
                        switch (question.Id)
                        {
                            case 1:
                                {
                            %>
                            <p><%: question.Description%></p>
                            <label><input type="radio" name="<%: question.Id %>" value="1" /> De 15 a 18</label>
                            <label><input type="radio" name="<%: question.Id %>" value="2" /> De 19 a 22</label>
                            <label><input type="radio" name="<%: question.Id %>" value="3" /> De 23 a 29</label>
                            <label><input type="radio" name="<%: question.Id %>" value="4" /> De 30 a 40</label>
                            <label><input type="radio" name="<%: question.Id %>" value="5" /> Acima de 40</label>
                            <%
                                    break;
                                }

                            case 2:
                                {
                            %>
                            <p><%: question.Description%></p>
                            <label><input type="radio" name="<%: question.Id %>" value="1" /> Nenhuma</label>
                            <label><input type="radio" name="<%: question.Id %>" value="2" /> Até duas</label>
                            <label><input type="radio" name="<%: question.Id %>" value="3" /> Até quatro</label>
                            <label><input type="radio" name="<%: question.Id %>" value="4" /> Até sete</label>
                            <label><input type="radio" name="<%: question.Id %>" value="5" /> Mais de sete</label>
                            <%
                                    break;
                                }

                            case 3:
                                {
                            %>
                            <p><%: question.Description%></p>
                            <label><input type="radio" name="<%: question.Id %>" value="1" /> Nenhum</label>
                            <label><input type="radio" name="<%: question.Id %>" value="2" /> Muito baixo</label>
                            <label><input type="radio" name="<%: question.Id %>" value="3" /> Baixo</label>
                            <label><input type="radio" name="<%: question.Id %>" value="4" /> Médio</label>
                            <label><input type="radio" name="<%: question.Id %>" value="5" /> Alto</label>
                            <%
                                    break;
                                }
                        }
                    }
            %>
            </div>
        </div>

        <div class="fieldset">
            <h2>Sobre o sistema</h2>
            <div class="questions">
            <%
                    var featureQuestions = questions.Where(q => q.Category == QuestionCategory.Features).ToList();

                    foreach (var question in featureQuestions)
                    {
                %>
                    <p><%: question.Description%></p>
                    <label><input type="radio" name="<%: question.Id %>" value="-2" /> Discorda totalmente</label>
                    <label><input type="radio" name="<%: question.Id %>" value="-1" /> Discorda</label>
                    <label><input type="radio" name="<%: question.Id %>" value="0" /> Não sabe</label>
                    <label><input type="radio" name="<%: question.Id %>" value="1" /> Concorda</label>
                    <label><input type="radio" name="<%: question.Id %>" value="2" /> Concorda totalmente</label>
                <%
                    }

                    var usabilityQuestions = questions.Where(q => q.Category == QuestionCategory.Usability).ToList();

                    foreach (var question in usabilityQuestions)
                    {
                %>
                    <p><%: question.Description%></p>
                    <label><input type="radio" name="<%: question.Id %>" value="-2" /> Discorda totalmente</label>
                    <label><input type="radio" name="<%: question.Id %>" value="-1" /> Discorda</label>
                    <label><input type="radio" name="<%: question.Id %>" value="0" /> Não sabe</label>
                    <label><input type="radio" name="<%: question.Id %>" value="1" /> Concorda</label>
                    <label><input type="radio" name="<%: question.Id %>" value="2" /> Concorda totalmente</label>
                <%
                    }
            %>
            </div>
        </div>

        <div class="fieldset">
            <h2>Para concluir</h2>
            <div class="questions">
            <%
                    var generalQuestions = questions.Where(q => q.Category == QuestionCategory.General).ToList();

                    foreach (var question in generalQuestions)
                    {
                %>
                    <p><%: question.Description%></p>
                    <label><input type="radio" name="<%: question.Id %>" value="1" /> Muito ruim</label>
                    <label><input type="radio" name="<%: question.Id %>" value="2" /> Ruim</label>
                    <label><input type="radio" name="<%: question.Id %>" value="3" /> Regular</label>
                    <label><input type="radio" name="<%: question.Id %>" value="4" /> Bom</label>
                    <label><input type="radio" name="<%: question.Id %>" value="5" /> Muito bom</label>
                <%
                    }
            %>
            </div>
        </div>

        <div class="buttons">
            <input type="reset" name="clear" value="Limpar" />
            <input type="submit" name="send" value="Enviar" />
        </div>
        </form>

        <%  }
            } %>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/App/Evaluation.js") %>" type="text/javascript"></script>
</asp:Content>
