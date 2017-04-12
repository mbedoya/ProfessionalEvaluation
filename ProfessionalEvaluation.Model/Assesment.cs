using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model
{
    public class Assesment
    {
        private int id;
        private string assesmentID;
        private AssesmentContextTO currentContext;
        private AssesmentTO info;

        public Assesment(int id)
        {
            this.id = id;
            info = AssesmentPersistence.GetByID(id);
            InitAdditionalData();
        }

        public Assesment(string assesmentID)
        {
            info = AssesmentPersistence.GetByAssesmentID(assesmentID);
            if (info != null)
            {
                this.assesmentID = assesmentID;
                this.id = info.ID;
                InitAdditionalData();
            }
        }

        private void InitAdditionalData()
        {
            GetSections();
            CreateContext();
        }

        public AssesmentContextTO GetCurrentContext()
        {
            return currentContext;
        }

        public AssesmentTO GetInfo()
        {
            return info;
        }

        private void GetSections()
        {
            if (info != null && info.Evaluation != null)
            {
                info.Evaluation.Sections = new Evaluation(info.Evaluation.ID).GetSections();
            }
        }

        public void Restart()
        {
            AssesmentPersistence.Restart(id);
        }

        public AssesmentAnswerQuestionResult AnswerQuestion(int responseIndex)
        {
            if (responseIndex <= 0)
            {
                return AssesmentAnswerQuestionResult.InvalidResponse;
            }

            return AssesmentAnswerQuestionResult.Successful;
        }

        public void UpdateLeftTime()
        {
            currentContext.MinutesLeft = currentContext.MinutesLeft - 1;
        }

        public AssesmentStartOperationState Start()
        {
            if (info == null)
            {
                return AssesmentStartOperationState.AssementNotFound;
            }

            if(info.DateStarted != null)
            {
                return AssesmentStartOperationState.AlreadyStarted;
            }

            info.DateStarted = DateTime.Now;
            AssesmentPersistence.SetStartDate(id);
            CreateContext();

            return AssesmentStartOperationState.Successful;
        }

        private void CreateContext()
        {
            if (info != null)
            {
                currentContext = new AssesmentContextTO() { SectionIndex = 1, QuestionIndex = 1, MinutesLeft = 60 };

                Section section = new Section();
                currentContext.CurrentSectionQuestions = section.GetQuestionsBySection(info.Evaluation.Sections[currentContext.SectionIndex - 1]);
            }
        }
    }
}
