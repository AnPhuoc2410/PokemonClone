using PokemonClone.Entities;
using PokemonClone.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PokemonClone
{
    public partial class MainWindow : Window
    {
        private PokeClient _pokeClient;
        private Pokemon _playerPokemon;
        private Pokemon _opponentPokemon;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the Pokémon API client with HttpClient
            _pokeClient = new PokeClient(new HttpClient());

            // Load Pokémon data dynamically
            LoadBattle();
        }

        private async void LoadBattle()
        {
            // Fetch player Pokémon data
            _playerPokemon = await _pokeClient.GetPokemon("pikachu");
            UpdatePlayerPokemonUI();

            // Fetch opponent Pokémon data
            _opponentPokemon = await _pokeClient.GetPokemon("bulbasaur");
            UpdateOpponentPokemonUI();
        }

        private void UpdatePlayerPokemonUI()
        {
            if (_playerPokemon != null)
            {
                PlayerPokemonName.Text = _playerPokemon.Name.ToUpper();
                PlayerLevel.Text = _playerPokemon.Level.ToString();
                PlayerHPBar.Maximum = CalculateMaxHP(_playerPokemon);
                PlayerHPBar.Value = PlayerHPBar.Maximum;
                PlayerHPText.Text = $"HP: {PlayerHPBar.Value}/{PlayerHPBar.Maximum}";
                PlayerSprite.Source = new BitmapImage(new Uri(_playerPokemon.Sprites.FrontDefault));

                var moves = _playerPokemon.Moves.Take(4).Select(m => m.Move.Name).ToList();
                UpdateMoveButtons(moves);
            }
        }

        private void UpdateOpponentPokemonUI()
        {
            if (_opponentPokemon != null)
            {
                OpponentPokemonName.Text = _opponentPokemon.Name.ToUpper();
                OpponentLevel.Text = _opponentPokemon.Level.ToString();
                OpponentHPBar.Maximum = CalculateMaxHP(_opponentPokemon);
                OpponentHPBar.Value = OpponentHPBar.Maximum;
                OpponentHPText.Text = $"HP: {OpponentHPBar.Value}/{OpponentHPBar.Maximum}";
                OpponentSprite.Source = new BitmapImage(new Uri(_opponentPokemon.Sprites.FrontDefault));
            }
        }

        private void UpdateMoveButtons(System.Collections.Generic.List<string> moves)
        {
            // Dynamically update the move buttons with the player's Pokémon's moves
            if (moves.Count > 0) Move1Button.Content = moves[0];
            if (moves.Count > 1) Move2Button.Content = moves[1];
            if (moves.Count > 2) Move3Button.Content = moves[2];
            if (moves.Count > 3) Move4Button.Content = moves[3];
        }

        private int CalculateMaxHP(Pokemon pokemon)
        {
            var hpStat = pokemon.Stats.FirstOrDefault(s => s.Stat.Name == "hp")?.BaseStat ?? 0;
            return (int)((hpStat * 2 * pokemon.Level) / 100) + pokemon.Level + 10;
        }

        private async void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Example of a basic attack mechanism
            if (_opponentPokemon != null)
            {
                var damage = new Random().Next(10, 20);
                OpponentHPBar.Value -= damage;
                OpponentHPText.Text = $"HP: {OpponentHPBar.Value}/{OpponentHPBar.Maximum}";

                if (OpponentHPBar.Value <= 0)
                {
                    MessageBox.Show($"{_playerPokemon.Name} wins!");
                     LoadBattle(); // Restart the battle
                }
            }
        }
    }
}