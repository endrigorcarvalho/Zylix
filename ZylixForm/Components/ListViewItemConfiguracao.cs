using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZylixForm.Entities;

namespace ZylixForm.Components
{
    /// <summary>
    /// Classe para manipulação da ListView de configuração contendo os dados do arquivo CSV.
    /// </summary>
    class ListViewItemConfiguracao
    {
        public ListView ListViewItem{ get; set; }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="listViewItem">Objeto ListView que será manipulado</param>
        public ListViewItemConfiguracao(ListView listViewItem)
        {
            ListViewItem = listViewItem;
        }

        /// <summary>
        /// Mostra para o usuário todos os itens da lista de objetos do arquivo CSV.
        /// Adiciona itens a ListView com os itens do objeto ListaItemConfiguracao
        /// </summary>
        /// <param name="lista">Objeto ListaItemConfiguracao contendo a lista de ItemConfiguracao</param>
        public void adicionarListaItemConfiguracao(ListaItemConfiguracao lista)
        {
            ListViewItem.Items.Clear();
            foreach (var item in lista.ListaConfiguracao)
            {
                ListViewItem listItem = new ListViewItem(item.Id.ToString());
                listItem.SubItems.Add(item.Description);
                listItem.SubItems.Add(item.Value);
                listItem.SubItems.Add(item.Comments);

                ListViewItem.Items.Add(listItem);

            }
        }

        /// <summary>
        /// Mostra para o usuário todos os itens da lista de objetos do arquivo CSV filtrados de Key recebida.
        /// Adiciona itens a ListView com os itens do objeto ListaItemConfiguracao filtrados de Key recebida.
        /// </summary>
        /// <param name="lista">Objeto ListaItemConfiguracao contendo a lista de ItemConfiguracao</param>
        /// <param name="key">Key relacionando os itens do Arquivo XML com CSV</param>
        public void adicionarListaItemConfiguracao(ListaItemConfiguracao lista, string key)
        {
            ListViewItem.Items.Clear();
            foreach(var item in lista.PesquisaPorTag(key))
            {
                ListViewItem listItem = new ListViewItem(item.Id.ToString());
                listItem.SubItems.Add(item.Description);
                listItem.SubItems.Add(item.Value);
                listItem.SubItems.Add(item.Comments);

                ListViewItem.Items.Add(listItem);

            }
        }

        /// <summary>
        /// Limpa ListView
        /// </summary>
        public void limparListaItemConfiguracao()
        {
            ListViewItem.Items.Clear();
        }
    }
}
