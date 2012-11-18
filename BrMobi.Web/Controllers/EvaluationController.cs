using System.Collections.Generic;
using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces.Evaluation;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class EvaluationController : BaseController
    {
        private readonly IQuestionService questionService;
        private readonly IEvaluationService evaluationService;

        public EvaluationController(IQuestionService questionService,
                                    IEvaluationService evaluationService)
        {
            this.questionService = questionService;
            this.evaluationService = evaluationService;
        }

        public ActionResult Index()
        {
            if (CanEvaluate)
            {
                ViewBag.Questions = questionService.ListAll();
            }
            else
            {
                ViewBag.Answered = true;
            }
            
            return View();
        }

        public ActionResult Send()
        {
            var valid = true;
            var answers = new Dictionary<int, int>();
            int value;

            for (int i = 1; i < 13; i++)
            {
                if (!int.TryParse(Request[i.ToString()], out value))
                {
                    valid = false;
                }
                else
                {
                    answers[i] = value;
                }
            }

            if (valid)
            {
                evaluationService.SaveUserAnswers(answers, LoggedUser);
                CanEvaluate = false;
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Result()
        {
            ViewBag.UserAnswers = evaluationService.ListAllUserAnswers();
            return View();
        }
    }
}