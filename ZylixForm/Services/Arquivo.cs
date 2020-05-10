using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZylixForm.Services

{
    public abstract class Arquivo
    {
        public string PathArquivo { get; set; }

        protected Arquivo(string pathArquivo)
        {
            PathArquivo = pathArquivo;
        }

        public abstract object LerArquivo();
        public abstract void GravarArquivo(object objeto);


    }
}
