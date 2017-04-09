using ProfessionalEvaluation.Persistence;
using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model
{
    public class Assesment
    {
        public Assesment()
        {

        }

        public AssesmentTO GetByAssesmentID(string assesmentID)
        {
            AssesmentTO assesment = AssesmentPersistence.GetByAssesmentID(assesmentID);
            return assesment;
        }
    }
}
