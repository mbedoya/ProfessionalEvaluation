using ProfessionalEvaluation.TO.AssesmentAnalysis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public class AnalysisPersistence : Persistence
    {
        public static AssesmentAnalysisReportTO GetAssesmentAnalysis(int assesmentID)
        {
            AssesmentAnalysisReportTO analysis = new AssesmentAnalysisReportTO();
            analysis.RoleResult = GetRoleResult(assesmentID);
            analysis.RoleLevels = GetRoleLevels(assesmentID).OrderBy(x => x.Points).ToList();

            return analysis;
        }

        private static AssesmentRoleResultTO GetRoleResult(int assesmentID)
        {
            AssesmentRoleResultTO result = new AssesmentRoleResultTO();
            result.Title = GetAssesmentRoleName(assesmentID);
            result.Points = GetAssesmentPoints(assesmentID);
            result.PossiblePoints = GetAssesmentPossiblePoints(assesmentID);

            return result;
        }

        public static String GetAssesmentRoleName(int assesmentID)
        {
            string name = "";

            //Points
            string execCommand = string.Format("SELECT r.Name " +
                " FROM assesment a " +
                " JOIN evaluation e on e.id = a.EvaluationID " +
                " JOIN role r on  r.id= e.RoleID " +
                " WHERE a.ID = {0} ", assesmentID);
            DataTable table = ExecuteCommand(execCommand);
            if (table != null && table.Rows.Count > 0)
            {
                name = Convert.ToString(table.Rows[0]["Name"]);
            }

            return name;
        }

        public static int GetAssesmentPoints(int assesmentID)
        {
            int points = 0;

            //Points
            string execCommand = string.Format("SELECT IFNULL(SUM(Points), 0)  as Points " +
                " FROM question q " +
                " JOIN assesment_response ar on q.id = ar.questionid " +
                " JOIN section s on  s.id=q.SectionID " +
                " WHERE ar.assesmentid = {0} and ar.answerisright = 1 ", assesmentID);
            DataTable table = ExecuteCommand(execCommand);
            if (table != null && table.Rows.Count > 0)
            {
                points = Convert.ToInt32(table.Rows[0]["Points"]);
            }

            return points;
        }

        public static List<AssesmentCandidateTO> GetAssesmentCandidates(int assesmentID)
        {
            List<AssesmentCandidateTO> candidates = new List<AssesmentCandidateTO>();

            string execCommand =
                string.Format("SELECT a.ID, a.PersonName, IFNULL(SUM(Points), 0)  as Points " +
                " FROM question q " +
                " JOIN assesment_response ar on q.id = ar.questionid " +
                " JOIN assesment a on ar.AssesmentID = a.ID " +
                " WHERE ar.answerisright = 1 AND a.EvaluationID = (SELECT EvaluationID FROM assesment WHERE ID = {0})  " +
                " GROUP BY a.ID, a.PersonName ", assesmentID);

            DataTable levelsTable = ExecuteCommand(execCommand);
            
            if (levelsTable != null && levelsTable.Rows.Count > 0)
            {
                foreach (DataRow item in levelsTable.Rows)
                {
                    candidates.Add(new AssesmentCandidateTO()
                    {
                        AssesmentID = Convert.ToInt32(item["ID"]),
                        Name = Convert.ToString(item["PersonName"]),
                        Points = Convert.ToInt32(item["Points"])
                    });
                }
            }

            return candidates;
        }

        public static int GetAssesmentPossiblePoints(int assesmentID)
        {
            int points = 0;

            //Points
            string execCommand = string.Format("SELECT IFNULL(SUM(Points), 0)  as Points " +
                " FROM question q " +
                " JOIN section s on  s.id=q.SectionID " +
                " JOIN assesment a ON a.EvaluationID = s.EvaluationID " +
                " WHERE a.ID = {0}", assesmentID);
            DataTable table = ExecuteCommand(execCommand);
            if (table != null && table.Rows.Count > 0)
            {
                points = Convert.ToInt32(table.Rows[0]["Points"]);
            }

            return points;
        }

        private static List<AssesmentRoleLevelTO> GetRoleLevels(int assesmentID)
        {
            List<AssesmentRoleLevelTO> levels = new List<AssesmentRoleLevelTO>();

            string roleLevelsCommando =
                String.Format("SELECT Title, Points, Description " +
                " FROM role_analysis " +
                " WHERE RoleID = (SELECT RoleID from evaluation e JOIN assesment a on e.ID = a.EvaluationID WHERE a.ID = {0})", assesmentID);
            DataTable levelsTable = ExecuteCommand(roleLevelsCommando);

            
            if (levelsTable != null && levelsTable.Rows.Count > 0)
            {
                foreach (DataRow item in levelsTable.Rows)
                {
                    levels.Add(new AssesmentRoleLevelTO()
                    {
                        Name = Convert.ToString(item["Title"]),
                        Description = Convert.ToString(item["Description"]),
                        Points = Convert.ToInt32(item["Points"])
                    });
                }
            }

            return levels;
        }
    }
}
