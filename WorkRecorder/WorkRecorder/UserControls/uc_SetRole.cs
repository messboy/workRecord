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
    public partial class uc_SetRole : UserControl
    {
        private RoleService _roleService;
        

        public uc_SetRole()
        {
            InitializeComponent();
            _roleService = new RoleService();
            Init();
            
        }

        private void Init()
        {
            RoleGridView.DataSource = _roleService.GetAll();
        }
    }
}
