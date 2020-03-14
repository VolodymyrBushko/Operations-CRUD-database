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
    public partial class DepartmentF : Form
    {
        public DepartmentF()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.Departaments.Select(d => new
                    {
                        Id = d.id,
                        Name = d.name
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
                    Department department = new Department()
                    {
                        name = textBoxName.Text
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.Departaments.Add(department);
                        context.SaveChanges();
                    }
                    this.textBoxName.Clear();
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
                        Department department = context.Departaments.First(d => d.id == id);
                        department.name = textBoxName.Text;
                        context.SaveChanges();
                    }
                    this.textBoxName.Clear();
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
                        Department department = context.Departaments.First(d => d.id == id);
                        context.Departaments.Remove(department);
                        context.SaveChanges();
                    }
                    InitializeDataGridView();
                }
            }
            catch { }
        }
    }
}