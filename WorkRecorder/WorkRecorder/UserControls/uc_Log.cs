using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkRecorder.BAL;

namespace WorkRecorder.UserControls
{
    public partial class uc_Log : UserControl
    {
        private IList<string> scopes = new List<string>();
        private RecordService _recordService;
        public uc_Log()
        {
            InitializeComponent();
            _recordService = new RecordService();

            try
            {

                //設定內容顯示
                initGridView();
                dataGridView1.DataSource = _recordService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            col2.Width = 150;
            col2.HeaderText = "標題";
            col2.DataPropertyName = "Title";
            col2.ReadOnly = true;

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "colField";
            col3.Width = 230;
            col3.HeaderText = "內容";
            col3.DataPropertyName = "Description";
            col3.ReadOnly = true;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "colField";
            col5.Width = 130;
            col5.HeaderText = "開始時間";
            col5.DataPropertyName = "OpenTime";
            col5.ReadOnly = true;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "colField";
            col6.Width = 130;
            col6.HeaderText = "結束時間";
            col6.DataPropertyName = "CloseTime";
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
