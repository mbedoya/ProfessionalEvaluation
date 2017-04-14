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
    public class SectionTest
    {
        private const int DEFAULT_ID = 1;

        [TestMethod]
        public void GetQuestionsByID_SectionHasQuestions_ReturnedListHasElements()
        {
            Section s = new Section();
            SectionTO to = new SectionTO();
            to.ID = DEFAULT_ID;
            to.Type = "Internal";

            List<QuestionTO> list = s.GetQuestionsBySection(to);
            Assert.AreEqual(true, list.Count > 0);
        }

        [TestMethod]
        public void GetQuestionsByID_SectionHasQuestionsWithValidData_ReturnedElementHasID()
        {
            Section s = new Section();
            SectionTO to = new SectionTO();
            to.ID = DEFAULT_ID;
            to.Type = "Internal";

            List<QuestionTO> list = s.GetQuestionsBySection(to);
            Assert.AreEqual(true, list[0].ID > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetQuestionsByID_SectionTypeIsNotValid_ExceptionThrown()
        {
            Section s = new Section();
            SectionTO to = new SectionTO();
            to.ID = DEFAULT_ID;
            to.Type = "xxx";

            List<QuestionTO> list = s.GetQuestionsBySection(to);
            Assert.AreEqual(true, list[0].ID > 0);
        }

        [TestMethod]
        public void GetSectionResponses_SectionsHasResponses_ReturnHasRows()
        {
            Section section = new Section();
            SectionTO to = new SectionTO();
            to.ID = DEFAULT_ID;
            to.Type = "Internal";

            List<QuestionResponseTO> responses = section.GetResponsesBySection(to);
            Assert.IsTrue(responses.Count > 0);
        }

        [TestMethod]
        public void GetSectionResponses_SectionsHasResponses_ReturnQuestionID()
        {
            Section section = new Section();
            SectionTO to = new SectionTO();
            to.ID = DEFAULT_ID;
            to.Type = "Internal";

            List<QuestionResponseTO> responses = section.GetResponsesBySection(to);
            Assert.IsTrue(responses[0].QuestionID > 0);
        }

        [TestMethod]
        public void GetSectionResponses_SectionsHasResponses_ReturnAnswerID()
        {
            Section section = new Section();
            SectionTO to = new SectionTO();
            to.ID = DEFAULT_ID;
            to.Type = "Internal";

            List<QuestionResponseTO> responses = section.GetResponsesBySection(to);
            Assert.IsTrue(responses[0].AnswerID > 0);
        }
    }
}
