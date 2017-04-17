using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.TO
{
    public class MailMessageTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public List<string> Attachements { get; set; }
    }
}
