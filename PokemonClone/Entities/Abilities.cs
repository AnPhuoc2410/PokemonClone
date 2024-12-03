using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonClone.Entities
{
    public class Abilities
    {
        public Ability Ability { get; set; }
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }
        public int Slot { get; set; }
    }
    public class Ability
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
