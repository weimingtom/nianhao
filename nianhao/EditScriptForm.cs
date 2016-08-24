using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

//http://www.java2s.com/Code/CSharp/GUI-Windows-Form/ReadanXMLDocumentanddisplaythefileasaTree.htm
//http://www.cnblogs.com/chenleiustc/archive/2009/07/21/1527868.html
//https://github.com/QuickenLoans/RegExpose/blob/master/RegExpose.UI/RichTextExtensions.cs
namespace nianhao
{
    public partial class EditScriptForm : Form
    {
        private Point pi;

        public EditScriptForm()
        {
            InitializeComponent();
        }

        private void EditScriptForm_Load(object sender, EventArgs e)
        {
            this.treeViewFiles.Nodes.Clear();

            TreeNode systemNode = this.treeViewFiles.Nodes.Add("系统脚本");
            TreeNode userNode = this.treeViewFiles.Nodes.Add("用户脚本");
            systemNode.Nodes.Add(new TreeNode("全局配置", 0, 0));
            systemNode.Expand();
            userNode.Nodes.Add(new TreeNode("main.txt", 0, 0));
            userNode.Nodes.Add(new TreeNode("sample3.txt", 0, 0));
            userNode.Nodes.Add(new TreeNode("start.txt", 0, 0));
            userNode.Expand();
        }

        private void treeViewFiles_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = this.treeViewFiles.GetNodeAt(pi);   
            if (pi.X >= node.Bounds.Left && 
                pi.X <= node.Bounds.Right && 
                node.Nodes.Count == 0 &&
                node.Parent != null &&
                node.Parent.Text == "用户脚本")   
            {   
                Debug.WriteLine("double click : " + node.Text);
                ParseScript("assets/data/" + node.Text);
            }
        }

        private void treeViewFiles_MouseDown(object sender, MouseEventArgs e)
        {
            this.pi = new Point(e.X, e.Y);   
        }

        private void ParseScript(string filename)
        {
            string text = "";
            if (File.Exists(filename))
            {
                try
                {
                    text = File.ReadAllText(filename, Encoding.UTF8);
                }
                finally
                {

                }
            }
            this.richTextBoxContent.Text = text;

            ClearHighlights();
            Highlight();
        }

        private void Highlight()
        {
            SetHighlight(0, 10, Color.Red);
        }

        public void SetHighlight(int startIndex, int length, Color color)
        {
            int selectionStart = this.richTextBoxContent.SelectionStart;
            int selectionLength = this.richTextBoxContent.SelectionLength;

            this.richTextBoxContent.SelectionStart = startIndex;
            this.richTextBoxContent.SelectionLength = length;
            this.richTextBoxContent.SelectionColor = color;
            
            this.richTextBoxContent.SelectionStart = selectionStart;
            this.richTextBoxContent.SelectionLength = selectionLength;
        }

        public void ClearHighlights()
        {
            int selectionStart = this.richTextBoxContent.SelectionStart;
            int selectionLength = this.richTextBoxContent.SelectionLength;

            this.richTextBoxContent.SelectionStart = 0;
            this.richTextBoxContent.SelectionLength = this.richTextBoxContent.TextLength;
            this.richTextBoxContent.SelectionColor = this.richTextBoxContent.ForeColor;
            this.richTextBoxContent.SelectionBackColor = this.richTextBoxContent.BackColor;
            this.richTextBoxContent.SelectionFont = this.richTextBoxContent.Font;
        
            this.richTextBoxContent.SelectionStart = selectionStart;
            this.richTextBoxContent.SelectionLength = selectionLength;
        }

        private void richTextBoxContent_TextChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine("richTextBoxContent_TextChanged");
            Highlight();
        }
    }
}
