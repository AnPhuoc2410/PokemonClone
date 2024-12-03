using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonClone.Entities
{
    public class Types
    {
        public int Slot { get; set; }
        public Type Type{ get; set; }
    }
    public class Type
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }


}
