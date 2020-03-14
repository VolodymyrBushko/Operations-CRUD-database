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
    public partial class EmployeeF : Form
    {
        public EmployeeF()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.Employees.Select(emp => new
                    {
                        Id = emp.id,
                        Name = emp.name,
                        Surname = emp.surname,
                        Passport = emp.passport
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
                    Employee employee = new Employee()
                    {
                        name = textBoxName.Text,
                        surname = textBoxSurname.Text,
                        passport = Convert.ToInt32(textBoxPassport.Text)
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.Employees.Add(employee);
                        context.SaveChanges();
                    }
                    TextBoxManager.ClearTextBoxes(this.Controls.OfType<TextBox>());
                    InitializeDataGridView();
                }
            }
            catch { }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxManager.IsNotNullOrWhiteSpaceTextBoxes(this.Controls.OfType<TextBox>()) &&
                    dataGridView1.SelectedRows[0] != null)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Employee employee = context.Employees.First(emp => emp.id == id);
                        employee.name = textBoxName.Text;
                        employee.surname = textBoxSurname.Text;
                        employee.passport = Convert.ToInt32(textBoxPassport.Text);
                        context.SaveChanges();
                    }
                    TextBoxManager.ClearTextBoxes(this.Controls.OfType<TextBox>());
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
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Employee employee = context.Employees.First(emp => emp.id == id);
                        context.Employees.Remove(employee);
                        context.SaveChanges();
                    }
                    TextBoxManager.ClearTextBoxes(this.Controls.OfType<TextBox>());
                    InitializeDataGridView();
                }
            }
            catch { }
        }
    }
}