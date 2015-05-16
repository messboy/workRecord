using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkRecorder.Model.ViewModel
{
    public class CalendarEventsViewModel
    {
        public CalendarEventsViewModel()
        {
        }

        public CalendarEventsViewModel(Event eventItem)
        {
            this.CalendarID = eventItem.ICalUID;
            this.ID = eventItem.Id;
            this.Project = GetProject(eventItem);
            this.Title = GetTitle(eventItem);
            this.Description = eventItem.Description;
            this.StartDate = GetStartDate(eventItem);
            this.EndDate = GetEndDate(eventItem); ;
        }

        public string CalendarID { get; set; }
        public string ID { get; set; }
        public string Role { get; set; }
        public string Project { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        private string GetStartDate(Event eventItem)
        {
            string when = eventItem.Start.DateTime.ToString();
            if (String.IsNullOrEmpty(when))
            {
                when = eventItem.Start.Date;
            }
            return when;
        }

        private string GetEndDate(Event eventItem)
        {
            string when = eventItem.End.DateTime.ToString();
            if (String.IsNullOrEmpty(when))
            {
                when = eventItem.End.Date;
            }
            return when;
        }

        private string GetProject(Event eventItem)
        {
            var index = eventItem.Summary.LastIndexOf("]");
            if (index < 0 )
            {
                return string.Empty;
            }
            return eventItem.Summary.Substring(0, index + 1);
        }

        private string GetTitle(Event eventItem)
        {
            var index = eventItem.Summary.LastIndexOf("]");
            if (index < 0)
            {
                return eventItem.Summary;
            }
            var title = eventItem.Summary.Substring(eventItem.Summary.LastIndexOf("]") + 1);
            return title;
        }
    }
}
