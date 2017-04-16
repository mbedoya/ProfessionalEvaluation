using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfessionalEvaluation.Utilities;
using ProfessionalEvaluation.TO.AssesmentResults;
using ProfessionalEvaluation.TO;
using ProfessionalEvaluation.TO.AssesmentAnalysis;

namespace ProfessionalEvaluationUnitTest.Tests
{
    [TestClass]
    public class PdfTest
    {
        [TestMethod]
        public void GenerateSimplePdf_PdfIsCreated_NoExceptionsThrown()
        {
            AssesmentReportTO report = new AssesmentReportTO();
            report.Sections = GetReportSections();
            report.AssesmentInfo = GetAssesmentInfo();
            report.Analysis = GetAnalysis();

            Pdf.GenerateSimplePdf(report);
            Assert.IsTrue(true);
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
