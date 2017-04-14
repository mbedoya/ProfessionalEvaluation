using ProfessionalEvaluation.Model.SectionData;
using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model
{
    public class Section
    {
        public List<QuestionTO> GetQuestionsBySection(SectionTO section)
        {
            SectionDataProvider provider = SectionDataFactory.CreateProvider(section.Type);
            List<QuestionTO> list = provider.GetQuestionsByID(section.ID);

            return list;
        }

        public List<QuestionResponseTO> GetResponsesBySection(SectionTO section)
        {
            SectionDataProvider provider = SectionDataFactory.CreateProvider(section.Type);
            List<QuestionResponseTO> list = provider.GetQuestionsResponsesByID(section.ID);

            return list;
        }
        
    }
}
