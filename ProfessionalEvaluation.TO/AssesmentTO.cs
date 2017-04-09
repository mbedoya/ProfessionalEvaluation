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
    }
}
