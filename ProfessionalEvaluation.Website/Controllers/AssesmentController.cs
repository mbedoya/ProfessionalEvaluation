using ProfessionalEvaluation.Model;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessionalEvaluation.Website.Controllers
{
    public class AssesmentController : Controller
    {
        private const string SESSION_ASSESMENT_OBJECT = "Assesment";
        private const string QUERY_STRING_ID = "id";

        //
        // GET: /Assesment/

        public ActionResult Index()
        {
            string assesmentID = Request.QueryString[QUERY_STRING_ID] != null ? Request.QueryString[QUERY_STRING_ID].ToString() : null;

            if (!String.IsNullOrEmpty(assesmentID))
            {
                Assesment assesment = new Assesment(assesmentID);
                Session[SESSION_ASSESMENT_OBJECT] = assesment;
            }

            return CheckAndRoute();
        }

        public ActionResult Instructions()
        {
            return CheckAndRoute();
        }

        public ActionResult Execution()
        {
            return CheckAndRoute();
        }

        public ActionResult AlreadyDone()
        {
            return CheckAndRoute();
        }

        public ActionResult Done()
        {
            return View();
        }

        public ActionResult GetData()
        {
            Assesment assesment = Session[SESSION_ASSESMENT_OBJECT] != null ? (Assesment)Session[SESSION_ASSESMENT_OBJECT] : null;

            if (assesment == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return Json(assesment.GetInfo(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContext()
        {
            Assesment assesment = Session[SESSION_ASSESMENT_OBJECT] != null ? (Assesment)Session[SESSION_ASSESMENT_OBJECT] : null;

            if (assesment == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return Json(assesment.GetCurrentContext(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Start()
        {
            Assesment assesment = Session[SESSION_ASSESMENT_OBJECT] != null ? (Assesment)Session[SESSION_ASSESMENT_OBJECT] : null;

            if (assesment == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);                    
            }

            AssesmentStartOperationState state = assesment.Start();

            return Json(new { result = state.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnswerQuestion(int responseIndex)
        {
            Assesment assesment = Session[SESSION_ASSESMENT_OBJECT] != null ? (Assesment)Session[SESSION_ASSESMENT_OBJECT] : null;

            if (assesment == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                return Json(new { result = assesment.AnswerQuestion(responseIndex).ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }            
        }

        public ActionResult UpdateLeftTime()
        {
            Assesment assesment = Session[SESSION_ASSESMENT_OBJECT] != null ? (Assesment)Session[SESSION_ASSESMENT_OBJECT] : null;

            if (assesment == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            assesment.UpdateLeftTime();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckAndRoute()
        {
            Assesment assesment = Session[SESSION_ASSESMENT_OBJECT] != null ? (Assesment)Session[SESSION_ASSESMENT_OBJECT] : null;

            if (assesment != null)
            {
                if (assesment.GetInfo().Status == AssesmentStatus.Done)
                {
                    return View("AlreadyDone");
                }
                else
                {
                    if (assesment.GetInfo().Status == AssesmentStatus.Started)
                    {
                        return View("Execution");
                    }
                }                
            }

            return View();
        }
    }
}
