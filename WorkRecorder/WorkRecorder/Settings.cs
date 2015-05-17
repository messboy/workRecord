using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkRecorder.UserControls;

namespace WorkRecorder
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            //預設side menu 展開
            treeView1.ExpandAll();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Google 日曆（網路）":
                    showScreen(new uc_GoogleCalendar());
                    break;
                case "專案設定":
                    showScreen(new uc_SetProject());
                    break;
                case "角色設定":
                    showScreen(new uc_SetRole());
                    break;
                case "Log":
                    showScreen(new uc_Log());
                    break;
                // etc...
            }
        }

        private void showScreen(Control ctl)
        {
            while (splitContainer1.Panel2.Controls.Count > 0)
                splitContainer1.Panel2.Controls[0].Dispose();
            // Support forms too:
            if (ctl is Form)
            {
                var frm = ctl as Form;
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Visible = true;
            }
            ctl.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(ctl);
        }
    }
}
