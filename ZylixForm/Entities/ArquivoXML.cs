using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZylixForm.Entities
{
    public class ArquivoXML : Arquivo
    {
        
        public ArquivoXML(string pathArquivo):base(pathArquivo)
        {            
        }
        public override void GravarArquivo(object objeto)
        {
            throw new NotImplementedException();
        }

        public override object LerArquivo()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(PathArquivo);

            return xmlDocument;
        }
    }
}
