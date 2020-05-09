using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZylixForm.Entities
{
    class ItemConfiguracao
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Comments { get; set; }
        public string Key { get; set; }

        public ItemConfiguracao(int id, string description, string value, string comments, string key)
        {
            Id = id;
            Description = description;
            Value = value;
            Comments = comments;
            Key = key;
        }
    }
}
