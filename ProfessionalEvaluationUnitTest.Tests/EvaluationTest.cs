using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ProfessionalEvaluation.Model;
using System.Collections.Generic;
using ProfessionalEvaluation.TO;

namespace ProfessionalEvaluationUnitTest.Tests
{
    [TestClass]
    public class EvaluationTest
    {
        private const int DEFAULT_ID = 1;
        private const string DEFAULT_INDUSTRY = "Software";
        private const string DEFAULT_ROLE = "Developer";

        [TestMethod]
        public void GetAll_EvaluationsCreated_AtLeastOneReturned()
        {
            int result = Evaluation.GetAll().Count;
            Assert.AreEqual(true, result > 0);
        }

        [TestMethod]
        public void GetSections_EvaluationByIndustryAndRole_AtLeastOneSectionsReturned()
        {
            Evaluation eval = new Evaluation(DEFAULT_INDUSTRY, DEFAULT_ROLE);
            int result = eval.GetSections().Count;
            Assert.AreEqual(true, result > 0);
        }

        [TestMethod]
        public void GetSections_EvaluationById_AtLeastOneSectionsReturned()
        {
            Evaluation eval = new Evaluation(DEFAULT_ID);
            int result = eval.GetSections().Count;
            Assert.AreEqual(true, result > 0);
        }

        [TestMethod]
        public void GetSections_EvaluationById_SectionHasId()
        {
            Evaluation eval = new Evaluation(DEFAULT_ID);
            List<SectionTO> list = eval.GetSections();
            Assert.AreEqual(true, list[0].ID > 0);
        }
    }
}
