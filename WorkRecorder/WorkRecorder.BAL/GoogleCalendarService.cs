using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkRecorder.Model.ViewModel;

namespace WorkRecorder.BAL
{
    public class GoogleCalendarService
    {
        private IList<string> scopes = new List<string>();
        private CalendarService service;
        private List<CalendarListViewModel> caList;

        public GoogleCalendarService()
        {
            scopes.Add(CalendarService.Scope.Calendar);

            UserCredential credential;

            //oauth2 client_secret
            using (FileStream stream = new FileStream(ConfigurationManager.AppSettings["client_secret"].ToString(), FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                   GoogleClientSecrets.Load(stream).Secrets, scopes, "user", CancellationToken.None,
                   new FileDataStore("")).Result;
            }

            // Create Calendar Service.
            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "app",
            });
        }

        public List<CalendarListViewModel> GetCalendarList()
        {
            var list = service.CalendarList.List().Execute().Items.ToList();

            caList = new List<CalendarListViewModel>();
            list.ForEach(m => caList.Add(new CalendarListViewModel()
            {
                CalendarID = m.Id,
                Summary = m.Summary
            }));

            return caList;
        }

        public List<CalendarEventsViewModel> GetEvents(string CalendarID = "primary")
        {
            List<CalendarEventsViewModel> CEList = new List<CalendarEventsViewModel>();

            // Define parameters of request.
            //Calendar ID 就是去網頁裡面找設定
            EventsResource.ListRequest request = service.Events.List(CalendarID);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 30;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var events = request.Execute();

            if (events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    CEList.Add(new CalendarEventsViewModel(eventItem));
                }
            }

            return CEList;

        }
    }
}
