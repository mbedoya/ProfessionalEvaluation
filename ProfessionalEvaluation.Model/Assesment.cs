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
            if (info != null)
            {
                InitAdditionalData();                
            }
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
            AssesmentPersistence.UpdateContext(id, GetDefaultContext());
        }

        public AssesmentAnswerQuestionResult AnswerQuestion(int responseIndex)
        {
            if (responseIndex <= 0)
            {
                return AssesmentAnswerQuestionResult.InvalidResponse;
            }

            if (SectionHasNextQuestion())
            {
                currentContext.QuestionIndex++;
            }
            else
            {
                if (EvaluationHasNextSection())
                {
                    currentContext.SectionIndex++;
                    currentContext.QuestionIndex = 1;
                }
            }
            AssesmentPersistence.UpdateContext(id, currentContext);

            return AssesmentAnswerQuestionResult.Successful;
        }

        private bool SectionHasNextQuestion()
        {
            return currentContext.CurrentSectionQuestions.Count > currentContext.QuestionIndex;
        }

        public bool EvaluationHasNextSection()
        {
            return info.Evaluation.Sections.Count > currentContext.SectionIndex;
        }

        public void UpdateLeftTime()
        {
            currentContext.MinutesLeft = currentContext.MinutesLeft - 1;
            AssesmentPersistence.UpdateContext(id, currentContext);
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
            info.AlreadyStarted = true;
            AssesmentPersistence.SetStartDate(id);
            CreateContext();

            return AssesmentStartOperationState.Successful;
        }

        private void CreateContext()
        {
            if (info != null )
            {
                if (!info.AlreadyStarted)
                {
                    currentContext = GetDefaultContext();
                    currentContext.MinutesLeft = ConvertEstimatedDurationToMinutesLeft(info.Evaluation.Sections[0].EstimatedDuration);

                    AssesmentPersistence.CreateContext(id, currentContext);
                }
                else
                {
                    currentContext = AssesmentPersistence.GetContextByAssesmentID(id);
                }
            }            

            Section section = new Section();
            currentContext.CurrentSectionQuestions = section.GetQuestionsBySection(info.Evaluation.Sections[currentContext.SectionIndex - 1]);
        }

        private AssesmentContextTO GetDefaultContext()
        {
            return new AssesmentContextTO() { QuestionIndex = 1, SectionIndex = 1, MinutesLeft = 60 };
        }

        private int ConvertEstimatedDurationToMinutesLeft(double estimatedDuration)
        {
            return Convert.ToInt32(estimatedDuration * 60);
        }
    }
}
