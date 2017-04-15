using ProfessionalEvaluation.TO;
using ProfessionalEvaluation.TO.AssesmentResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public class AssesmentPersistence : Persistence
    {
        private static string command = 
                "SELECT a.ID, a.AssesmentID, a.DateStarted, a.DateFinished, a.PersonName ,c.Name, c.LogoPath, e.ID as EvalID, e.Name as EvalName, e.Description as EvalDescription " +
                " FROM assesment a " +
                " JOIN company c ON a.CompanyID = c.ID" +
                " JOIN evaluation e ON a.EvaluationID = e.ID" +
                " WHERE a.{0} = '{1}'";

        public static AssesmentTO GetByAssesmentID(string assesmentID)
        {
            AssesmentTO element = null;
            string execCommand = String.Format(command, "AssesmentID", assesmentID);

            DataTable table = ExecuteCommand(execCommand);
            if (table != null && table.Rows.Count > 0)
            {
                element = MapElement(table);
            }

            return element;
        }

        public static void Restart(int id)
        {
            string execCommand = String.Format("UPDATE assesment SET DateStarted = NULL,DateFinished=NULL WHERE ID = {0}", id);
            ExecuteCommand(execCommand);
        }

        public static void SetStartDate(int id)
        {
            string execCommand = String.Format("UPDATE assesment SET DateStarted = CURRENT_TIMESTAMP WHERE ID = {0}", id);
            ExecuteCommand(execCommand);
        }

        public static void SetFinishedDate(int id)
        {
            string execCommand = String.Format("UPDATE assesment SET DateFinished = CURRENT_TIMESTAMP WHERE ID = {0}", id);
            ExecuteCommand(execCommand);
        }

        public static List<SectionPointsTO> GetAssesmentPoints(int assesmentID){

            List<SectionPointsTO> list = new List<SectionPointsTO>();

            string execCommand = string.Format("SELECT s.ID, SUM(Points) as Points " +
                " FROM question q " +
                " JOIN assesment_response ar on q.id = ar.questionid " +
                " JOIN section s on  s.id=q.SectionID " +
                " WHERE ar.assesmentid = {0} and ar.answerisright = 1 " +
                " GROUP by s.ID", assesmentID);

            DataTable table = ExecuteCommand(execCommand);

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
	            {
                    SectionPointsTO points = new SectionPointsTO();
                    points.SectionID = Convert.ToInt32(item["ID"]);
                    points.Points = Convert.ToInt32(item["Points"]);

                    list.Add(points);
	            }                
            }

            return list;
        }

        

        public static AssesmentTO GetByID(int id)
        {
            AssesmentTO element = null;
            string execCommand = String.Format(command, "ID", id);

            DataTable table = ExecuteCommand(execCommand);
            if (table != null && table.Rows.Count > 0)
            {
                element = MapElement(table);
            }

            return element;
        }

        public static void CreateContext(int assesmentID, AssesmentContextTO context)
        {
            string execCommand = String.Format("INSERT INTO assesment_context (AssesmentID, SectionIndex, QuestionIndex, MinutesLeft) " +
                " VALUES ({0},{1},{2},{3})", assesmentID, context.SectionIndex, context.QuestionIndex, context.MinutesLeft);
            ExecuteAssementExecutionCommand(execCommand);
        }

        public static void UpdateContext(int assesmentID, AssesmentContextTO context)
        {
            string execCommand = String.Format("UPDATE assesment_context " + 
                " SET SectionIndex={1}, QuestionIndex={2}, MinutesLeft={3} " +
                " WHERE AssesmentID = {0}", assesmentID, context.SectionIndex, context.QuestionIndex, context.MinutesLeft);
            ExecuteAssementExecutionCommand(execCommand);
        }

        public static void SaveResponse(int assesmentID, int questionID, int answerID, bool answerIsRight)
        {
            string execCommand = String.Format("INSERT INTO assesment_response (AssesmentID, QuestionID, AnswerID, AnswerIsRight) " +
                " VALUES ({0},{1},{2},{3})", assesmentID, questionID, answerID, answerIsRight);
            ExecuteCommand(execCommand);
        }

        public static AssesmentContextTO GetContextByAssesmentID(int id)
        {
            AssesmentContextTO currentContext = null;

            string execCommand = String.Format("SELECT SectionIndex, QuestionIndex, MinutesLeft " +
                " FROM assesment_context " +
                " WHERE AssesmentID = {0}", id);

            DataTable table = ExecuteAssementExecutionCommand(execCommand);

            if (table != null && table.Rows.Count > 0)
            {
                currentContext = new AssesmentContextTO();
                currentContext.SectionIndex = Convert.ToInt32(table.Rows[0]["SectionIndex"]);
                currentContext.QuestionIndex = Convert.ToInt32(table.Rows[0]["QuestionIndex"]);
                currentContext.MinutesLeft = Convert.ToInt32(table.Rows[0]["MinutesLeft"]);
            }

            return currentContext;
        }

        private static AssesmentTO MapElement(DataTable table)
        {
            AssesmentTO element = null;

            element = new AssesmentTO();
            element.ID = Convert.ToInt32(table.Rows[0]["ID"]);
            element.Status = AssesmentStatus.Created;
            element.PersonName = Convert.ToString(table.Rows[0]["PersonName"]);
            if (!(table.Rows[0]["DateStarted"] is DBNull))
            {
                element.DateStarted = Convert.ToDateTime(table.Rows[0]["DateStarted"]);
                element.Status = AssesmentStatus.Started;
            }

            if (!(table.Rows[0]["DateFinished"] is DBNull))
            {
                element.DateFinished = Convert.ToDateTime(table.Rows[0]["DateFinished"]);
                element.Status = AssesmentStatus.Done;
            }

            element.Company = new CompanyTO();
            element.Company.Name = Convert.ToString(table.Rows[0]["Name"]);
            element.Company.Logo = Convert.ToString(table.Rows[0]["LogoPath"]);

            element.Evaluation = new EvaluationTO();
            element.Evaluation.ID = Convert.ToInt32(table.Rows[0]["EvalID"]);
            element.Evaluation.Name = Convert.ToString(table.Rows[0]["EvalName"]);
            element.Evaluation.Description = Convert.ToString(table.Rows[0]["EvalDescription"]);

            return element;
        }
    }
}
