using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ZylixForm.Entities.Exceptions;

namespace ZylixForm.Entities
{
     public class ListaItemConfiguracao
    {
        public List<ItemConfiguracao> ListaConfiguracao = new List<ItemConfiguracao>();

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

        public void CarregarListaDoArquivo(Func<object> funcaoLeituraArquivo)
        {

            object obJValores = funcaoLeituraArquivo.Invoke();

            if(!(obJValores is List<ItemConfiguracao>))
            {
                throw new DomainException(string.Format("Erro ao ler objeto. \nErro: Function CarregarListaDoArquivo() \\ Class ListaItemConfiguracao "));
            }

            try
            {
                List<ItemConfiguracao> valores = (List<ItemConfiguracao>)obJValores;

                ListaConfiguracao.Clear();
                foreach (var item in valores)
                {
                    ListaConfiguracao.Add(item);
                }
            }
            catch(Exception e)
            {
                throw new DomainException(string.Format("Erro ao carregar lista do arquivo CSV. Favor verificar conteúdo do arquivo CSV.\nErro: {0}", e.Message));
            }
            
        }
        

        public ItemConfiguracao GetItemConfiguracaoPorKey(int id)
        {
            return ListaConfiguracao.Where(p => p.Id == id).First();
        }

        public void UpdateItemConfiguracao(int id, string description, string value, string commments)
        {
            ItemConfiguracao item = ListaConfiguracao.Where(p => p.Id == id).First();
            if(item == null)
            {
                throw new DomainException(string.Format("Erro ao fazer update do ID:{0} na lista. \nErro: Function UpdateItemConfiguracao() \\ Class ListaItemConfiguracao ", id));
            }

            item.Description = description;
            item.Value = value;
            item.Comments = commments;
        }

       
        
    }
}
