﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfessionalEvaluation.Persistence
{
    public abstract class Persistence
    {
        protected static DataTable ExecuteCommand(string command)
        {
            System.Data.IDataAdapter adapter = DataAdapterFactory.CreateAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            return dataSet.Tables[0];
        }
    }
}