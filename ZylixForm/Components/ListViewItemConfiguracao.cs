using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZylixForm.Entities;

namespace ZylixForm.Components
{
    class ListViewItemConfiguracao
    {
        public ListView ListViewItem{ get; set; }


        public ListViewItemConfiguracao(ListView listViewItem)
        {
            ListViewItem = listViewItem;
        }

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

        public void adicionarListaItemConfiguracao(ListaItemConfiguracao lista, string key)
        {
            ListViewItem.Items.Clear();
            foreach(var item in lista.PesquisaPorKey(key))
            {
                ListViewItem listItem = new ListViewItem(item.Id.ToString());
                listItem.SubItems.Add(item.Description);
                listItem.SubItems.Add(item.Value);
                listItem.SubItems.Add(item.Comments);

                ListViewItem.Items.Add(listItem);

            }
        }

        public void limparListaItemConfiguracao()
        {
            ListViewItem.Items.Clear();
        }
    }
}
