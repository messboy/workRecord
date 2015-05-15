using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.DAL;
using WorkRecorder.Model;

namespace WorkRecorder.BAL
{
    public class RoleService
    {
        private SqlLiteHelper _db;
        public RoleService() 
        {
            _db = new SqlLiteHelper();
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
