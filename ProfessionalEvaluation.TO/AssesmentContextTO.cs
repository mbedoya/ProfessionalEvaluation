using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.TO
{
    public class AssesmentContextTO
    {
        public int SectionIndex { get; set; }
        public int QuestionIndex { get; set; }
        public int MinutesLeft { get; set; }
        public List<QuestionTO> CurrentSectionQuestions { get; set; }
    }
}
