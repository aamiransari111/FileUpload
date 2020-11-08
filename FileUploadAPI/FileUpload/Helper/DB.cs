using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.Helper
{
    public class DB
    {
        SqlConnection connection = new SqlConnection(@"server=(localdb)\ProjectsV13;database=Test;integrated security=true");
        SqlTransaction transaction;
        public void BulkCopy(DataTable dt)
        {
            connection.Open();
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
            {
                bulkCopy.DestinationTableName = "tbl_Users";
                bulkCopy.EnableStreaming = true;

                bulkCopy.WriteToServer(dt);
            }
            connection.Close();
        }
    }
}
