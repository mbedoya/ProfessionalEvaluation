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
        public static AssesmentTO GetByAssesmentID(string assesmentID)
        {
            AssesmentTO element = null;
            string command = String.Format(
                "SELECT a.ID, a.AssesmentID, c.Name, c.LogoPath, e.ID as EvalID, e.Name as EvalName, e.Description as EvalDescription " +
                " FROM assesment a " + 
                " JOIN company c ON a.CompanyID = c.ID" +
                " JOIN evaluation e ON a.EvaluationID = e.ID" +
                " WHERE a.AssesmentID = '{0}'", assesmentID);

            DataTable table = ExecuteCommand(command);
            if (table != null && table.Rows.Count > 0)
            {
                element = new AssesmentTO();
                element.ID = Convert.ToInt32(table.Rows[0]["ID"]);

                element.Company = new CompanyTO();
                element.Company.Name = Convert.ToString(table.Rows[0]["Name"]);
                element.Company.Logo = Convert.ToString(table.Rows[0]["LogoPath"]);

                element.Evaluation = new EvaluationTO();
                element.Evaluation.ID = Convert.ToInt32(table.Rows[0]["EvalID"]);
                element.Evaluation.Name = Convert.ToString(table.Rows[0]["EvalName"]);
                element.Evaluation.Description = Convert.ToString(table.Rows[0]["EvalDescription"]);
            }

            return element;
        }
    }
}
