using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.Model.Domain;

namespace WorkRecorder.DAL
{
    public class SqlServerDao
    {
        public static void Insert(RecordModel model)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SmallCRMConnectionString"].ToString()))
            {
                string sqlString = @"
                        INSERT INTO Record
                                   (ID
                                   ,UserName
                                   ,ProjectName
                                   ,Character
                                   ,Description
                                   ,OpenTime
                                   ,CloseTime)
                             VALUES
                                   (@ID
                                   ,@UserName
                                   ,@ProjectName
                                   ,@Character
                                   ,@Description
                                   ,@OpenTime
                                   ,@CloseTime)";
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                cmd.Parameters.AddWithValue("@ID", model.ID);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@ProjectName", model.Project);
                cmd.Parameters.AddWithValue("@Character", model.Role);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@OpenTime", model.OpenTime);
                cmd.Parameters.AddWithValue("@CloseTime", model.CloseTime);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
