﻿using ProfessionalEvaluation.Persistence;
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

        private List<SectionTO> sections;

        public Evaluation(string industry, string role)
        {
            this.industry = industry;
            this.role = role;

            //sections = FindSections();
        }

        public Evaluation(int id)
        {
            Section section = new Section();
            sections = section.GetByEvaluationID(id);
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
