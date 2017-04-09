﻿using ProfessionalEvaluation.Model;
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

        //
        // GET: /Assesment/

        public ActionResult Index()
        {
            Session[SESSION_ASSESMENT_ID] = Request.QueryString["id"];
            return View();
        }

        public ActionResult GetData()
        {
            AssesmentTO data = null;
            string assesmentID = Session[SESSION_ASSESMENT_ID] != null ? Session[SESSION_ASSESMENT_ID].ToString() : "";

            if (!String.IsNullOrEmpty(assesmentID))
            {
                Assesment assesment = new Assesment();
                data = assesment.GetByAssesmentID(assesmentID);
            }

            if (data == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
