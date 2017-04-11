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
        private const string SESSION_ASSESMENT_ID = "AssesmentID";
        private const string SESSION_ASSESMENT_OBJECT = "Assesment";
        private const string QUERY_STRING_ID = "id";

        //
        // GET: /Assesment/

        public ActionResult Index()
        {
            string assesmentID = Request.QueryString[QUERY_STRING_ID] != null ? Request.QueryString[QUERY_STRING_ID].ToString() : null;

            if (!String.IsNullOrEmpty(assesmentID))
            {
                Session[SESSION_ASSESMENT_ID] = assesmentID;

                Assesment assesment = new Assesment();
                Session[SESSION_ASSESMENT_OBJECT] = assesment.GetByAssesmentID(assesmentID); ;
            }

            return View();
        }

        public ActionResult Instructions()
        {
            return View();
        }

        public ActionResult GetData()
        {
            AssesmentTO data = Session[SESSION_ASSESMENT_OBJECT] != null ? (AssesmentTO)Session[SESSION_ASSESMENT_OBJECT] : null;
            
            if (data == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Execution()
        {
            return View();
        }

    }
}
