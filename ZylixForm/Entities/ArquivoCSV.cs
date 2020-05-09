using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ZylixForm.Entities
{
    class ArquivoCSV : Arquivo
    {


        public ArquivoCSV(string pathArquivo)
        {
            PathArquivo = pathArquivo;
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



    }
}
