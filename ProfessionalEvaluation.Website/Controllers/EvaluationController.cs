using ProfessionalEvaluation.Model;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessionalEvaluation.Website.Controllers
{
    public class EvaluationController : Controller
    {
        //
        // GET: /Evaluation/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            List<EvaluationTO> list = Evaluation.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSections(int evaluationID)
        {
            Evaluation evaluation = new Evaluation(evaluationID);
            List<SectionTO> list = evaluation.GetSections();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
