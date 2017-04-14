using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model.SectionData
{
    public interface SectionDataProvider
    {
        List<QuestionTO> GetQuestionsByID(int id);
        List<QuestionResponseTO> GetQuestionsResponsesByID(int id);
    }
}
