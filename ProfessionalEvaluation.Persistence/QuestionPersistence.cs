using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public class QuestionPersistence : Persistence
    {
        public static List<QuestionTO> GetBySectionID(int id)
        {
            List<QuestionTO> list = new List<QuestionTO>();

            DataTable table = ExecuteCommand(
                String.Format(
                " SELECT ID, Text, Type " +
                " FROM question WHERE SectionID = {0} ", id));
            foreach (DataRow item in table.Rows)
            {
                QuestionTO element = MapElement(item);
                list.Add(element);
            }

            return list;
        }

        private static QuestionTO MapElement(DataRow item)
        {
            return new QuestionTO()
            {
                ID = Convert.ToInt32(item["ID"]),
                Text = item["Text"].ToString(),
                Type = item["Type"].ToString()
            };
        }
    }
}
