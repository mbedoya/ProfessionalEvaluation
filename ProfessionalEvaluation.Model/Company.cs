using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model
{
    public class Company
    {
        private CompanyTO info;

        public Company()
        {
            info = null;
        }

        public CompanyTO Get(int id){
            info = CompanyPersistence.GetById(id);
            return info;
        }

    }
}
