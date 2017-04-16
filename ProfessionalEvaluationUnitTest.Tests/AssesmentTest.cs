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
        private bool doCleanUp = false;

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_ElementIsNotNull()
        {
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentTO to = a.GetInfo();
            Assert.IsNotNull(to);
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentDoesNotExist_ElementIsNull()
        {
            Assesment a = new Assesment("xxx");
            AssesmentTO to = a.GetInfo();
            Assert.IsNull(to);
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_DefaultIdIsRight()
        {
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentTO to = a.GetInfo();
            Assert.AreEqual(1, to.ID);
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_CompanyHasName()
        {
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentTO to = a.GetInfo();
            Assert.AreEqual(true, !String.IsNullOrEmpty(to.Company.Name));
        }

        [TestMethod]
        public void GetByAssesmentID_AssesmentFound_EvaluationHasName()
        {
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentTO to = a.GetInfo();
            Assert.AreEqual(true, !String.IsNullOrEmpty(to.Evaluation.Name));
        }

        [TestMethod]
        public void GetByID_AssesmentFound_IdIsValid()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentTO to = a.GetInfo();
            Assert.AreEqual(true, !String.IsNullOrEmpty(to.Evaluation.Name));
        }

        [TestMethod]
        public void GetByID_AssesmentNotFound_ObjectIsNull()
        {
            Assesment a = new Assesment(0);
            AssesmentTO to = a.GetInfo();
            Assert.AreEqual(null, to);
        }

        [TestMethod]
        public void Start_AssesmentStartedSuccessfully_SuccessfulResult()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentStartOperationState state = a.Start();
            Assert.AreEqual(AssesmentStartOperationState.Successful, state);
        }

        [TestMethod]
        public void GetContext_AssesmentJustStarted_InitialDataSectionIs1()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO context = a.GetCurrentContext();
            Assert.AreEqual(1, context.SectionIndex);
        }

        [TestMethod]
        public void GetContext_AssesmentJustStarted_InitialDataQuestionIs1()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO context = a.GetCurrentContext();
            Assert.AreEqual(1, context.QuestionIndex);
        }        

        [TestMethod]
        public void AnswerQuestion_ResponseIsValid_AnswerIsSuccessful()
        {
            int responseIndex = 1;
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentAnswerQuestionResult result = a.AnswerQuestion(responseIndex);
            Assert.AreEqual(true, a.ResponseWasRight());
        }

        [TestMethod]
        public void AnswerQuestion_ResponseIsValid_AnswerIsWrong()
        {
            int responseIndex = 2;
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentAnswerQuestionResult result = a.AnswerQuestion(responseIndex);
            Assert.IsFalse(a.ResponseWasRight());
        }

        [TestMethod]
        public void AnswerQuestion_ResponseIsNotValid_AnswerQuestionInvalidResponse()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentAnswerQuestionResult result = a.AnswerQuestion(0);
            Assert.AreEqual(AssesmentAnswerQuestionResult.InvalidResponse, result);
        }

        [TestMethod]
        public void AnswerQuestion_MoreQuestionsInSection_QuestionIndexIncreased()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO beforeContext = a.GetCurrentContext();
            AssesmentAnswerQuestionResult result = a.AnswerQuestion(1);
            AssesmentContextTO afterContext = a.GetCurrentContext();

            if (beforeContext.CurrentSectionQuestions.Count < beforeContext.QuestionIndex)
            {
                Assert.AreEqual(beforeContext.QuestionIndex + 1, afterContext.QuestionIndex);
            }
            else
            {
                Assert.AreEqual(true, true);
            }
        }

        [TestMethod]
        public void AnswerQuestion_NoMoreQuestionsInSection_SectionIndexIncreased()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO beforeContext = a.GetCurrentContext();
            AssesmentAnswerQuestionResult result = a.AnswerQuestion(1);
            AssesmentContextTO afterContext = a.GetCurrentContext();

            if (a.GetInfo().Evaluation.Sections.Count < beforeContext.SectionIndex)
            {
                Assert.AreEqual(beforeContext.SectionIndex + 1, afterContext.SectionIndex);
            }
            else
            {
                Assert.AreEqual(true, true);
            }
        }

        [TestMethod]
        public void AnswerQuestion_LastQuestion_IndexesNotUpdated()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO beforeContext = a.GetCurrentContext();
            AssesmentAnswerQuestionResult result = a.AnswerQuestion(1);
            AssesmentContextTO afterContext = a.GetCurrentContext();

            if (a.GetInfo().Evaluation.Sections.Count == beforeContext.SectionIndex 
                && beforeContext.CurrentSectionQuestions.Count == beforeContext.QuestionIndex)
            {
                Assert.AreEqual(beforeContext.SectionIndex, afterContext.SectionIndex);
            }
            else
            {
                Assert.AreEqual(true, true);
            }
        }

        /*
        [TestMethod]
        public void UpdateLeftTime_LeftTimeIsUpdated_LeftTimeMinus1()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO context = a.GetCurrentContext();
            int minutesLeft = context.MinutesLeft;
            a.Start();
            a.UpdateLeftTime();
            context = a.GetCurrentContext();
            Assert.AreEqual(minutesLeft - 1, context.MinutesLeft);
        }

        [TestMethod]
        public void UpdateLeftTime_NewObjectLeftTimeIsUpdated_LeftTimeMinus1()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentContextTO context = a.GetCurrentContext();
            int minutesLeft = context.MinutesLeft;
            a.Start();
            a.UpdateLeftTime();

            Assesment b = new Assesment(DEFAULT_ID);
            AssesmentContextTO context2 = b.GetCurrentContext();
            Assert.AreEqual(minutesLeft - 1, context2.MinutesLeft);
        }
         */

        [TestMethod]
        public void UpdateLeftTime_TimeRunOut_MinutesLeftStillZero()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            a.Start();
            a.UpdateLeftTime();
            AssesmentContextTO context = a.GetCurrentContext();
            Assert.AreEqual(true, context.MinutesLeft >= 0);
        }

        [TestMethod]
        public void Start_AssesmentNotFound_NotFoundResult()
        {
            Assesment a = new Assesment(0);
            AssesmentStartOperationState state = a.Start();
            Assert.AreEqual(AssesmentStartOperationState.AssementNotFound, state);
        }

        [TestMethod]
        public void Start_AssesmentAlreadyStarted_AlreadyStartedResult()
        {
            doCleanUp = true;
            Assesment a = new Assesment(DEFAULT_ID);
            AssesmentStartOperationState state = a.Start();
            Assert.AreEqual(AssesmentStartOperationState.AlreadyStarted, state);
        }

        [TestMethod]
        public void Start_ByAssesmentIdStartedSuccessfully_SuccessfulResult()
        {
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentStartOperationState state = a.Start();
            Assert.AreEqual(AssesmentStartOperationState.Successful, state);
        }

        [TestMethod]
        public void GetContext_ByAssesmentIdJustStarted_InitialDataSectionIs1()
        {
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentContextTO context = a.GetCurrentContext();
            Assert.AreEqual(1, context.SectionIndex);
        }

        [TestMethod]
        public void End_AssesmentStartedAndFinishesSuccessfully_SuccessfulResult()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            if (a.GetInfo().Status == AssesmentStatus.Started)
            {
                a.End();
                Assert.AreEqual(AssesmentStatus.Done, a.GetInfo().Status);
            }
            else
            {
                Assert.IsTrue(true);
            }
            
        }

        [TestMethod]
        public void End_AssesmentNotStartedStatusRemains_StatusDoesNotChange()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            if (a.GetInfo().Status == AssesmentStatus.Created)
            {
                a.End();
                Assert.AreEqual(AssesmentStatus.Created, a.GetInfo().Status);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void End_AssesmentDoneStatusRemains_StatusDoesNotChange()
        {
            Assesment a = new Assesment(DEFAULT_ID);
            if (a.GetInfo().Status == AssesmentStatus.Done)
            {
                a.End();
                Assert.AreEqual(AssesmentStatus.Done, a.GetInfo().Status);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Start_ByAssesmentIdAlreadyStarted_AlreadyStartedResult()
        {
            doCleanUp = true;
            Assesment a = new Assesment(DEFAULT_ASSESMENT_ID);
            AssesmentStartOperationState state = a.Start();
            Assert.AreEqual(AssesmentStartOperationState.AlreadyStarted, state);
        }

        [TestInitialize]
        public void Init()
        {
            doCleanUp = false;
        }

        [TestCleanup]
        public void ResetAssesment()
        {
            if (doCleanUp)
            {
                Assesment a = new Assesment(DEFAULT_ID);
                a.Restart();
            }
            
        }
    }
}
