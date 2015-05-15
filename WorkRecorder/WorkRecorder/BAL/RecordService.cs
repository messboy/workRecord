using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkRecorder.DAL;
using WorkRecorder.Model;

namespace WorkRecorder.BAL
{
    public class RecordService
    {
        private SqlLiteHelper db;
        public RecordService() {
            db = new SqlLiteHelper();
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

        public void GetAll()
        {

        }

        public void GetById(string id)
        {

        }
    }
}
