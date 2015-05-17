using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.Model.Domain;

namespace WorkRecorder.DAL
{
    public class SqliteDao
    {
        public SQLiteConnection sqlite_connect { get; set; }
        public SQLiteCommand sqlite_cmd { get; set; }

        public void CreateDb()
        {
            //if (!File.Exists(Application.StartupPath + @"\smallCRM.db"))
            //{
            //    SQLiteConnection.CreateFile("smallCRM.db");
            //}

            sqlite_connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SmallCRM_SqlLite"].ToString());
            //建立資料庫連線

            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command

            sqlite_cmd.CommandText = @"CREATE TABLE 'Record' (
	                                    'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                                    'UserName'	TEXT NOT NULL,
	                                    'Project'	TEXT,
                                        'Title'	TEXT,
	                                    'Role'	TEXT,
	                                    'Description'	TEXT,
	                                    'OpenTime'	TEXT,
	                                    'CloseTime'	TEXT);

                                        CREATE TABLE 'Project' (
	                                        'ProjectID'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                                        'Name'	TEXT
                                        );

                                        CREATE TABLE 'Role' (
	                                        'RoleID'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                                        'RoleName'	TEXT
                                        );
                                        ";

            //create table header
            //INTEGER PRIMARY KEY AUTOINCREMENT=>auto increase index
            sqlite_cmd.ExecuteNonQuery(); //using behind every write cmd



            sqlite_connect.Close();
        }

        public void Insert(RecordModel model)
        {
            using (sqlite_connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SmallCRM_SqlLite"].ToString()))
            {

                string sqlString = @"
                        INSERT INTO Record(ID, UserName, Project, Title, Role, Description, OpenTime, CloseTime)
                               VALUES(null, @UserName, @Project, @Title, @Role, @Description, @OpenTime, @CloseTime)";

                sqlite_connect.Open();

                //sqlite_connect.CreateCommand();//create command
                sqlite_cmd = new SQLiteCommand(sqlString, sqlite_connect);
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@UserName", model.UserName ?? "system"));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Project", model.Project));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Title", model.Title));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Role", model.Role));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@Description", model.Description));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@OpenTime", model.OpenTime));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("@CloseTime", model.CloseTime));


                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd

                sqlite_connect.Close();
            }
        }

        public void InsertRole(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public List<RecordModel> GetAll()
        {
            using (sqlite_connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SmallCRM_SqlLite"].ToString()))
            {
                sqlite_connect.Open();
                sqlite_cmd = sqlite_connect.CreateCommand();//create command
                sqlite_cmd.CommandText = "SELECT * FROM Record"; //select table
                //SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

                SQLiteDataAdapter DB = new SQLiteDataAdapter(sqlite_cmd);
                DataSet DS = new DataSet();
                DataTable DT = new DataTable();
                DB.Fill(DS);
                DT = DS.Tables[0];

                //List<RecordModel> list = DT.DataTableToList<RecordModel>();
                List<RecordModel> list = RecordModel.DataTableToList(DT);

                sqlite_connect.Close();

                return list;
            }
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

    public static class Helper
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
