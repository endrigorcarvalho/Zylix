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
    /// <summary>
    ///     O Software funciona basicamente utiliza 3 arquivos padrões, tendo objetivo de possibilitar as alterações de valores sem a necessidade de alteração do software
    ///         - Arquivo .Ini - Arquivo contendo os endereços do arquivo .CSV e .XMV. O nome deve ser "Config.ini"
    ///         - Arquivo .XML - Arquivo .XML com os nodos mostrado para o usuário na treeView. Cada nodo deve conter a chave "Value" que será o nome mostrado na treeView e a chave "Tag" que será o valor relacionado com o item do arquivo .CSV
    ///         - Arquivo .CSV - Arquivo .CSV com os itens mostrado na ListView. O Arquivo Obrigatoriamente dever ter os seguintes valores: ID, DESCRIPTION, VALUE, COMMENTS E TAG. O valor da "TAG" deverá ser o mesmo valor na chave "Tag" do arquivo .XML para poderem ser relacionados.
    ///
    ///     O software inicia lendo o arquivo .Ini e pegando os endereços dos arquivos .CSV e .XML. Após ele faz a carga da TreeView (treeView1) com dados do arquivo .Xml e do ListView(listView1) com os dados do arquivo .CSV.
    ///     Quando o usuário clicar no nodo da TreeView, deve se mostrar os dados relacionados através da chave "Tag" na ListView.
    ///     Quando o usuário clicar em um item da ListView, irá abrir uma janela que possibilitará a alteração dos dados e posterior gravação da alteração no arquivo .CSV
    ///     Quando o usuário clicar no menu File -> Load, deve-se abrir uma janela para o usuário localizar o arquivo .CSV que desejar e após esse endereço é gravado no arquivo .Ini
    /// 
    /// </summary>
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

        /// <summary>
        /// Responsável por carregar os arquivos .Ini e .XML e .CSV e posteriormente carregar a TreeView e ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Adicionar um nodo na TreeView
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="trv"></param>
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

        /// <summary>
        /// Função recursiva que faz a leitura do arquivo .XML e popula a TreeView.
        /// </summary>
        /// <param name="parent_nodes"></param>
        /// <param name="xml_node"></param>
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

        /// <summary>
        /// Função chamada depois do usuário selecionar um nodo da treeView. Chama a função da classe de controle 
        /// (ListViewItemConfiguracao.adicionarListaItemConfiguracao()) passando o valor da Tag do nodo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Função chamada quando um item da listView é clicado. A função abre uma janela para alteração dos valores do item selecionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            loadFileToolStripMenuItem_Click(sender, e);
        }
    }
}
