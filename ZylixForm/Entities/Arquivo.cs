using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZylixForm.Entities
{
    public abstract class Arquivo
    {
        public string PathArquivo { get; set; }
        public abstract object LerArquivo();
        
    }
}
