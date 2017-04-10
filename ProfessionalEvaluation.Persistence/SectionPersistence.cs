using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public class SectionPersistence : Persistence
    {
        public static List<SectionTO> GetByEvaluationID(int id)
        {
            List<SectionTO> list = new List<SectionTO>();

            DataTable table = ExecuteCommand(
                String.Format(
                " SELECT ID, Name, Description, EstimatedDuration, Type " + 
                " FROM section WHERE EvaluationID = {0} ", id));
            foreach (DataRow item in table.Rows)
            {
                SectionTO element = MapElement(item);
                list.Add(element);
            }

            return list;
        }

        private static SectionTO MapElement(DataRow item)
        {
            return new SectionTO() { ID = Convert.ToInt32(item["ID"]),
                Name = item["Name"].ToString(), 
                Description = item["Description"].ToString(),
                                     Type = item["Type"].ToString(),
                                     EstimatedDuration = Convert.ToDouble(item["EstimatedDuration"])
            };
        }
    }
}
