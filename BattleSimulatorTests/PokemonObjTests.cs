using PokeAPI;
using PokeApiTest.Model;
using System.Net;

namespace BattleSimulatorTests {
    public class PokemonObjTests {
        
        [Fact]
        public async void LoadPokemonFromApi_ValidPokemonName() {
            var pk = new PokemonObj();

            await pk.LoadPokemonFromApi("mew");

            Assert.NotNull(pk.Name);
            Assert.NotEmpty(pk.Name);

        }

        [Fact]
        public async Task LoadPokemonFromApi_InvalidPokemonName() {
            var pk = new PokemonObj();
            
            await Assert.ThrowsAsync<HttpRequestException>(() => pk.LoadPokemonFromApi("gabumon"));
        }
    }
}