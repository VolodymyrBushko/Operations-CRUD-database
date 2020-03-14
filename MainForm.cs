using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolodEF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectForm(object sender, EventArgs e)
        {
            Form form = null;
            switch ((sender as ToolStripMenuItem).Text)
            {
                case "Department" : form = new Forms.DepartmentF(); break;
                case "Employee"   : form = new Forms.EmployeeF();   break;
                case "Manager"    : form = new Forms.ManagerF();    break;
                case "Position"   : form = new Forms.PositionF();   break;
                case "DepEmp"     : form = new Forms.DepEmpF();     break;
                case "Salary"     : form = new Forms.SalaryF();     break;
                case "Vacation"   : form = new Forms.VacationF();   break;
            }
            form.ShowDialog();
        }
    }
}