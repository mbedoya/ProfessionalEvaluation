using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfessionalEvaluation.Utilities;
using ProfessionalEvaluation.TO.AssesmentResults;
using ProfessionalEvaluation.TO;

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

            Pdf.GenerateSimplePdf(report);
            Assert.IsTrue(true);
        }

        private List<SectionReportTO> GetReportSections()
        {
            List<SectionReportTO> list = new List<SectionReportTO>();
            list.Add(new SectionReportTO() { Name = "Capacidad de análisis", Percentage = 50 });
            list.Add(new SectionReportTO() { Name = "Lógica de Programación", Percentage = 75 });

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
    }
}
