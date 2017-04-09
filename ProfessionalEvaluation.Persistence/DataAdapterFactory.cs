using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public class DataAdapterFactory
    {
        private static string DB_AppSetting = "Database";

        public static IDataAdapter CreateAdapter(string command)
        {
            return new MySql.Data.MySqlClient.MySqlDataAdapter(command, new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.AppSettings[DB_AppSetting]));
        }
    }
}
