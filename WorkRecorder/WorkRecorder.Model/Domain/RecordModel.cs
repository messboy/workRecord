using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkRecorder.Model.Domain
{
    public class RecordModel
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Project { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }

        public static List<RecordModel> DataTableToList(DataTable dtInput)
        {
            List<RecordModel> objectList = new List<RecordModel>();

            foreach (DataRow dr in dtInput.Rows)
            {
                RecordModel newObj = new RecordModel();
                newObj.ID = dr["ID"].ToString();
                newObj.UserName = dr["UserName"].ToString();
                newObj.Project = dr["Project"].ToString();
                newObj.Role = dr["Role"].ToString();
                newObj.Title = dr["Title"].ToString();
                newObj.Description = dr["Description"].ToString();
                newObj.OpenTime = Convert.ToDateTime(dr["OpenTime"]);
                newObj.CloseTime = Convert.ToDateTime(dr["CloseTime"]);


                objectList.Add(newObj);
            }
            return objectList;
        }
    }
}
