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
    public partial class uc_SetProject : UserControl
    {
        private ProjectService _projectService;
        public uc_SetProject()
        {
            InitializeComponent();
            _projectService = new ProjectService();
            Init();
        }

        private void Init()
        {
            ProjectGridView.DataSource = _projectService.GetAll();
        }
    }
}
