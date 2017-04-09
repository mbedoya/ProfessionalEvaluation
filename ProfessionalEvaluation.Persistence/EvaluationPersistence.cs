using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data;

namespace ProfessionalEvaluation.Persistence
{
    public class EvaluationPersistence : Persistence
    {
        public static List<EvaluationTO> GetAll()
        {
            List<EvaluationTO> list = new List<EvaluationTO>();

            DataTable table = ExecuteCommand("SELECT ID, Name, Description FROM Evaluation");
            foreach (DataRow item in table.Rows)
            {
                EvaluationTO element = MapElement(item);
                list.Add(element);
            }

            return list;
        }

        private static EvaluationTO MapElement(DataRow item)
        {
            return new EvaluationTO() { ID = Convert.ToInt32(item["ID"]), Name = item["Name"].ToString(), Description = item["Description"].ToString() };
        }
    }
}
