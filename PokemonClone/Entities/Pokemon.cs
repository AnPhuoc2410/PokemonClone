using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonClone.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        public List<Abilities> Abilities { get; set; }
        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }
        public int Height { get; set; }
        public List<Moves> Moves { get; set; }
        public string Name { get; set; }
        public Sprites Sprites { get; set; }
        public List<Stats> Stats { get; set; }
        public List<Types> Types { get; set; }
        public int Weight { get; set; }
        public int Level { get; set; } = 5; // Default level
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
    }
}
