using ProfessionalEvaluation.TO.AssesmentAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.TO.AssesmentResults
{
    public class AssesmentReportTO
    {
        public AssesmentTO AssesmentInfo { get; set; }
        public List<SectionReportTO> Sections { get; set; }
        public AssesmentAnalysisReportTO Analysis { get; set; }
    }
}
