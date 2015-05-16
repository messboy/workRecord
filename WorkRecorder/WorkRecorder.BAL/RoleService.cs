using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.DAL;
using WorkRecorder.Model.Domain;

namespace WorkRecorder.BAL
{
    public class RoleService
    {
        private SqliteDao _db;
        public RoleService() 
        {
            _db = new SqliteDao();
        }

        public void Add(RoleModel model) 
        {
            _db.InsertRole(model);
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }

        public List<RoleModel> GetAll()
        {
            return _db.GetAllRoles();
        }

        public void GetById(string id) 
        {
 
        }
    }
}
