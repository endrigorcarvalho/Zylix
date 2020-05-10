using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;


namespace ZylixForm.Services
{
    public class ArquivoINI : Arquivo
    {

        public string PathArquivoXML { get; set; }
        public string PathArquivoCSV { get; set; }

        private INIFile IniFile;

        
        public ArquivoINI(string pathArquivo) :base(pathArquivo)
        {


            string path = string.Format("{0}{1}Files{1}Config.ini", Directory.GetCurrentDirectory(), Path.AltDirectorySeparatorChar);
            
            if(!Directory.Exists(path))
            {
                IniFile = new INIFile(path);

                path = string.Format("{0}{1}Files{1}FileCSV.csv", Directory.GetCurrentDirectory(), Path.AltDirectorySeparatorChar);
                IniFile.IniWriteValue("Configuration", "Path File CSV", path);

                path = string.Format("{0}{1}Files{1}FileXML.xml", Directory.GetCurrentDirectory(), Path.AltDirectorySeparatorChar);
                IniFile.IniWriteValue("Configuration", "Path File XML", path);
            }

            
        }
        public override void GravarArquivo(object objeto)
        {
            if(!(objeto is string[]))
            {
                return;
            }
            string[] pathArquivos = (string[])objeto;

            PathArquivoXML = pathArquivos[0];
            PathArquivoCSV = pathArquivos[1];
            
            IniFile.IniWriteValue("Configuration", "Path File XML", PathArquivoXML);
            IniFile.IniWriteValue("Configuration", "Path File CSV", PathArquivoCSV);
        }

        public override object LerArquivo()
        {
            string[] pathArquivos = new string [2];
            PathArquivoXML = pathArquivos[0] = IniFile.IniReadValue("Configuration", "Path File XML");
            PathArquivoCSV = pathArquivos[1] = IniFile.IniReadValue("Configuration", "Path File CSV");

            return pathArquivos;

        }
    }
}
