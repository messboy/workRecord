using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkRecorder.Model;

namespace WorkRecorder.DAL
{
    public class SqlLiteHelper
    {
        public SQLiteConnection sqlite_connect { get; set; }
        public SQLiteCommand sqlite_cmd { get; set; }

        public void CreateDb()
        {
            if (!File.Exists(Application.StartupPath + @"\smallCRM.db"))
            {
                SQLiteConnection.CreateFile("smallCRM.db");
            }

            sqlite_connect = new SQLiteConnection("Data source=smallCRM.db");
            //建立資料庫連線

            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command

            sqlite_cmd.CommandText = @"CREATE TABLE 'Record' (
	                                    'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                                    'UserName'	TEXT NOT NULL,
	                                    'Project'	TEXT,
	                                    'Role'	TEXT,
	                                    'Description'	TEXT,
	                                    'OpenTime'	TEXT,
	                                    'CloseTime'	TEXT);";
            //create table header
            //INTEGER PRIMARY KEY AUTOINCREMENT=>auto increase index
            sqlite_cmd.ExecuteNonQuery(); //using behind every write cmd

          

            sqlite_connect.Close();
        }

        public void TestSelectDb() {
            sqlite_connect = new SQLiteConnection("Data source=smallCRM.db");
            //建立資料庫連線
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command

            sqlite_cmd.CommandText = "INSERT INTO Record VALUES (null, '小名', '小專案', 'PG', 'codeing', '2015-5-15', '2015-5-15');";
            sqlite_cmd.ExecuteNonQuery();//using behind every write cmd

            sqlite_cmd.CommandText = "SELECT * FROM Record"; //select table

            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) //read every data
            {
                String name_load = sqlite_datareader["UserName"].ToString();
                String phone_number_load = sqlite_datareader["Project"].ToString();
                //MessageBox.Show(name_load + ":" + phone_number_load);
            }
            sqlite_connect.Close();
        }

        public void Insert(RecordModel model)
        {
            using (sqlite_connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SmallCRM_SqlLite"].ToString()))
            {

                string sqlString = @"
                        INSERT INTO Record(ID, UserName, Project, Role, Description, OpenTime, CloseTime)
                               VALUES(null, @UserName, @Project, @Role, @Description, @OpenTime, @CloseTime)";
                
                sqlite_connect.Open();

                //sqlite_connect.CreateCommand();//create command
                sqlite_cmd = new SQLiteCommand(sqlString, sqlite_connect);
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@UserName", model.UserName));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Project", model.Project));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Role", model.Role));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Description", model.Description));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@OpenTime", model.OpenTime));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@CloseTime", model.CloseTime));


                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd

                sqlite_connect.Close();
            }
        }

        internal void InsertRole(Model.RoleModel model)
        {
            throw new NotImplementedException();
        }

        public List<RoleModel> GetAllRoles()
        {
            using (sqlite_connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SmallCRM_SqlLite"].ToString()))
            {
                sqlite_connect.Open();
                sqlite_cmd = sqlite_connect.CreateCommand();//create command
                sqlite_cmd.CommandText = "SELECT * FROM Role"; //select table
                SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

                List<RoleModel> list = new List<RoleModel>();
                while (sqlite_datareader.Read()) //read every data
                {
                    RoleModel m = new RoleModel()
                    {
                        RoleID = sqlite_datareader["RoleID"].ToString(),
                        RoleName = sqlite_datareader["RoleName"].ToString()
                    };
                    list.Add(m);
                }
                sqlite_connect.Close();

                return list;
            }
        }

        public List<ProjectModel> GetAllProjects()
        {
            using (sqlite_connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SmallCRM_SqlLite"].ToString()))
            {
                sqlite_connect.Open();
                sqlite_cmd = sqlite_connect.CreateCommand();//create command
                sqlite_cmd.CommandText = "SELECT * FROM Project"; //select table
                SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

                List<ProjectModel> list = new List<ProjectModel>();
                while (sqlite_datareader.Read()) //read every data
                {
                    ProjectModel m = new ProjectModel()
                    {
                        ProjectID = sqlite_datareader["ProjectID"].ToString(),
                        Name = sqlite_datareader["Name"].ToString()
                    };
                    list.Add(m);
                }
                sqlite_connect.Close();

                return list;
            }
        }
    }
}
