# Pokémon Battle Simulator

Battle Simulator é uma pequena simulação comparando o HP entre 2 Pokémons a fim de definir um vencedor.
O projeto utiliza [PokéAPI](https://pokeapi.co/) e um [.NET wrapper](https://github.com/PokeD/PokeAPI-NJ.NET) para abstrair e facilitar as chamadas para a API.

## Arquitetura do Projeto
O projeto faz uso de 2 classes, **PokemonObj** e **BattleSimulator**:

### PokemonObj
Essa classe é a representação de um Pokémon que é carregado por uma chamada à PokéAPI, os atributos básicos
de um Pokémon (como a vida, defesa, ataque) são atribuídos após carregar o objeto da API (utilizando o nome do Pokémon)
```
Pokemon p = await DataFetcher.GetNamedApiObject<Pokemon>(PokemonName.ToLower());
```
Após termos esse objeto carregado em memória atribuímos os campos relevantes via um filtro:
```
this.Health = (p.Stats.FirstOrDefault(x => x.Stat.Name == "hp")).BaseValue;
```
Esse filtro é necessário pois temos um Array de stats vindos da API que representa todos esses stats Básicos de batalha encapsulados em um objeto
com campos como *Name* e *BaseValue*.

O método **LoadPokemonFromApi(string pokemonName)** da minha classe é responsável por fazer a chamada à API e carregar as informações.
Esse projeto foi criado como uma **Console Application** e as informações aparecem no terminal de console na hora da execução, por conta disso criei um método **DisplayPokemon()** para imprimir as informações do Pokémon de uma forma mais clara.

A responsabilidade da classe PokémonObj é apenas representar um Pokémon, a comparação de hp entre 2 pokémons é feita na classe **BattleSimulator**.

### Battle Simulator
Essa classe é extremamente simples e consta com apenas 1 método **StartBattle(PokemonObj p1, PokemonObj p2)** onde é comparado o hp entre p1 e p2 e define um possível vencendor baseado **apenas** no hp.

### BattleSimulatorTests
Nesse projeto temos os testes unitários da classe PokémonObj, como fazemos chamadas para uma API Externa o método é encapsulado com uma exceção *HttpRequestException* então é testado
uma chamada bem sucedida onde as informações são carregadas e uma com um nome errado que o método retorna 404.
Como BattleSimulator é uma classe extremamente simples que usa e depende apenas de 2 objetos da classe testada sem adicionar integração ou funcionalidade não foi feito testes nela.

### Execução
Dentro de Program.cs está sendo instanciado dois PokemonObj passando o nome de 2 pokémons, são os 2 a serem simulados via BattleSimulator, os resultados e informações aparecem no console criado na execução!
![PokeBattleExample](https://github.com/TMSP/PokeApiTest/assets/13991801/f65ba2b1-6108-4ee7-916f-94acfc9551ec)

### Considerações Finais
Achei extremamente divertido fazer o parse de informações da PokéAPI! Eu pensei em fazer mais funcionalidades ou fazer um sistema de batalha levando em conta
mais status como ataque/defesa/tipo do pokémon porém senti que sairia muito do escopo do que foi pedido e que será avaliado, porém da maneira que foi feito (separando Pokémon via PokemonObj e BattleSimulator)
é possível extender essas funcionalidades de maneira bem intuitiva, é possível ter a lista de habilidades do Pokémon e itens por exemplo, e na classe BattleSimulator temos como criar um loop onde tomamos turnos usando essas habilidades e criando lógicas de batalha.
Única coisa que fiz "a mais" é imprimir na tela os status do Pokémon e seu tipo, mas apenas levando em conta o hp para definir o resultado.
Um detalhe também é o carregamento das informações vindas da Api em PokeObj
Obrigado!
