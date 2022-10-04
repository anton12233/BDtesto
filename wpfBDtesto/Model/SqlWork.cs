using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpfBDtesto.Model
{
    internal class SqlWork
    {
        
        private static SqlConnection connection = new SqlConnection("Server=localhost; Database=testoAdjale; Trusted_Connection=True;");
        
        public static DataSet selectData(string query)
        {
            try
            {
                connection.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                
                adapter.Fill(ds);
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["EventNumber"] };

                return ds;

            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void updateData(DataSet ds, string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(ds);

        }

    }
}
