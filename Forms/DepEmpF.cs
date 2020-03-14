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
    public partial class DepEmpF : Form
    {
        public DepEmpF()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeComboBoxEmployee();
            InitializeComboBoxDepartment();
            InitializeComboBoxPosition();
        }

        private void InitializeComboBoxPosition()
        {
            using (MyModel context = new MyModel())
            {
                this.comboBoxPosition.Items.AddRange(context.Positions.ToArray());
            }
        }

        private void InitializeComboBoxDepartment()
        {
            using (MyModel context = new MyModel())
            {
                this.comboBoxDepartment.Items.AddRange(context.Departaments.ToArray());
            }
        }

        private void InitializeComboBoxEmployee()
        {
            using (MyModel context = new MyModel())
            {
                this.comboBoxEmployee.Items.AddRange(context.Employees.ToArray());
            }
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.DepEmps.Select(de => new
                    {
                        DepId = de.idDepartament,
                        DepName = de.Departament.name,
                        EmpId = de.idEmployee,
                        EmpName = de.Employee.name,
                        Date = de.dt,
                        Position = de.Position.name
                    }).ToList();
                dataGridView1.DataSource = bindingSource1;
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxDate.Text) &&
                    comboBoxEmployee.SelectedItem != null &&
                    comboBoxDepartment.SelectedItem != null &&
                    comboBoxPosition.SelectedItem != null)
                {
                    DepEmp depEmp = new DepEmp()
                    {
                        dt = Convert.ToDateTime(textBoxDate.Text),
                        idEmployee = (comboBoxEmployee.SelectedItem as Employee).id,
                        idDepartament = (comboBoxDepartment.SelectedItem as Department).id,
                        idPosition = (comboBoxPosition.SelectedItem as Position).id
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.DepEmps.Add(depEmp);
                        context.SaveChanges();
                    }
                    this.textBoxDate.Clear();
                    this.comboBoxEmployee.Text = this.comboBoxDepartment.Text = this.comboBoxPosition.Text = string.Empty;
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
                   dataGridView1.SelectedRows[0] != null &&
                   comboBoxEmployee.SelectedItem != null &&
                   comboBoxDepartment.SelectedItem != null &&
                   comboBoxPosition.SelectedItem != null)
                {
                    int empId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmpId"].Value);
                    int depId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["DepId"].Value);
                    using (MyModel context = new MyModel())
                    {
                        DepEmp depEmp = context.DepEmps.First(de => de.idEmployee == empId && de.idDepartament == depId);
                        depEmp.dt = Convert.ToDateTime(textBoxDate.Text);
                        depEmp.idPosition = (comboBoxPosition.SelectedItem as Position).id;
                        context.SaveChanges();
                    }
                    this.textBoxDate.Clear();
                    this.comboBoxEmployee.Text = this.comboBoxDepartment.Text = this.comboBoxPosition.Text = string.Empty;
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
                    int empId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmpId"].Value);
                    int depId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["DepId"].Value);
                    using (MyModel context = new MyModel())
                    {
                        DepEmp depEmp = context.DepEmps.First(de => de.idEmployee == empId && de.idDepartament == depId);
                        context.DepEmps.Remove(depEmp);
                        context.SaveChanges();
                    }
                    InitializeDataGridView();
                }
            }
            catch { }
        }
    }
}