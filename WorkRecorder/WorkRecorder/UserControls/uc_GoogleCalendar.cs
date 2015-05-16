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
using WorkRecorder.BAL;



namespace WorkRecorder
{
    public partial class uc_GoogleCalendar : UserControl
    {
        private IList<string> scopes = new List<string>();
        private GoogleCalendarService _googleCalendarService;

        public uc_GoogleCalendar()
        {
            InitializeComponent();
            _googleCalendarService = new GoogleCalendarService();

            try
            {
                //設定下拉選單
                SetDDL(comboBox1);


                //設定內容顯示
                initGridView();
                dataGridView1.DataSource = _googleCalendarService.GetEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = comboBox1.SelectedValue.ToString();
            dataGridView1.DataSource = _googleCalendarService.GetEvents(id);
        }

        private void SetDDL(ComboBox combobox)
        {
            combobox.DataSource = _googleCalendarService.GetCalendarList();
            combobox.DisplayMember = "Summary";
            combobox.ValueMember = "CalendarID";
        }

        public void initGridView()
        {

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "colField";
            col1.Width = 100;
            col1.HeaderText = "專案";
            col1.DataPropertyName = "Project";
            col1.ReadOnly = true;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "colField";
            col4.Width = 40;
            col4.HeaderText = "角色";
            col4.DataPropertyName = "Role";
            col4.ReadOnly = true;

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "colField";
            col2.Width = 130;
            col2.HeaderText = "標題";
            col2.DataPropertyName = "Title";
            col2.ReadOnly = true;

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "colField";
            col3.Width = 200;
            col3.HeaderText = "內容";
            col3.DataPropertyName = "Description";
            col3.ReadOnly = true;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "colField";
            col5.Width = 100;
            col5.HeaderText = "開始時間";
            col5.DataPropertyName = "StartDate";
            col5.ReadOnly = true;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "colField";
            col6.Width = 100;
            col6.HeaderText = "結束時間";
            col6.DataPropertyName = "EndDate";
            col6.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 
                col1,
                col4,
                col2,
                col3,
                col5,
                col6
            });

            dataGridView1.AutoGenerateColumns = false;
        }
    }
}
