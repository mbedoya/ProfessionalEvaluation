using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ProfessionalEvaluation.Model;

namespace ProfessionalEvaluationUnitTest.Tests
{
    [TestClass]
    public class IndustryTest
    {
        [TestMethod]
        public void GetAll_IndustryCreated_AtLeastOneReturned()
        {
            int result = Industry.GetAll().Count;
            Assert.AreEqual(true, result > 0);
        }
    }
}
