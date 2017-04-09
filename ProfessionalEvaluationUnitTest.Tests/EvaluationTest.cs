using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ProfessionalEvaluation.Model;

namespace ProfessionalEvaluationUnitTest.Tests
{
    [TestClass]
    public class EvaluationTest
    {
        [TestMethod]
        public void GetAll_EvaluationsCreated_AtLeastOneReturned()
        {
            int result = Evaluation.GetAll().Count;
            Assert.AreEqual(true, result > 0);
        }

        [TestMethod]
        public void GetSections_EvaluationByIndustryAndRole_AtLeastOneSectionsReturned()
        {
            Evaluation eval = new Evaluation("Software", "Developer");
            int result = eval.GetSections().Count;
            Assert.AreEqual(true, result > 0);
        }
    }
}
