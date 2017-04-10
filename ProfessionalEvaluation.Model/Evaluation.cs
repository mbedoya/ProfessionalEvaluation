using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model
{
    public class Evaluation
    {
        private string industry;
        private string role;

        private int id;

        private List<SectionTO> sections;

        public Evaluation(string industry, string role)
        {
            this.industry = industry;
            this.role = role;

            //sections = FindSections();
        }

        public Evaluation(int id)
        {
            this.id = id;
            sections = GetSectionsByID();
        }

        public List<SectionTO> GetSectionsByID()
        {
            List<SectionTO> list = SectionPersistence.GetByEvaluationID(id);
            return list;
        }

        public static List<EvaluationTO> GetAll()
        {
            List<EvaluationTO> list = EvaluationPersistence.GetAll();
            return list;
        }

        public List<SectionTO> GetSections()
        {
            return sections;
        }
    }
}
