using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model.SectionData
{
    public class SectionDataFactory
    {
        public static SectionDataProvider CreateProvider(string type)
        {
            if (type.ToLower() == "internal")
            {
                return new InternalSectionData();
            }

            throw new Exception("Section Type '" + type + "' unexpected");
        }
    }
}
