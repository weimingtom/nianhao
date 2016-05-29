using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace nianhao
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.treeViewLeftMenu.Nodes.Clear();

            TreeNode queryNode = this.treeViewLeftMenu.Nodes.Add("查询类型");
            TreeNode helpNode = this.treeViewLeftMenu.Nodes.Add("帮助");
            queryNode.Nodes.Add(new TreeNode("年号", 14, 14));
            queryNode.Expand();
            helpNode.Nodes.Add(new TreeNode("设置", 15, 15));
            helpNode.Expand();

            this.listViewQuery.BeginUpdate();
            this.listViewQuery.Columns.Add("帝王名号", 200, HorizontalAlignment.Left); //一步添加  
            this.listViewQuery.Columns.Add("年号", 80, HorizontalAlignment.Left); //一步添加  
            this.listViewQuery.Columns.Add("使用年数", 80, HorizontalAlignment.Left); //一步添加  
            this.listViewQuery.Columns.Add("元年公元", 80, HorizontalAlignment.Left); //一步添加  
            this.listViewQuery.EndUpdate();

            TestAddList();
        }

        private void TestAddList()
        {
            this.listViewQuery.BeginUpdate();
            for (int i = 0; i < 10; i++)
            {
                this.listViewQuery.Items.Add(new ListViewItem(
                    new string[]{"帝号", "年号", "年数", "公元"}, 14));
            }
            this.listViewQuery.EndUpdate();
        }

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddEraForm addEraForm = new AddEraForm();
            addEraForm.ShowDialog(this);
        }

        private void listViewQuery_DoubleClick(object sender, EventArgs e)
        {
            AddEraForm addEraForm = new AddEraForm();
            addEraForm.ShowDialog(this);
        }
    }
}
