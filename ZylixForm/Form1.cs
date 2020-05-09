using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ZylixForm
{
    public partial class Form1 : Form
    {
        private XmlDocument _xmlDocument;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(@"C:\Users\endri\OneDrive\Documentos\Visual Studio 2015\Projects\Icon_Zylix\Zylix\ZylixForm\ConfiguracaoInicial.xml");
            loadTreeView(_xmlDocument, treeView1);
           
        }

        private void loadTreeView(XmlDocument xmlDoc, TreeView trv)
        {
            trv.Nodes.Clear();
            AddTreeViewNode(trv.Nodes, xmlDoc.DocumentElement);
        }
        private void AddTreeViewNode(TreeNodeCollection parent_nodes, XmlNode xml_node)
        {
            if(xml_node.Attributes.GetNamedItem("Value") != null)
            {
                string valor = xml_node.Attributes.GetNamedItem("Value").InnerText;

                TreeNode new_node = parent_nodes.Add(valor);

                foreach (XmlNode child_node in xml_node.ChildNodes)
                {
                    AddTreeViewNode(new_node.Nodes, child_node);
                }
            }
            

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text = e.Node.Text;
        }
    }
}
