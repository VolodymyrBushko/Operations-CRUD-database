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
    public partial class VacationF : Form
    {
        public VacationF()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeComboBoxEmployee();
        }

        private void InitializeComboBoxEmployee()
        {
            using (MyModel context = new MyModel())
            {
                comboBoxEmployee.Items.AddRange(context.Employees.ToArray());
            }
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.Vacations.Select(v => new
                    {
                        EmpId = v.Employee.id,
                        EmpName = v.Employee.name,
                        Duration = v.duration,
                        Date = v.dtStart
                    }).ToList();
                dataGridView1.DataSource = bindingSource1;
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxManager.IsNotNullOrWhiteSpaceTextBoxes(this.Controls.OfType<TextBox>()))
                {
                    Vacation vacation = new Vacation()
                    {
                        duration = Convert.ToInt32(textBoxDuration.Text),
                        dtStart = Convert.ToDateTime(textBoxDate.Text),
                        idEmployee = (comboBoxEmployee.SelectedItem as Employee).id
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.Vacations.Add(vacation);
                        context.SaveChanges();
                    }
                    TextBoxManager.ClearTextBoxes(this.Controls.OfType<TextBox>());
                    this.comboBoxEmployee.Text = string.Empty;
                    InitializeDataGridView();
                }
            }
            catch { }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxDuration.Text) && dataGridView1.SelectedRows[0] != null)
                {
                    DateTime date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Date"].Value);
                    int empId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmpId"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Vacation vacation = context.Vacations.First(v => v.dtStart.Equals(date) && v.idEmployee == empId);
                        vacation.duration = Convert.ToInt32(textBoxDuration.Text);
                        context.SaveChanges();
                    }
                    TextBoxManager.ClearTextBoxes(this.Controls.OfType<TextBox>());
                    this.comboBoxEmployee.Text = string.Empty;
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
                    DateTime date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Date"].Value);
                    int empId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmpId"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Vacation vacation = context.Vacations.First(v => v.dtStart.Equals(date) && v.idEmployee == empId);
                        context.Vacations.Remove(vacation);
                        context.SaveChanges();
                    }
                    InitializeDataGridView();
                }
            }
            catch { }
        }
    }
}