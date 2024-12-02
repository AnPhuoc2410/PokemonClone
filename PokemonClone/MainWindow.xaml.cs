// MainWindow.xaml.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PokemonGame
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BASE_URL = "https://pokeapi.co/api/v2/";
        private Pokemon currentPokemon;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void FindPokemon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                int pokemonId = random.Next(1, 152);
                currentPokemon = await GetPokemonAsync(pokemonId);

                // Update UI
                PokemonName.Text = currentPokemon.Name.ToUpper();
                PokemonSprite.Source = new BitmapImage(new Uri(currentPokemon.Sprites.FrontDefault));
                PokemonInfo.Text = $"Height: {currentPokemon.Height / 10.0}m\nWeight: {currentPokemon.Weight / 10.0}kg";

                BattleControls.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task<Pokemon> GetPokemonAsync(int id)
        {
            string response = await client.GetStringAsync($"{BASE_URL}pokemon/{id}");
            return JsonSerializer.Deserialize<Pokemon>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    public class Pokemon
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Sprites Sprites { get; set; }
    }

    public class Sprites
    {
        public string FrontDefault { get; set; }
    }
}