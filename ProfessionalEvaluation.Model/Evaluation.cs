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

        private List<Section> sections;

        public Evaluation(string industry, string role)
        {
            this.industry = industry;
            this.role = role;

            sections = FindSections();
        }

        public static List<EvaluationTO> GetAll()
        {
            List<EvaluationTO> list = EvaluationPersistence.GetAll();

            return list;
        }

        private List<Section> FindSections()
        {
            List<Section> list;
            list = new List<Section>();
            list.Add(new Section());

            return list;
        }

        public List<Section> GetSections()
        {
            return sections;
        }
    }
}
