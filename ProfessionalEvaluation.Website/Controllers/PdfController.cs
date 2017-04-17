using ProfessionalEvaluation.TO;
using ProfessionalEvaluation.TO.AssesmentAnalysis;
using ProfessionalEvaluation.TO.AssesmentResults;
using ProfessionalEvaluation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessionalEvaluation.Website.Controllers
{
    public class PdfController : Controller
    {
        //
        // GET: /Pdf/

        public ActionResult Index()
        {
            MesssageTO message = new MesssageTO();
            message.Text = "Successful";
            try
            {
                AssesmentReportTO report = new AssesmentReportTO();
                report.Sections = GetReportSections();
                report.AssesmentInfo = GetAssesmentInfo();
                report.Analysis = GetAnalysis();

                Pdf.GenerateSimplePdf(report);
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
            return View(message);
        }

        private List<SectionReportTO> GetReportSections()
        {
            List<SectionReportTO> list = new List<SectionReportTO>();
            list.Add(new SectionReportTO() { Name = "Capacidad de análisis", Percentage = 50 });
            list.Add(new SectionReportTO() { Name = "Lógica de Programación", Percentage = 75 });
            list.Add(new SectionReportTO() { Name = "Ejecución de pruebas", Percentage = 100 });
            list.Add(new SectionReportTO() { Name = "Inglés", Percentage = 30 });

            return list;
        }

        private AssesmentTO GetAssesmentInfo()
        {
            AssesmentTO info = new AssesmentTO();
            info.Company = new CompanyTO();
            info.Company.Name = "Perceptio";
            info.Company.Logo = "logo-perceptio.png";

            info.Evaluation = new EvaluationTO();
            info.Evaluation.Name = "EVALUACIÓN INTEGRAL DESARROLLADOR DE SOFTWARE";

            info.PersonName = "Mauricio Bedoya";
            info.DateFinished = DateTime.Now;
            info.ID = 1;

            return info;
        }

        private AssesmentAnalysisReportTO GetAnalysis()
        {
            AssesmentAnalysisReportTO analysis = new AssesmentAnalysisReportTO();
            analysis.RoleResult = new AssesmentRoleResultTO() { Title = "DESARROLLADOR JUNIOR", Points = 100, PossiblePoints = 200 };

            List<AssesmentRoleLevelTO> levels = new List<AssesmentRoleLevelTO>();
            levels.Add(new AssesmentRoleLevelTO() { Name = "JUNIOR", Description = "No debería ejecutar proyectos sin ayuda de expertos, no cuenta con el conocimiento ni experiencia necesarios para implementar soluciones complejas." });
            levels.Add(new AssesmentRoleLevelTO() { Name = "MIDDLE", Description = "Puede ejecutar proyectos sin ayuda de expertos" });
            levels.Add(new AssesmentRoleLevelTO() { Name = "SENIOR", Description = "Puede ejecutar proyectos solo, cuenta con el conocimiento suficiente." });
            analysis.RoleLevels = levels;

            return analysis;
        }

    }
}
