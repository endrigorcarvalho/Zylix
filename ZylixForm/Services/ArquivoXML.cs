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
    /// <summary>
    /// Classe para manipulação de arquivos XML
    /// </summary>
    public class ArquivoXML : Arquivo
    {
        /// <summary>
        /// Construtor da classe ArquivoXML
        /// </summary>
        /// <param name="pathArquivo">Path do arquivo XML</param>
        public ArquivoXML(string pathArquivo):base(pathArquivo)
        {            
        }
        /// <summary>
        /// Classe não implementada
        /// </summary>
        /// <param name="objeto"></param>
        public override void GravarArquivo(object objeto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Leitura do arquivo XML.
        /// </summary>
        /// <returns>xmlDocument com o conteúdo do arquivo XML</returns>
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
                throw new DomainException(string.Format("Erro na leitura do arquivo XML. \nError: {0}", e.Message));
            }
            
        }
    }
}
