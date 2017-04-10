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
        public List<SectionTO> GetByEvaluationID(int id)
        {
            List<SectionTO> list = SectionPersistence.GetByEvaluationID(id);
            return list;
        }
    }
}
