using ProfessionalEvaluation.TO;
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
                "SELECT a.ID, a.AssesmentID, a.DateStarted, c.Name, c.LogoPath, e.ID as EvalID, e.Name as EvalName, e.Description as EvalDescription " +
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
            string execCommand = String.Format("UPDATE assesment SET DateStarted = NULL WHERE ID = {0}", id);
            ExecuteCommand(execCommand);
        }

        public static void SetStartDate(int id)
        {
            string execCommand = String.Format("UPDATE assesment SET DateStarted = CURRENT_TIMESTAMP WHERE ID = {0}", id);
            ExecuteCommand(execCommand);
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

        private static AssesmentTO MapElement(DataTable table)
        {
            AssesmentTO element = null;

            element = new AssesmentTO();
            element.ID = Convert.ToInt32(table.Rows[0]["ID"]);
            element.AlreadyStarted = false;
            if (!(table.Rows[0]["DateStarted"] is DBNull))
            {
                element.DateStarted = Convert.ToDateTime(table.Rows[0]["DateStarted"]);
                element.AlreadyStarted = true;
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
