using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model.SectionData
{
    public class InternalSectionData : SectionDataProvider
    {
        public List<QuestionTO> GetQuestionsByID(int id)
        {
            List<QuestionTO> list = QuestionPersistence.GetBySectionID(id);

            return list;
        }
    }
}
