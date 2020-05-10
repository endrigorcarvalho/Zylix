using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using ZylixForm.Entities;

namespace ZylixForm.Services
{
    class ArquivoCSV : Arquivo
    {
                
        public ArquivoCSV(string pathArquivo):base(pathArquivo)
        {
            
        }
  
        public override object LerArquivo()
        {
            List<ItemConfiguracao> list = new List<ItemConfiguracao>();

            using (StreamReader stream = new StreamReader(PathArquivo))
            {
                while(!stream.EndOfStream)
                {
                    string[] value = stream.ReadLine().Split(',');
                    list.Add(new ItemConfiguracao(int.Parse(value[0]), value[1], value[2], value[3], value[4]));
                }

                return list;
            }
        }

        public override void GravarArquivo(object objeto)
        {
            
            if (!(objeto is ListaItemConfiguracao))
            {
                return;
            }

            ListaItemConfiguracao list = (ListaItemConfiguracao)objeto;

            File.Delete(PathArquivo);

            using (StreamWriter stream = File.AppendText(PathArquivo))
            {
                foreach (var item in list.ListaConfiguracao)
                {
                    stream.WriteLine(string.Format("{0},{1},{2},{3},{4}", item.Id, item.Description, item.Value, item.Comments, item.Key));
                }
            }
        }
    }
}
