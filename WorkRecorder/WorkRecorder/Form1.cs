﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WorkRecorder.BAL;
using WorkRecorder.Model.Domain;




namespace WorkRecorder
{
    public partial class Form1 : Form
    {
        private RecordService _db;
        private RoleService _roleService;
        private ProjectService _projectService;
        private GoogleCalendarService _googleCalendarService;

        public Form1()
        {
            InitializeComponent();
            tpOpen.CustomFormat = "HH : mm";
            tpClose.CustomFormat = "HH : mm";
            setTimePicker();

            _db = new RecordService();
            _roleService = new RoleService();
            _projectService = new ProjectService();
            _googleCalendarService = new GoogleCalendarService();

            //建立SQLite DB檔案
            if (!File.Exists(Application.StartupPath + @"\smallCRM.db"))
            {
                SQLiteConnection.CreateFile("smallCRM.db");
                new WorkRecorder.DAL.SqliteDao().CreateDb();
            }

            //設定下拉選單
            SetDDL(cbProj, _projectService.GetAll(), "Name", "ProjectID");
            SetDDL(cbRole, _roleService.GetAll(), "RoleName", "RoleID");

            //表擔位置
            //System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width ;//'螢幕的寬 
            //System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;//'螢幕的長 
            this.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width; 
        }

        //縮到最小視窗
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon1.Visible = true;
                this.Hide();
            }
            else
            {
                this.notifyIcon1.Visible = false;
            }
        }

        //防止關閉
        protected override void WndProc(ref Message m)
        {   //不給關
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }

            base.WndProc(ref m);
        }


        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
                
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            setTimePicker();

            timer1.Enabled = false;
            timer1.Stop();
            this.notifyIcon1.Visible = false;
            this.Show();
        }

        private void setTimePicker() 
        {
            tpOpen.Value = DateTime.Now;
            tpClose.Value = DateTime.Now.AddHours(1);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (checkData())
            {
                #region set up timer
                int timespan = (((tpClose.Value.Hour - DateTime.Now.Hour) * 60 + tpClose.Value.Minute - DateTime.Now.Minute) * 60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond;
                timer1.Interval = timespan;
                timer1.Enabled = true;
                timer1.Start();
                #endregion

                #region set up model
                RecordModel model = new RecordModel();
                model.ID = Guid.NewGuid().ToString().ToLower();
                //model.UserName = txtUserName.Text;
                model.Project = cbProj.Text;
                model.Role = cbRole.Text;
                model.Title = txtTitle.Text;
                model.Description = txtDescription.Text;
                model.OpenTime = tpOpen.Value;
                model.CloseTime = tpClose.Value;
                #endregion 

                #region save to db
                try
                {
                    _db.Add(model);
                    _googleCalendarService.Add(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("發生未預期的錯誤，請洽安迪：" + ex.Message);
                }
                #endregion

                #region visible window show icon
                this.notifyIcon1.Visible = true;
                this.Hide();
                #endregion
            }
        }

        private Boolean checkData() 
        {
            //檢查使用者名稱
            //if (string.IsNullOrEmpty(txtUserName.Text)) 
            //{
            //    MessageBox.Show("你是誰？");
            //    return false;
            //}
            //檢查工作內容
            //if (string.IsNullOrEmpty(txtDescription.Text))
            //{
            //    MessageBox.Show("工作內容？");
            //    return false; 
            //}
            //檢查標題
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("標題？");
                return false;
            }
            //檢查結束時間 > 開始時間
            if (tpClose.Value < tpOpen.Value)
            {
                MessageBox.Show("結束時間需大於開始時間");
                return false; 
            }
            //檢查結束時間 > 現在時間
            if (tpClose.Value < DateTime.Now)
            {
                MessageBox.Show("結束時間需大於目前時間");
                return false; 
            }

            return true;
        }

        private void roleSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void roleSettingToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f2 = new Settings();
            f2.Show();
        }

        private void SetDDL(ComboBox combobox, object DataSource, string DisplayMember, string ValueMember)
        {
            combobox.DataSource = DataSource;
            combobox.DisplayMember = DisplayMember;
            combobox.ValueMember = ValueMember;
        }
    }
}