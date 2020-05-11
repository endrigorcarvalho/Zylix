using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using ZylixForm.Entities;
using ZylixForm.Entities.Exceptions;

namespace ZylixForm.Services
{
    /// <summary>
    /// Classe para manipulação de arquivos CSV
    /// </summary>
    class ArquivoCSV : Arquivo
    {
                
        public ArquivoCSV(string pathArquivo):base(pathArquivo)
        {
            
        }

        /// <summary>
        /// Leitura de arquivos CSV.
        /// </summary>
        /// <returns>Lista do tipo List<ItemConfiguracao> com conteúdo do arquivo CSV </returns>
        public override object LerArquivo()
        {
            List<ItemConfiguracao> list = new List<ItemConfiguracao>();

            if(! File.Exists(PathArquivo))
            {
                throw new DomainException(string.Format("Arquivo CSV não encontrado. \n Erro: Function LerArquivo() \\ Class ArquivoCSV "));
            }

            try
            {
                using (StreamReader stream = new StreamReader(PathArquivo))
                {
                    while (!stream.EndOfStream)
                    {
                        string[] value = stream.ReadLine().Split(',');
                        list.Add(new ItemConfiguracao(int.Parse(value[0]), value[1], value[2], value[3], value[4]));
                    }

                    stream.Close();
                    return list;
                }
            }
            catch(Exception e)
            {
                throw new DomainException(string.Format("Erro ao ler arquivo CSV. \nErro: {0}", e.Message));
            }
                        
        }

        /// <summary>
        /// Grava arquivo CSV.
        /// </summary>
        /// <param name="objeto">Objeto contendo o tipo ListaItemConfiguracao.</param>
        public override void GravarArquivo(object objeto)
        {
            
            if (!(objeto is ListaItemConfiguracao))
            {
                throw new DomainException(string.Format("Erro ao ler objeto. \n Erro: Function GravarArquivo() \\ Class ArquivoCSV "));
            }

            ListaItemConfiguracao list = (ListaItemConfiguracao)objeto;

            try
            {
                File.Delete(PathArquivo);
            }
            catch(Exception e)
            {
                throw new DomainException(string.Format("Erro ao apagar arquivo CSV. \nErro: {0}", e.Message));
            }

            try
            {
                using (StreamWriter stream = File.AppendText(PathArquivo))
                {
                    foreach (var item in list.ListaConfiguracao)
                    {
                        stream.WriteLine(string.Format("{0},{1},{2},{3},{4}", item.Id, item.Description, item.Value, item.Comments, item.Tag));
                    }

                    stream.Close();
                }
            }
            catch(Exception e)
            {
                throw new DomainException(string.Format("Erro ao gravar arquivo CSV. \nErro: {0}", e.Message));
            }

        }
    }
}
