using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.TO
{
    public class AssesmentTO
    {
        public CompanyTO Company { get; set; }
        public EvaluationTO Evaluation { get; set; }
        public int ID { get; set; }
        public DateTime? DateStarted { get; set; }
        public bool AlreadyStarted { get; set; }
    }

    public enum AssesmentStartOperationState
    {
        Successful,
        AssementNotFound,
        AlreadyStarted
    }

    public enum AssesmentAnswerQuestionResult
    {
        Successful,
        InvalidResponse
    }
}
