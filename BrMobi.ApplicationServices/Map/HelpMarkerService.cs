using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core;
using BrMobi.Core.Extensions;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.ApplicationServices.Map
{
    public class HelpMarkerService : IHelpMarkerService
    {
        private readonly IHelpMarkerRepository helpMarkerRepository;
        private readonly IAnswerRepository answerRepository;

        public HelpMarkerService(IHelpMarkerRepository helpMarkerRepository,
                                 IAnswerRepository answerRepository)
        {
            this.helpMarkerRepository = helpMarkerRepository;
            this.answerRepository = answerRepository;
        }

        public HelpMarker UpdateQuestion(int markerId, string question)
        {
            return helpMarkerRepository.UpdateQuestion(markerId, question);
        }

        public string GetHelpMarkerInfoDetails(int id, string template, User loggedUser)
        {
            var marker = helpMarkerRepository.Get(id);

            if (marker != null)
            {
                var question = marker.Question;

                var displayAll = (marker.Owner.Email != loggedUser.Email) ? "block" : "none";
                var displayOwner = (marker.Owner.Email != loggedUser.Email) ? "none" : "block";
                var displayOwner2 = (marker.Owner.Email != loggedUser.Email) ? "none" : "inline";

                var answers = new StringBuilder();

                var markerAnswers = answerRepository.List(marker.Id);

                foreach (var answer in markerAnswers.OrderByDescending(a => a.CreatedOn))
                {
                    answers.Append("<div class='item'>");
                    answers.AppendFormat("<input type='hidden' name='answerId' value='{0}' />", answer.Id);
                    answers.AppendFormat("<a href='/Perfil/{0}'><img src='data:image/jpg;base64,{1}' alt='Imagem do usuário' title='{2}' /></a>",
                                            answer.CreatedBy.Id, answer.CreatedBy.Picture, answer.CreatedBy.Name);
                    answers.AppendFormat("<p>{0}</p>", answer.Text);
                    answers.AppendFormat("<p class='date'>{0} {1}", answer.CreatedOn.ToString("d", new CultureInfo("pt-BR")), answer.CreatedOn.ToString("t", new CultureInfo("pt-BR")));
                    
                    if (loggedUser.Id == answer.CreatedBy.Id || loggedUser.Id == marker.Owner.Id)
                    {
                        answers.Append("<input type='button' name='remove' class='remove' value='Excluir' />");
                    }

                    answers.Append("</p></div>");
                }

                template = template.SetParam("question", question)
                                   .SetParam("displayAll", displayAll)
                                   .SetParam("displayOwner", displayOwner)
                                   .SetParam("ownerId", marker.Owner.Id.ToString())
                                   .SetParam("ownerPicture", marker.Owner.Picture)
                                   .SetParam("ownerName", marker.Owner.Name)
                                   .SetParam("date", marker.CreatedOn.ToString("d", new CultureInfo("pt-BR")))
                                   .SetParam("answers", answers.ToString())
                                   .SetParam("displayOwner2", displayOwner2);
            }

            return template;
        }

        public Answer AddAnswer(Answer answer, int markerId)
        {
            return helpMarkerRepository.AddAnswer(answer, markerId);
        }

        public void RemoveAnswer(int answerId, User loggedUser)
        {
            var answer = answerRepository.Get(answerId);

            if (answer.CreatedBy.Id == loggedUser.Id || answer.Marker.Owner.Id == loggedUser.Id)
            {
                helpMarkerRepository.RemoveAnswer(answerId);
            }
        }

        public IList<Answer> ListAnswers(int markerId)
        {
            return answerRepository.List(markerId);
        }

        public void Remove(int markerId, User loggedUser)
        {
            var marker = helpMarkerRepository.Get(markerId);

            if (marker.Owner.Id == loggedUser.Id)
            {
                helpMarkerRepository.Remove(markerId);
            }
        }
    }
}