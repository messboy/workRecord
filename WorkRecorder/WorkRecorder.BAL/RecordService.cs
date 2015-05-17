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
    public class RecordService
    {
        private SqliteDao db;
        public RecordService() {
            db = new SqliteDao();
        }
        public void Add(RecordModel model)
        {
            db.Insert(model);
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }

        public List<RecordModel> GetAll()
        {
            return db.GetAll();
        }

        public void GetById(string id)
        {

        }
    }
}
