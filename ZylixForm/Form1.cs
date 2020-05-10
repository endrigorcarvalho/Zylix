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
using ZylixForm.Entities.Exceptions;
using ZylixForm.Components;
using ZylixForm.Forms;
using ZylixForm.Services;
using System.IO;

namespace ZylixForm
{
    public partial class Form1 : Form
    {
        private ListaItemConfiguracao _listaItemConfiguracao = new ListaItemConfiguracao();
        private Arquivo _arquivoIni;
        private Arquivo _arquivoCSV;
        private Arquivo _arquivoXML;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                _arquivoIni = new ArquivoINI(null);
                object pathFilesObject = _arquivoIni.LerArquivo();

                if (!(pathFilesObject is string[]))
                {
                    throw new DomainException(string.Format("Erro ao ler objeto. Erro: Function Form1_Load() \\ Class Form1 "));
                }


                string[] pathFile = (string[])pathFilesObject;

                _arquivoXML = new ArquivoXML(pathFile[0]);
                loadTreeView((XmlDocument)_arquivoXML.LerArquivo(), treeView1);

                _arquivoCSV = new ArquivoCSV(pathFile[1]);
                _listaItemConfiguracao.CarregarListaDoArquivo(_arquivoCSV.LerArquivo);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message,"Erro", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void loadTreeView(XmlDocument xmlDoc, TreeView trv)
        {
            try
            {
                trv.Nodes.Clear();
                AddTreeViewNode(trv.Nodes, xmlDoc.DocumentElement);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddTreeViewNode(TreeNodeCollection parent_nodes, XmlNode xml_node)
        {
            try
            {
                if (xml_node.Attributes.GetNamedItem("Value") == null)
                {
                    throw new DomainException("Erro na leitura do arquivo XML. Favor verificar o arquivo XML. \nErro: Função AddTreeViewNode() \\ Classe: Form1");
                } 
                
                string valor = xml_node.Attributes.GetNamedItem("Value").InnerText;
                string tag = xml_node.Attributes.GetNamedItem("Tag").InnerText;

                TreeNode new_node = parent_nodes.Add(valor);
                new_node.Tag = tag;

                foreach (XmlNode child_node in xml_node.ChildNodes)
                {
                    AddTreeViewNode(new_node.Nodes, child_node);
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string text = e.Node.Text;

                if (!(e.Node.Tag is string))
                {
                    throw new DomainException(string.Format("Erro ao ler String. Erro: Function treeView1_AfterSelect() \\ Class Form1 "));
                }
                string tag = (string)e.Node.Tag;

                ListViewItemConfiguracao list = new ListViewItemConfiguracao(listView1);
                if (!string.IsNullOrEmpty(tag))
                {
                    list.adicionarListaItemConfiguracao(_listaItemConfiguracao, tag);
                }
                else
                {
                    list.limparListaItemConfiguracao();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                string id = listView1.SelectedItems[0].SubItems[0].Text;
                string description = listView1.SelectedItems[0].SubItems[1].Text;
                string value = listView1.SelectedItems[0].SubItems[2].Text;
                string comments = listView1.SelectedItems[0].SubItems[3].Text;

                FormEdicaoItemConfiguracao formEdicao = new FormEdicaoItemConfiguracao(int.Parse(id), description, value, comments, _listaItemConfiguracao.UpdateItemConfiguracao, UpdateListView, GravaArquivoCSV);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GravaArquivoCSV()
        {
            try
            {
                _arquivoCSV.GravarArquivo(_listaItemConfiguracao);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UpdateListView()
        {
            try
            {
                ListViewItemConfiguracao listViewItemConfiguracao = new ListViewItemConfiguracao(listView1);
                listViewItemConfiguracao.adicionarListaItemConfiguracao(_listaItemConfiguracao);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = string.Format("{0}\\Files", Directory.GetCurrentDirectory());
                    openFileDialog.Filter = "csv files (*.csv)|*.csv";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        string filePathCSV = openFileDialog.FileName;

                        string[] filePath = new string[2];
                        filePath = (string[])_arquivoIni.LerArquivo();

                        filePath[1] = filePathCSV;
                        _arquivoIni.GravarArquivo(filePath);

                        Arquivo arquivoCSV = new ArquivoCSV(filePath[1]);
                        _listaItemConfiguracao.CarregarListaDoArquivo(arquivoCSV.LerArquivo);

                        treeView1.TopNode.Collapse(true);

                        ListViewItemConfiguracao list = new ListViewItemConfiguracao(listView1);
                        list.limparListaItemConfiguracao();

                    }
                } 
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
