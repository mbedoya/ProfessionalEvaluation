using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ProfessionalEvaluation.Model;
using ProfessionalEvaluation.TO;

namespace ProfessionalEvaluationUnitTest.Tests
{
    [TestClass]
    public class AssesmentTest
    {
        private const string DEFAULT_ASSESMENT_ID = "aaaBBBccc";
        private const int DEFAULT_ID = 1;

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_ElementIsNotNull()
        {
            Assesment a = new Assesment();
            AssesmentTO to = a.GetByAssesmentID(DEFAULT_ASSESMENT_ID);
            Assert.AreEqual(true, to != null);
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentDoesNotExist_ElementIsNull()
        {
            Assesment a = new Assesment();
            AssesmentTO to = a.GetByAssesmentID("xxx");
            Assert.AreEqual(true, to == null);
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_DefaultIdIsRight()
        {
            Assesment a = new Assesment();
            AssesmentTO to = a.GetByAssesmentID(DEFAULT_ASSESMENT_ID);
            Assert.AreEqual(true, to.ID == 1);
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_CompanyHasName()
        {
            Assesment a = new Assesment();
            AssesmentTO to = a.GetByAssesmentID(DEFAULT_ASSESMENT_ID);
            Assert.AreEqual(true, !String.IsNullOrEmpty(to.Company.Name));
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_EvaluationHasName()
        {
            Assesment a = new Assesment();
            AssesmentTO to = a.GetByAssesmentID(DEFAULT_ASSESMENT_ID);
            Assert.AreEqual(true, !String.IsNullOrEmpty(to.Evaluation.Name));
        }

    }
}
