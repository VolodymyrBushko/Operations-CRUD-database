using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolodEF.Database;

namespace VolodEF.Forms
{
    public partial class ManagerF : Form
    {
        public ManagerF()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeComboBoxDepartment();
            InitializeComboBoxEmployee();
        }

        private void InitializeComboBoxEmployee()
        {
            using (MyModel context = new MyModel())
            {
                comboBoxEmployee.Items.AddRange(context.Employees.ToArray());
            }
        }

        private void InitializeComboBoxDepartment()
        {
            using (MyModel context = new MyModel())
            {
                comboBoxDepartment.Items.AddRange(context.Departaments.ToArray());
            }
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.Managers.Select(m => new
                    {
                        DepId = m.Departament.id,
                        DepName = m.Departament.name,
                        EmpId = m.Employee.id,
                        EmpName = m.Employee.name,
                        Date = m.dtStart
                    }).ToList();
                dataGridView1.DataSource = bindingSource1;
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxDate.Text) &&
                    comboBoxDepartment.SelectedItem != null &&
                    comboBoxEmployee.SelectedItem != null)
                {
                    Manager manager = new Manager()
                    {
                        dtStart = Convert.ToDateTime(textBoxDate.Text),
                        idDepartament = (comboBoxDepartment.SelectedItem as Department).id,
                        idEmployee = (comboBoxEmployee.SelectedItem as Employee).id
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.Managers.Add(manager);
                        context.SaveChanges();
                    }
                    this.comboBoxDepartment.Text = string.Empty;
                    this.comboBoxEmployee.Text = string.Empty;
                    this.textBoxDate.Clear();
                    InitializeDataGridView();
                }
            }
            catch { }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxDate.Text) &&
                   comboBoxDepartment.SelectedItem != null &&
                   comboBoxEmployee.SelectedItem != null)
                {
                    int depId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["DepId"].Value);
                    int empId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmpId"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Manager manager = context.Managers.First(m => m.Departament.id == depId && m.Employee.id == empId);
                        manager.dtStart = Convert.ToDateTime(textBoxDate.Text);
                        manager.idDepartament = (comboBoxDepartment.SelectedItem as Department).id;
                        manager.idEmployee = (comboBoxEmployee.SelectedItem as Employee).id;
                        context.SaveChanges();
                    }
                    this.comboBoxDepartment.Text = string.Empty;
                    this.comboBoxEmployee.Text = string.Empty;
                    this.textBoxDate.Clear();
                    InitializeDataGridView();
                }
            }
            catch { }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows[0] != null)
                {
                    int depId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["DepId"].Value);
                    int empId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmpId"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Manager manager = context.Managers.First(m => m.Departament.id == depId && m.Employee.id == empId);
                        context.Managers.Remove(manager);
                        context.SaveChanges();
                    }
                    InitializeDataGridView();
                }
            }
            catch { }
        }
    }
}