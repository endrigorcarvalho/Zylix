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

    public partial class FormEdicaoItemConfiguracao : Form
    {

        public Action<int, string, string, string> FuncaoUpdateLista { get; set; }

        public Action FuncaoUpdateListView { get; set; }

        public Action FuncaoGravaArquivoCSV { get; set; }

        public FormEdicaoItemConfiguracao()
        {
            InitializeComponent();
        }

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

        private void btOk_Click(object sender, EventArgs e)
        {
            FuncaoUpdateLista.Invoke(int.Parse(LabelId.Text), tbDescription.Text, tbValue.Text, tbComments.Text);
            FuncaoUpdateListView.Invoke();
            FuncaoGravaArquivoCSV.Invoke();





            this.Close();
        }

        public void getParametros(out int id)
        {
            id = int.Parse(LabelId.Text);
        }
        private void FormEdicaoItemConfiguracao_Load(object sender, EventArgs e)
        {

        }
    }
}
