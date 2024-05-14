# Pok�mon Battle Simulator

Battle Simulator � uma pequena simula��o comparando o HP entre 2 Pok�mons a fim de definir um vencedor.
O projeto utiliza [Pok�API](https://pokeapi.co/) e um [.NET wrapper](https://github.com/PokeD/PokeAPI-NJ.NET) para abstrair e facilitar as chamadas para a API.

## Arquitetura do Projeto
O projeto faz uso de 2 classes, **PokemonObj** e **BattleSimulator**:

### PokemonObj
Essa classe � a representa��o de um Pok�mon que � carregado por uma chamada � Pok�API, os atributos b�sicos
de um Pok�mon (como a vida, defesa, ataque) s�o atribu�dos ap�s carregar o objeto da API (utilizando o nome do Pok�mon)
```
Pokemon p = await DataFetcher.GetNamedApiObject<Pokemon>(PokemonName.ToLower());
```
Ap�s termos esse objeto carregado em mem�ria atribu�mos os campos relevantes via um filtro:
```
this.Health = (p.Stats.FirstOrDefault(x => x.Stat.Name == "hp")).BaseValue;
```
Esse filtro � necess�rio pois temos um Array de stats vindos da API que representa todos esses stats B�sicos de batalha encapsulados em um objeto
com campos como *Name* e *BaseValue*.

O m�todo **LoadPokemonFromApi(string pokemonName)** da minha classe � respons�vel por fazer a chamada � API e carregar as informa��es.
Esse projeto foi criado como uma **Console Application** e as informa��es aparecem no terminal de console na hora da execu��o, por conta disso criei um m�todo **DisplayPokemon()** para imprimir as informa��es do Pok�mon de uma forma mais clara.

A responsabilidade da classe Pok�monObj � apenas representar um Pok�mon, a compara��o de hp entre 2 pok�mons � feita na classe **BattleSImulator**.

### Battle Simulator
Essa classe � extremamente simples e consta com apenas 1 m�todo **StartBattle(PokemonObj p1, PokemonObj p2)** onde � comparado o hp entre p1 e p2 e define um poss�vel vencendor baseado **apenas** no hp.

### BattleSimulatorTests
Nesse projeto temos os testes unit�rios da classe Pok�monObj, como fazemos chamadas para uma API Externa o m�todo � encapsulado com uma exce��o *HttpRequestException* ent�o � testado
uma chamada bem sucedida onde as informa��es s�o carregadas e uma com um nome errado que o m�todo retorna 404.
Como BattleSimulator � uma classe extremamente simples que usa e depende apenas de 2 objetos da classe testada sem adicionar integra��o ou funcionalidade n�o foi feito testes nela.

### Considera��es Finais
Achei extremamente divertido fazer o parse de informa��es da Pok�API! Eu pensei em fazer mais funcionalidades ou fazer um sistema de batalha levando em conta
mais status como ataque/defesa/tipo do pok�mon por�m senti que sairia muito do escopo do que foi pedido e que ser� avaliado, por�m da maneira que foi feito (separando Pok�mon via PokemonObj e BattleSimulator)
� poss�vel extender essas funcionalidades de maneira bem intuitiva, � poss�vel ter a lista de habilidades do Pok�mon e itens por exemplo, e na classe BattleSimulator temos como criar um loop onde tomamos turnos usando essas habilidades e criando l�gicas de batalha.
�nica coisa que fiz "a mais" � imprimir na tela os status do Pok�mon e seu tipo, mas apenas levando em conta o hp para definir o resultado.
Um detalhe tamb�m � o carregamento das informa��es vindas da Api em PokeObj
Obrigado!