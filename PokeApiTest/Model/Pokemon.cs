using PokeAPI;
using System.Net;

namespace PokeApiTest.Model {
    public class PokemonObj {
        public string Name { get; private set; } = String.Empty;
        public string Type { get; private set; } = String.Empty;
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int SpecialAttack { get; private set; }
        public int SpecialDefense { get; private set; }
        public int Speed { get; private set; }

        public async Task LoadPokemonFromApi(string PokemonName) {
            if(string.IsNullOrEmpty(PokemonName)) {
                throw new ArgumentException("O nome do Pokémon tem que ser diferente de nulo/vazio");
            }
            
            try {
                Pokemon p = await DataFetcher.GetNamedApiObject<Pokemon>(PokemonName.ToLower());

                //O erro no teste quando fizemos juntos está aqui, eu estava fazendo p.Stats.Where, era só ter usado o .FirstOrDefault :( rs
                this.Name = p.Name;
                this.Health = (p.Stats.FirstOrDefault(x => x.Stat.Name == "hp")).BaseValue;
                this.Attack = (p.Stats.FirstOrDefault(x => x.Stat.Name == "attack")).BaseValue;
                this.Defense = (p.Stats.FirstOrDefault(x => x.Stat.Name == "defense")).BaseValue;
                this.SpecialAttack = (p.Stats.FirstOrDefault(x => x.Stat.Name == "special-attack")).BaseValue;
                this.SpecialDefense = (p.Stats.FirstOrDefault(x => x.Stat.Name == "special-defense")).BaseValue;
                this.Speed = (p.Stats.FirstOrDefault(x => x.Stat.Name == "speed")).BaseValue;

                //alguns Pokemóns tem mais de um tipo, a Lugia por exemplo é Flying e Psychic
                foreach (var type in p.Types) {
                    if (String.IsNullOrEmpty(Type))
                        Type = type.Type.Name;
                    else
                        Type = Type + "/" + type.Type.Name;
                }
                //checar se os status foram carregados
                int[] pokeStatus = { this.Health, this.Attack, this.Defense, this.SpecialAttack, this.SpecialDefense, this.Speed };
                
                if(pokeStatus.Any(x => x == 0) || this.Type.Length == 0) {
                    throw new InvalidOperationException("Um ou mais status do Pokémon não foram encontrados");
                }
            
            } catch(Exception ex) {
                if(ex is HttpRequestException apiException) {
                    if(apiException.StatusCode == HttpStatusCode.NotFound) {
                        throw new HttpRequestException($"Pokémon {PokemonName} não encontrado! certifique-se que o nome está correto.");
                    }
                    else {
                        throw new HttpRequestException($"Erro ao acessar a API, não foi possível carregar o Pokémon {PokemonName}");
                    }
                }
                else {
                    throw new Exception($"Erro: {ex.Message}");
                }
            }
        }
        public void DisplayPokemon() {
            Console.WriteLine($"Pokémon: {this.Name} ({this.Type})");
            Console.WriteLine($"HP {this.Health}");
            Console.WriteLine($"Attack: {this.Attack} Defense: {this.Defense} Speed: {this.Defense}");
            Console.WriteLine($"Special Attack: {this.SpecialAttack} Special Defense: {this.SpecialDefense}");
        }
    }
}
