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

        public static List<QuestionResponseTO> GetQuestionsResponsesBySectionID(int id)
        {
            List<QuestionResponseTO> list = new List<QuestionResponseTO>();

            DataTable table = ExecuteCommand(
                String.Format(
                " SELECT ID, RightAnswerID " +
                " FROM question WHERE SectionID = {0} ", id));
            foreach (DataRow item in table.Rows)
            {
                QuestionResponseTO element = new QuestionResponseTO()
                {
                    QuestionID = Convert.ToInt32(item["ID"]),
                    AnswerID = Convert.ToInt32(item["RightAnswerID"])
                };
                list.Add(element);
            }

            return list;
        }

        private static QuestionTO MapElement(DataRow item)
        {
            QuestionTO question = new QuestionTO()
            {
                ID = Convert.ToInt32(item["ID"]),
                Text = item["Text"].ToString(),
                Type = item["Type"].ToString()
            };
            question.Answers = GetAnswers(question.ID);

            return question;
        }

        private static List<AnswerTO> GetAnswers(int id)
        {
            List<AnswerTO> list = new List<AnswerTO>();

            DataTable table = ExecuteCommand(
                String.Format(
                " SELECT ID, Text " +
                " FROM answer WHERE QuestionID = {0} ", id));
            foreach (DataRow item in table.Rows)
            {
                AnswerTO element = new AnswerTO()
                {
                    ID = Convert.ToInt32(item["ID"]),
                    Text = Convert.ToString(item["Text"])
                };
                list.Add(element);
            }

            return list;
        }
    }
}
