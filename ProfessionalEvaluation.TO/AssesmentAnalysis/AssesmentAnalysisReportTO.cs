using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.TO.AssesmentAnalysis
{
    public class AssesmentAnalysisReportTO
    {
        public AssesmentRoleResultTO RoleResult { get; set; }
        public List<AssesmentRoleLevelTO> RoleLevels { get; set; }
        public AssesmentResultComparisonTO Comparisons { get; set; }
        public List<AssesmentCandidateTO> Candidates { get; set; }
    }
}
