using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ZylixForm.Entities
{
    class ListaItemConfiguracao
    {
        List<ItemConfiguracao> ListaConfiguracao = new List<ItemConfiguracao>();

        public void AdicionarLista(ItemConfiguracao item)
        {
            ListaConfiguracao.Add(item);
        }


        public void RemoverLista(ItemConfiguracao item)
        {
            ListaConfiguracao.Remove(item);
        }

        public void LimpaLista()
        {
            ListaConfiguracao.Clear();
        }

        public List<ItemConfiguracao> PesquisaPorKey(string key)
        {            
            return ListaConfiguracao.Where(p => p.Key == key).ToList();
        }

        public void CarregarListaDoArquivo(Func<List<ItemConfiguracao>> funcaoLeituraArquivo)
        {
            List<ItemConfiguracao> valores = funcaoLeituraArquivo.Invoke();

            ListaConfiguracao.Clear();
            foreach(var item in valores)
            {
                ListaConfiguracao.Add(item);
            }
        

        }
        
        public void carregaExemplo()
        {
            ListaConfiguracao.Add(new ItemConfiguracao(ListaConfiguracao.Count, string.Format("Description {0}", ListaConfiguracao.Count), string.Format("Value {0}", ListaConfiguracao.Count), string.Format("comments {0}", ListaConfiguracao.Count), "0"));
            ListaConfiguracao.Add(new ItemConfiguracao(ListaConfiguracao.Count, string.Format("Description {0}", ListaConfiguracao.Count), string.Format("Value {0}", ListaConfiguracao.Count), string.Format("comments {0}", ListaConfiguracao.Count), "0"));
            ListaConfiguracao.Add(new ItemConfiguracao(ListaConfiguracao.Count, string.Format("Description {0}", ListaConfiguracao.Count), string.Format("Value {0}", ListaConfiguracao.Count), string.Format("comments {0}", ListaConfiguracao.Count), "1"));
            ListaConfiguracao.Add(new ItemConfiguracao(ListaConfiguracao.Count, string.Format("Description {0}", ListaConfiguracao.Count), string.Format("Value {0}", ListaConfiguracao.Count), string.Format("comments {0}", ListaConfiguracao.Count), "1"));

        }
       
        
    }
}
