using PokeApiTest.Model;


try {
    PokemonObj p1 = new PokemonObj();
    PokemonObj p2 = new PokemonObj();

    await p1.LoadPokemonFromApi("gengar");
    await p2.LoadPokemonFromApi("tauros");

    BattleSimulator battle = new BattleSimulator();

    battle.StartBattle(p1, p2);
}
catch (Exception ex) {
    if(ex is HttpRequestException apiException) {
        Console.WriteLine($"Erro de HttpRequest: {apiException.Message}");
    }
    else {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

