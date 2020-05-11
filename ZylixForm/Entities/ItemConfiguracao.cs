using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZylixForm.Entities
{
    public class ItemConfiguracao
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Comments { get; set; }
        public string Tag { get; set; }

        public ItemConfiguracao(int id, string description, string value, string comments, string tag)
        {
            Id = id;
            Description = description;
            Value = value;
            Comments = comments;
            Tag = tag;
        }
    }
}
