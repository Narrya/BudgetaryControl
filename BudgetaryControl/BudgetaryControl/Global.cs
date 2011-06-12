using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace BudgetaryControl
{
    class Global
    {
        public static SqlCeConnection polaczenie;
        public static bool Polacz()
        {
            polaczenie = new SqlCeConnection("Data Source=BUDGETdatabase.sdf");
            try
            {
                polaczenie.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SqlCeResultSet viewdata(string komenda)
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = polaczenie;
            cmd.CommandText = komenda;
            return cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
        }

        public static SqlCeResultSet updatedata(string komenda)
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = polaczenie;
            cmd.CommandText = komenda;
            return cmd.ExecuteResultSet(ResultSetOptions.Scrollable | ResultSetOptions.Updatable);
        }        
    }
}
