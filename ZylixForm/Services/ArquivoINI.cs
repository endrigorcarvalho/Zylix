using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using ZylixForm.Entities.Exceptions;

namespace ZylixForm.Services
{
    public class ArquivoINI : Arquivo
    {



        private INIFile IniFile;


        public ArquivoINI(string pathArquivo) : base(pathArquivo)
        {


            string path = string.Format("{0}\\Config.ini", Directory.GetCurrentDirectory());
            PathArquivoINI = path;

            IniFile = new INIFile(path);

            if (!File.Exists(path))
            {
                try
                {
                    path = string.Format("{0}{1}Files{1}FileCSV.csv", Directory.GetCurrentDirectory(), Path.AltDirectorySeparatorChar);
                    IniFile.IniWriteValue("Configuration", "Path File CSV", path);

                    path = string.Format("{0}{1}Files{1}FileXML.xml", Directory.GetCurrentDirectory(), Path.AltDirectorySeparatorChar);
                    IniFile.IniWriteValue("Configuration", "Path File XML", path);
                }
                catch (Exception e)
                {
                    throw new DomainException(string.Format("Erro ao criar arquivo INI. \nErro: {0}", e.Message));
                }

            }





        }
        public override void GravarArquivo(object objeto)
        {
            if (!(objeto is string[]))
            {
                throw new DomainException(string.Format("Erro ao ler objeto. \n Erro: Function GravarArquivo() \\ Class ArquivoINI "));
            }
            string[] pathArquivos = (string[])objeto;
            try
            {
                PathArquivoXML = pathArquivos[0];
                PathArquivoCSV = pathArquivos[1];

                IniFile.IniWriteValue("Configuration", "Path File XML", PathArquivoXML);
                IniFile.IniWriteValue("Configuration", "Path File CSV", PathArquivoCSV);
            }
            catch (Exception e)
            {
                throw new DomainException(string.Format("Erro ao gravar arquivo INI. \nErro: {0}", e.Message));
            }

        }

        public override object LerArquivo()
        {
            string[] pathArquivos = new string[2];

            if (!File.Exists(PathArquivoINI))
            {
                throw new DomainException(string.Format("Arquivo INI não encontrado. \n Erro: Function LerArquivo() \\ Class ArquivoINI "));
            }

            try
            {
                PathArquivoXML = pathArquivos[0] = IniFile.IniReadValue("Configuration", "Path File XML");
                PathArquivoCSV = pathArquivos[1] = IniFile.IniReadValue("Configuration", "Path File CSV");


            }
            catch (Exception e)
            {
                throw new DomainException(string.Format("Erro ao ler arquivo INI. \nErro: {0}", e.Message));
            }
            return pathArquivos;

        }

        public string getArquivoXML()        
        {
            return PathArquivoXML;
        }
    }
}
