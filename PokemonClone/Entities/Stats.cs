using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonClone.Entities
{
    public class Stats
    {
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }
        public int Effort { get; set; }
        public Stat Stat { get; set; }
    }
    public class Stat
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }


}
