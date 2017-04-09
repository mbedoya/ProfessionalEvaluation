using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Model
{
    public class Industry
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Industry(string name)
        {
            this.name = name;
        }

        public static List<Industry> GetAll()
        {
            List<Industry> list = new List<Industry>();
            list.Add(new Industry("Software"));
            return list;
        }
    }
}
