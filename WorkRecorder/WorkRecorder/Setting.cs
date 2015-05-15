using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkRecorder.BAL;

namespace WorkRecorder
{
    public partial class Setting : Form
    {
        private RoleService _roleService;
        private ProjectService _projectService;
        public Setting()
        {
            InitializeComponent();
            _roleService = new RoleService();
            _projectService = new ProjectService();

            RoleGridView.DataSource = _roleService.GetAll();
            ProjectGridView.DataSource = _projectService.GetAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
