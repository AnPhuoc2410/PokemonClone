using PokemonClone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonClone.Services
{
    public class PokeClient
    {
        public HttpClient Client { get; set; }

        public PokeClient(HttpClient client)
        {
            this.Client = client;
        }
        public async Task<Pokemon> GetPokemon(string id)
        {
            try
            {
                var response = await Client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: Unable to fetch Pokémon. Status Code: {response.StatusCode}");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<Pokemon>(content, options);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return null;
            }
        }

    }
}
