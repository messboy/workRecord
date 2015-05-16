using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.DAL;
using WorkRecorder.Model;
using WorkRecorder.Model.Domain;

namespace WorkRecorder.BAL
{
    public class ProjectService
    {
        private SqliteDao db;
        public ProjectService()
        {
            db = new SqliteDao();
        }
        public void Add()
        {

        }

        public void Update()
        {

        }

        public void Delete()
        {

        }

        public List<ProjectModel> GetAll()
        {
            return db.GetAllProjects();
        }

        public void GetById(string id)
        {

        }
    }
}
