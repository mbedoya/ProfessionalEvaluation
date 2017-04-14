using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using ProfessionalEvaluation.TO.AssesmentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProfessionalEvaluation.Utilities;

namespace ProfessionalEvaluation.Model
{
    public class Assesment
    {
        private int id;
        private string assesmentID;
        private int currentQuestionAnswerIndex;
        private bool responseWasRight;
        private List<QuestionResponseTO> currentSectionResponses;
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

            if (responseIndex > GetCurrentQuestion().Answers.Count)
            {
                return AssesmentAnswerQuestionResult.InvalidResponse;
            }

            UpdateResponseStatus(responseIndex);
            SaveResponse();
            CheckAndEndAssesment();
            UpdateContext();

            return AssesmentAnswerQuestionResult.Successful;
        }

        private QuestionTO GetCurrentQuestion()
        {
            return currentContext.CurrentSectionQuestions[currentContext.QuestionIndex - 1];
        }

        private void UpdateResponseStatus(int responseIndex)
        {
            responseWasRight = responseIndex == currentQuestionAnswerIndex;
        }

        private void CheckAndEndAssesment()
        {
            if (info.Evaluation.Sections.Count == currentContext.SectionIndex &&
                currentContext.CurrentSectionQuestions.Count == currentContext.QuestionIndex)
            {
                End();
            }
        }

        private void UpdateContext()
        {
            if (SectionHasNextQuestion())
            {
                currentContext.QuestionIndex++;

                UpdateQuestionResponseInfo();
            }
            else
            {
                if (EvaluationHasNextSection())
                {
                    currentContext.SectionIndex++;
                    currentContext.QuestionIndex = 1;

                    UpdateSectionResponseInfo();
                }
            }
            AssesmentPersistence.UpdateContext(id, currentContext);
        }

        private void SaveResponse()
        {
            QuestionTO question = GetCurrentQuestion();
            AssesmentPersistence.SaveResponse(id, question.ID, question.Answers[currentQuestionAnswerIndex - 1].ID, responseWasRight);
        }

        public bool ResponseWasRight()
        {
            return responseWasRight;
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
            if (currentContext.MinutesLeft < 0)
            {
                currentContext.MinutesLeft = 0;
            }
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
            info.Status = AssesmentStatus.Started;
            AssesmentPersistence.SetStartDate(id);
            CreateContext();

            return AssesmentStartOperationState.Successful;
        }

        private void CreateContext()
        {
            if (info != null )
            {
                if (info.Status == AssesmentStatus.Created)
                {
                    currentContext = GetDefaultContext();
                    currentContext.MinutesLeft = ConvertEstimatedDurationToMinutesLeft(info.Evaluation.Sections[0].EstimatedDuration);

                    AssesmentPersistence.CreateContext(id, currentContext);
                }
                else
                {
                    currentContext = AssesmentPersistence.GetContextByAssesmentID(id);
                }

                UpdateSectionResponseInfo();
            }
        }

        public AssesmentEndOperationState End()
        {
            if (info.Status == AssesmentStatus.Created)
            {
                return AssesmentEndOperationState.AssementNotStarted;
            }

            if (info.Status == AssesmentStatus.Done)
            {
                return AssesmentEndOperationState.AlreadyDone;
            }

            info.Status = AssesmentStatus.Done;
            info.DateFinished= DateTime.Now;
            AssesmentPersistence.SetFinishedDate(id);
            GenerateAndSendResults();

            return AssesmentEndOperationState.Successful;
        }

        private void GenerateAndSendResults()
        {
            AssesmentReportTO report = new AssesmentReportTO();
            report.Sections = new List<SectionReportTO>();

            List<SectionPointsTO> list = AssesmentPersistence.GetAssesmentPoints(id);
            for (int i = 0; i < info.Evaluation.Sections.Count; i++)
            {
                SectionReportTO section = new SectionReportTO();
                section.Name = info.Evaluation.Sections[i].Name;
                SectionPointsTO points = list.Find(x => x.SectionID == info.Evaluation.Sections[i].ID);
                if (points != null)
                {
                    section.Percentage = (points.Points/100)*100;
                }

                report.Sections.Add(section);
            }

            Pdf.GenerateSimplePdf(report);
        }

        private void UpdateSectionResponseInfo()
        {
            SetCurrentSectionQuestions();
            UpdateQuestionResponseInfo();
        }

        private void SetCurrentSectionQuestions()
        {
            Section section = new Section();
            currentContext.CurrentSectionQuestions = section.GetQuestionsBySection(info.Evaluation.Sections[currentContext.SectionIndex - 1]);
        }

        private void UpdateQuestionResponseInfo()
        {
            currentSectionResponses = new Section().GetResponsesBySection(info.Evaluation.Sections[currentContext.SectionIndex - 1]);
            int currentQuestionAnswerID = currentSectionResponses.Find( x => x.QuestionID == currentContext.CurrentSectionQuestions[currentContext.QuestionIndex - 1].ID).AnswerID;

            currentQuestionAnswerIndex = 0;
            QuestionTO question = GetCurrentQuestion();

            for (int i = 0; i < question.Answers.Count; i++)
            {
                if (question.Answers[i].ID == currentQuestionAnswerID)
                {
                    currentQuestionAnswerIndex = i+1;
                    break;
                }
            }
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
