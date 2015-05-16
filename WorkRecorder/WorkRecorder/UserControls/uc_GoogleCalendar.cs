using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using WorkRecorder.Model.ViewModel;



namespace WorkRecorder
{
    public partial class uc_GoogleCalendar : UserControl
    {
        private IList<string> scopes = new List<string>();
        private CalendarService service;
        private List<CalendarViewModel> caList;

        public uc_GoogleCalendar()
        {
            InitializeComponent();

            try
            {
                //初始化
                InitCanledarService();

                //設定下拉選單
                SetDDL(comboBox1);

                //設定內容顯示
                SetBizLogic(GetEvents());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = comboBox1.SelectedValue.ToString();
            SetBizLogic(GetEvents(id));
        }

        private void InitCanledarService()
        {
            scopes.Add(CalendarService.Scope.Calendar);

            UserCredential credential;

            //oauth2
            using (FileStream stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
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

        private void SetBizLogic(Events events)
        {
            textBox1.Text = string.Empty;
            if (events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    textBox1.Text += string.Format("{0} ({1}) \r\n", eventItem.Summary, when);
                }
            }
            else
            {
                textBox1.Text += string.Format("No upcoming events found.");
            }
        }

        private Events GetEvents(string CalendarID = "primary")
        {
            // Define parameters of request.
            //Calendar ID 就是去網頁裡面找設定
            EventsResource.ListRequest request = service.Events.List(CalendarID);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 30;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return request.Execute();

        }

        private void SetDDL(ComboBox combobox)
        {
            var list = service.CalendarList.List().Execute().Items.ToList();
            caList = new List<CalendarViewModel>();
            list.ForEach(m => caList.Add(new CalendarViewModel()
            {
                CalendarID = m.Id,
                Summary = m.Summary
            }));

            combobox.DataSource = caList;
            combobox.DisplayMember = "Summary";
            combobox.ValueMember = "CalendarID";
        }
    }
}
