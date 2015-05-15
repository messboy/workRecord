using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.DAL;
using WorkRecorder.Model;

namespace WorkRecorder.BAL
{
    public class ProjectService
    {
        private SqlLiteHelper db;
        public ProjectService()
        {
            db = new SqlLiteHelper();
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
