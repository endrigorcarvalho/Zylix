﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

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
            List<ItemConfiguracao> valores = (List<ItemConfiguracao>)funcaoLeituraArquivo.Invoke();

           /* if (!funcaoLeituraArquivo.GetType().Equals(typeof(List<ItemConfiguracao>)))
            {
                return;
            }
            */
            

            ListaConfiguracao.Clear();
            foreach(var item in valores)
            {
                ListaConfiguracao.Add(item);
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
                return;
            }

            item.Description = description;
            item.Value = value;
            item.Comments = commments;
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
