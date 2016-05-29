using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace nianhao
{
    public partial class AddEraForm : Form
    {
        private int lastSelectItem = -1;
        private EraModel model = new EraModel();

        public AddEraForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WriteModel();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddEraForm_Load(object sender, EventArgs e)
        {
            this.listViewProperty.Columns.Add("属性", 100, HorizontalAlignment.Left); //一步添加  
            this.listViewProperty.Columns.Add("值", 100, HorizontalAlignment.Left); //一步添加  

            RefreshList();

            //this.listViewProperty.SelectedItems = 0;
            this.lastSelectItem = 0;
        }

        private void RefreshList()
        {
            this.listViewProperty.BeginUpdate();
            if (this.listViewProperty.Items.Count == 0)
            {
                this.listViewProperty.Items.Add(new ListViewItem(new string[] { "帝王名号", model.name}, 0));
                this.listViewProperty.Items.Add(new ListViewItem(new string[] {"年号", model.era}, 0));
                this.listViewProperty.Items.Add(new ListViewItem(new string[] {"年数", model.year}, 0));
                this.listViewProperty.Items.Add(new ListViewItem(new string[] {"元年公元", model.fromTime}, 0));
            }
            else
            {
                this.listViewProperty.Items[0].SubItems[1].Text = model.name;
                this.listViewProperty.Items[1].SubItems[1].Text = model.era;
                this.listViewProperty.Items[2].SubItems[1].Text = model.year;
                this.listViewProperty.Items[3].SubItems[1].Text = model.fromTime;
            }
            this.listViewProperty.EndUpdate();
        }

        private void WriteModel()
        {
            switch (this.lastSelectItem)
            {
                default:
                case 0:
                    model.name = textBox1.Text;
                    break;

                case 1:
                    model.era = textBox1.Text;
                    break;

                case 2:
                    model.year = textBox1.Text;
                    break;

                case 3:
                    model.fromTime = textBox1.Text;
                    break;
            }
            RefreshList();

            if (this.listViewProperty.SelectedItems.Count > 0)
            {
                this.lastSelectItem = this.listViewProperty.SelectedItems[0].Index;
                switch (this.listViewProperty.SelectedItems[0].Index)
                {
                    default:
                    case 0:
                        textBox1.Text = model.name;
                        break;

                    case 1:
                        textBox1.Text = model.era;
                        break;

                    case 2:
                        textBox1.Text = model.year;
                        break;

                    case 3:
                        textBox1.Text = model.fromTime;
                        break;
                }
            }
        }

        private void listViewProperty_ItemActivate(object sender, EventArgs e)
        {
        }

        private void listViewProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteModel();
        }
    }
}
