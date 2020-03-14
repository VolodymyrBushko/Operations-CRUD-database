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
    public partial class PositionF : Form
    {
        public PositionF()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            using (MyModel context = new MyModel())
            {
                bindingSource1.DataSource =
                    context.Positions.Select(p => new
                    {
                        Id = p.id,
                        Name = p.name,
                        Description = p.description
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
                    Position position = new Position()
                    {
                        name = textBoxName.Text,
                        description = textBoxDescription.Text
                    };
                    using (MyModel context = new MyModel())
                    {
                        context.Positions.Add(position);
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
                        Position position = context.Positions.First(p => p.id == id);
                        position.name = textBoxName.Text;
                        position.description = textBoxDescription.Text;
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
                        Position position = context.Positions.First(p => p.id == id);
                        context.Positions.Remove(position);
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