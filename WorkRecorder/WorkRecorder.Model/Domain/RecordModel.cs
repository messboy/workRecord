using System;
using System.Collections.Generic;
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
        public string Description { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
    }
}
