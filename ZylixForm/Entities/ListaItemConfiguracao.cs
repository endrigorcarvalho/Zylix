using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ZylixForm.Entities.Exceptions;

namespace ZylixForm.Entities
{
    /// <summary>
    /// Classe contendo a lista com os itens do arquivo CSV.
    /// </summary>
     public class ListaItemConfiguracao
    {
        public List<ItemConfiguracao> ListaConfiguracao = new List<ItemConfiguracao>();

        public void AdicionarLista(ItemConfiguracao item)
        {
    
            ListaConfiguracao.Add(item);

        }

        /// <summary>
        /// Remove da Lista
        /// </summary>
        /// <param name="item"></param>
        public void RemoverLista(ItemConfiguracao item)
        {
            ListaConfiguracao.Remove(item);
        }

        /// <summary>
        /// Limpa Lista
        /// </summary>
        public void LimpaLista()
        {
            ListaConfiguracao.Clear();
        }

        /// <summary>
        /// Retorna lista contendo itens que contém o valor da tag recebida.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<ItemConfiguracao> PesquisaPorTag(string tag)
        {        
            return ListaConfiguracao.Where(p => p.Tag == tag).ToList();
        }

        /// <summary>
        /// Carrega a lista com os itens recebidos da função Leitura de Arquivo CSV (Delegate).
        /// </summary>
        /// <param name="funcaoLeituraArquivo">Delegate com a função de leitura do arquivo CSV. Função deve retornar um tipo List<ItemConfiguracao></param>
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

        /// <summary>
        /// Retorna um item da lista por ID
        /// </summary>
        /// <param name="id">Id do item</param>
        /// <returns>objeto ItemConfiguracao</returns>
        public ItemConfiguracao GetItemConfiguracaoPorId(int id)
        {
            return ListaConfiguracao.Where(p => p.Id == id).First();
        }

        /// <summary>
        /// Update do item da lista localizado por Id
        /// </summary>
        /// <param name="id">Id do ItemConfiguracao</param>
        /// <param name="description">Description do ItemConfiguracao</param>
        /// <param name="value">Value do ItemConfiguracao</param>
        /// <param name="commments">Comments do ItemConfiguracao</param>
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
