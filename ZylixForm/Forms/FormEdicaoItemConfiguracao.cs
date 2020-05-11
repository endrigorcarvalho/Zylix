using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZylixForm.Components;
using ZylixForm.Entities;

namespace ZylixForm.Forms
{
    /// <summary>
    /// Form para edição dos itens contido no arquivo CSV e visualizado na ListView.
    /// Quando o item é alterado, a listView é alterada e também é feito a gravação no arquivo CSV.
    /// </summary>
    public partial class FormEdicaoItemConfiguracao : Form
    {

        public Action<int, string, string, string> FuncaoUpdateLista { get; set; }

        public Action FuncaoUpdateListView { get; set; }

        public Action FuncaoGravaArquivoCSV { get; set; }
        /*
        public FormEdicaoItemConfiguracao()
        {
            InitializeComponent();
        }*/

        /// <summary>
        /// Sobrecarga do construtor recebendo os dados a serem atualizados e os delegates para updates e gravação do arquivo CSV.
        /// </summary>
        /// <param name="id">Id do item a ser alterado</param>
        /// <param name="description">Description do item a ser alterado</param>
        /// <param name="value">Value do item a ser alterado</param>
        /// <param name="comments">Comments do item a ser alterado</param>
        /// <param name="funcaoUpdateLista">Delegate com a função para fazer o Update da lista da classe ListaItemConfiguracao</param>
        /// <param name="funcaoUpdateListView">Delegate com a função para fazer o Update da ListView do Form </param>
        /// <param name="funcaoGravaArquivoCSV">Delegate com a função para fazer a gravação do arquivo XML</param>
        public FormEdicaoItemConfiguracao(int id, string description, string value, string comments, Action<int, string, string, string> funcaoUpdateLista, Action funcaoUpdateListView, Action funcaoGravaArquivoCSV)
        {
            InitializeComponent();

            FuncaoUpdateLista = funcaoUpdateLista;
            FuncaoUpdateListView = funcaoUpdateListView;
            FuncaoGravaArquivoCSV = funcaoGravaArquivoCSV;


            LabelId.Text = id.ToString();
            tbDescription.Text = description;
            tbValue.Text =  value;
            tbComments.Text = comments;

            this.Visible = true;

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Invoca os delegates para updates e gravação do arquivo CSV recebidos no construtor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            try 
            {
                FuncaoUpdateLista.Invoke(int.Parse(LabelId.Text), tbDescription.Text, tbValue.Text, tbComments.Text);
                FuncaoUpdateListView.Invoke();
                FuncaoGravaArquivoCSV.Invoke();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }



    }
}
