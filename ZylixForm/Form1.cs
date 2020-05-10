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
using ZylixForm.Entities;
using ZylixForm.Components;
using ZylixForm.Forms;
using System.Security.Cryptography;

namespace ZylixForm
{
    public partial class Form1 : Form
    {
        private XmlDocument _xmlDocument;

        

        public Form1()
        {
            InitializeComponent();
        }

        ListaItemConfiguracao lista = new ListaItemConfiguracao();
        private void Form1_Load(object sender, EventArgs e)
        {


            ArquivoXML arquivoXML = new ArquivoXML(@"C:\Users\endri\OneDrive\Documentos\Visual Studio 2015\Projects\Icon_Zylix\Zylix\ZylixForm\ConfiguracaoInicial.xml");
            loadTreeView((XmlDocument)arquivoXML.LerArquivo(), treeView1);

            Arquivo arquivoCSV = new ArquivoCSV(@"C:\Users\endri\OneDrive\Documentos\Visual Studio 2015\Projects\Icon_Zylix\Zylix\ZylixForm\ArquivoCSV.csv");
            lista.CarregarListaDoArquivo(arquivoCSV.LerArquivo);

            
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
                string tag = xml_node.Attributes.GetNamedItem("Tag").InnerText;

                TreeNode new_node = parent_nodes.Add(valor);
                new_node.Tag = tag;

                foreach (XmlNode child_node in xml_node.ChildNodes)
                {
                    AddTreeViewNode(new_node.Nodes, child_node);
                }
            }
            

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text = e.Node.Text;
            
            if (!(e.Node.Tag is string))
            {
                return;
            }
            string tag = (string)e.Node.Tag;

            ListViewItemConfiguracao list = new ListViewItemConfiguracao(listView1);
            if (!string.IsNullOrEmpty(tag))
            {
                list.adicionarListaItemConfiguracao(lista, tag);
            }
            else
            {                                
                list.limparListaItemConfiguracao();
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ListViewItemConfiguracao list = new ListViewItemConfiguracao(listView1);
            list.adicionarListaItemConfiguracao(lista);
        }



        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            string id = listView1.SelectedItems[0].SubItems[0].Text; 
            string description = listView1.SelectedItems[0].SubItems[1].Text;
            string value = listView1.SelectedItems[0].SubItems[2].Text;
            string comments = listView1.SelectedItems[0].SubItems[3].Text;
                     

            FormEdicaoItemConfiguracao formEdicao = new FormEdicaoItemConfiguracao(int.Parse(id), description, value, comments, lista.UpdateItemConfiguracao, UpdateListView, GravaArquivoCSV);
                      

        }

        private void GravaArquivoCSV()
        {
            Arquivo arquivoCSV = new ArquivoCSV(@"C:\Users\endri\OneDrive\Documentos\Visual Studio 2015\Projects\Icon_Zylix\Zylix\ZylixForm\ArquivoCSV.csv");
            arquivoCSV.GravarArquivo(lista);
        }
        private void UpdateListView()
        {
            ListViewItemConfiguracao list = new ListViewItemConfiguracao(listView1);
            list.adicionarListaItemConfiguracao(lista);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ListViewItemConfiguracao list = new ListViewItemConfiguracao(listView1);
            list.adicionarListaItemConfiguracao(lista, "2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}
