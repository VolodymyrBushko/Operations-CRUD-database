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
    public partial class SalaryF : Form
    {
        public SalaryF()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeComboBoxPosition();
        }

        private void InitializeComboBoxPosition()
        {
            using (MyModel context = new MyModel())
            {
                comboBoxPosition.Items.AddRange(context.Positions.ToArray());
            }
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.Salaries.Select(s => new
                    {
                        Id = s.id,
                        Amount = s.amount,
                        Date = s.dtSalary,
                        Position = s.Position.name
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
                    Salary salary = new Salary()
                    {
                        amount = Convert.ToInt32(textBoxAmount.Text),
                        dtSalary = Convert.ToDateTime(textBoxDate.Text),
                        idPosition = (comboBoxPosition.SelectedItem as Position).id
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.Salaries.Add(salary);
                        context.SaveChanges();
                    }
                    TextBoxManager.ClearTextBoxes(this.Controls.OfType<TextBox>());
                    this.comboBoxPosition.Text = string.Empty;
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
                        Salary salary = context.Salaries.First(s => s.id == id);
                        salary.amount = Convert.ToInt32(textBoxAmount.Text);
                        salary.dtSalary = Convert.ToDateTime(textBoxDate.Text);
                        salary.idPosition = (comboBoxPosition.SelectedItem as Position).id;
                        context.SaveChanges();
                    }
                    TextBoxManager.IsNotNullOrWhiteSpaceTextBoxes(this.Controls.OfType<TextBox>());
                    this.comboBoxPosition.Text = string.Empty;
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
                        Salary salary = context.Salaries.First(s => s.id == id);
                        context.Salaries.Remove(salary);
                        context.SaveChanges();
                    }
                    InitializeDataGridView();
                }
            }
            catch { }
        }
    }
}