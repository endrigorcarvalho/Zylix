using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZylixForm.Entities.Exceptions;

namespace ZylixForm.Services
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
            if(!File.Exists(PathArquivo))
            {
                throw new DomainException(string.Format("Arquivo XML não encontrado. \nErro: Function LerArquivo() \\ Class ArquivoXML"));
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(PathArquivo);

                return xmlDocument;
            }
            catch(Exception e)
            {
                throw new DomainException(string.Format("Error to load XML file. \nError: {0}", e.Message));
            }
            
        }
    }
}
