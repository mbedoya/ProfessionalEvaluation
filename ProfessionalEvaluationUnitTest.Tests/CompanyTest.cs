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
    public class CompanyTest
    {
        private int DEFAULT_ID = 1;

        [TestMethod]
        public void GetById_DefaultCompanyExists_CompanyIsNotNull()
        {
            Company company = new Company();
            CompanyTO info = company.Get(DEFAULT_ID);
            Assert.IsNotNull(info);
        }

        [TestMethod]
        public void GetById_DefaultCompanyHasName_NameHasValue()
        {
            Company company = new Company();
            CompanyTO info = company.Get(DEFAULT_ID);
            Assert.AreEqual(true, !String.IsNullOrEmpty(info.Name));
        }

        [TestMethod]
        public void GetById_DefaultCompanyHasEmail_EmailHasValue()
        {
            Company company = new Company();
            CompanyTO info = company.Get(DEFAULT_ID);
            Assert.AreEqual(true, !String.IsNullOrEmpty(info.ContactEmail));
        }

        [TestMethod]
        public void GetById_DefaultCompanyHasID_IdSentEqualToReceived()
        {
            Company company = new Company();
            CompanyTO info = company.Get(DEFAULT_ID);
            Assert.AreEqual(DEFAULT_ID, info.ID);
        }

        [TestMethod]
        public void GetById_CompanyDoesNotExist_CompanyIsNull()
        {
            Company company = new Company();
            CompanyTO info = company.Get(0);
            Assert.IsNull(info);
        }
    }
}
