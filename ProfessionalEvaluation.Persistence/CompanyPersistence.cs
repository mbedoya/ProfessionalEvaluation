using ProfessionalEvaluation.TO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public class CompanyPersistence : Persistence
    {
        public static CompanyTO GetById(int id)
        {
            CompanyTO company = null;
            string execCommand = String.Format(
                " SELECT ID, Name, LogoPath, ContactName, ContactEmail " +
                " FROM company " + 
                " WHERE ID = {0}", id);

            DataTable table = ExecuteCommand(execCommand);
            if (table != null && table.Rows.Count > 0)
            {
                company = MapElement(table.Rows[0]);
            }

            return company;
        }

        private static CompanyTO MapElement(DataRow row)
        {
            CompanyTO company = new CompanyTO();
            company.ID = Convert.ToInt32(row["ID"]);
            company.Name = Convert.ToString(row["Name"]);
            company.Logo = Convert.ToString(row["LogoPath"]);
            company.ContactName = Convert.ToString(row["ContactName"]);
            company.ContactEmail = Convert.ToString(row["ContactEmail"]);

            return company;
        }
    }
}
