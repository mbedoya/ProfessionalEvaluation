﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.TO
{
    public class EvaluationTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SectionTO> Sections { get; set; }
    }
}
