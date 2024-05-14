
namespace PokeApiTest.Model {
    public class BattleSimulator {
    
        public void StartBattle(PokemonObj p1, PokemonObj p2) {
            p1.DisplayPokemon();
            Console.WriteLine("----------------");
            p2.DisplayPokemon();
            Console.WriteLine();

            if (p1.Health > p2.Health)
                Console.WriteLine($"Façam suas apostas! Parece que o Pokémon {p1.Name} vai ganhar! ele tem mais HP que o Pokémon {p2.Name}");
            else if (p2.Health > p1.Health)
                Console.WriteLine($"Façam suas apostas! Parece que o Pokémon {p2.Name} vai ganhar! ele tem mais HP que o Pokémon {p1.Name}");
            else if (p1.Health == p2.Health)
                Console.WriteLine("Possível empate! melhor ficar atento aos outros stats do seu Pokémon como ataque ou defesa, quem sabe você ganhe!");
        }
    }
}
