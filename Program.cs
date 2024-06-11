using System.Text;
using DesafioProjetoHospedagem.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
Console.OutputEncoding = Encoding.UTF8;

bool menuPrincipal = true;
bool criandoSuites = true;

List<Suite> suites = new List<Suite>();
List<Reserva> reservas = new List<Reserva>();

while(menuPrincipal)
{
    // Criando as primeiras suites:
    while (criandoSuites)
    {
        Console.Clear();

        AddNovaSuite();

        MostraSuitesDisponiveis();

        Console.WriteLine("\nDeseja adicionar outro tipo de suíte? Responda com 'S' para Sim ou 'N' para Não:");
        string escolha = Console.ReadLine();
        if (escolha.ToUpper() != "S")
            criandoSuites = false;
    }

    Console.Clear();
    Console.WriteLine(
        "Menu principal - Selecione uma das opções:" + 
        "\n1 - Criar nova reserva" +
        "\n2 - Ver reservas existentes" + 
        "\n3 - Excluir reserva existente" +
        "\n4 - Verificar tipos de suítes disponíveis" +
        "\n5 - Encerrar o programa"
    );
    int selecao = int.Parse(Console.ReadLine());
    Console.Clear();
    switch(selecao)
    {
        case 1:
            CriaNovaReserva();
            break;

        case 2:
            VerReservasExistentes();
            break;

        case 3:
            RemoverReserva();
            break;

        case 4:
            MostraSuitesDisponiveis();
            break;

        case 5:
            menuPrincipal = false;
            Console.WriteLine("Encerrando o programa...");
            break;

        default:
            Console.WriteLine("Escolha inválida, tente novamente.");
            break;
    }
    Console.WriteLine("\nPressione Enter para continuar.");
    Console.ReadLine();
}


void MostraSuitesDisponiveis()
{
    Console.WriteLine("\nSuas suítes são: ");
    foreach(var item in suites)
    {
        Console.WriteLine($"Suite tipo: {item.TipoSuite}.\tCapacidade: {item.Capacidade}." + 
        $"\tValor da diária: {item.ValorDiaria.ToString("C")}.");
    }
}


void ListaNomeDasPessoas(List<Pessoa> pessoas)
{
    foreach(var item in pessoas)
    {
        Console.Write(item.NomeCompleto + ", ");
    }
}


void AddNovaSuite()
{
    Console.WriteLine("Defina as características das suítes que estão disponíveis no seu hotel: \n");

    Console.WriteLine("Qual o tipo da suíte?");
    string tipo = Console.ReadLine();
    Console.WriteLine("Qual a capacidade da suíte?");
    int capacidade = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Qual o valor da diária desta suíte?");
    decimal valor = Decimal.Parse(Console.ReadLine());

    Suite suite = new Suite(tipoSuite: tipo, capacidade: capacidade, valorDiaria: valor);
    suites.Add(suite);
}


void CriaNovaReserva()
{
    int countHospede = 1;
    List<Pessoa> hospedes = new List<Pessoa>();
    bool adicionandoHospedes = true;
    
    // Definindo hóspedes da reserva
    while(adicionandoHospedes)
    {
        // Adicionando um novo hóspede
        Console.Clear();
        Console.WriteLine("Primeiro vamos cadastrar os hóspedes:\n");
        Console.WriteLine($"Qual o primeiro nome do hóspede #{countHospede}?");
        string primeiroNome = Console.ReadLine();
        Console.WriteLine($"Qual o sobrenome do hóspede #{countHospede}?");
        string sobrenome = Console.ReadLine();

        hospedes.Add(new Pessoa(nome: primeiroNome, sobrenome: sobrenome));
        countHospede++;

        // Listando os hóspedes adicionados
        Console.Clear();
        Console.WriteLine("Os hóspedes adicionados até o momento para esta reserva:");
        for(int i = 0; i < hospedes.Count; i++)
        {
            Console.WriteLine($"Hóspede {i+1} - {hospedes[i].NomeCompleto}");
        }
        
        // Confirmando se a quantidade de hóspedes adicionada está dentro da capacidade máxima da maior suíte:
        int capacidadeMax = suites.Max(s => s.Capacidade);
        if (hospedes.Count < capacidadeMax)
        {
            // Verificando se será adicionado mais hóspedes.
            Console.WriteLine("\nDeseja adicionar mais um hóspede? Responda com 'S' para Sim ou 'N' para Não:");
            string escolha = Console.ReadLine().ToUpper();
            if (escolha == "N")
                adicionandoHospedes = false;
        }
        else
        {
            adicionandoHospedes = false;
            Console.WriteLine("\nQuantidade máxima de hóspedes por reserva atingida. \nPressione Enter para continuar.");
            Console.ReadLine();
        }  
    }

    // Definindo a suite da reserva
    Console.Clear();
    Console.Write("Qual o tipo de suíte escolhida?");
    MostraSuitesDisponiveis();
    
    string suiteEscolhida = Console.ReadLine().ToUpper();
    Suite suite = suites.FirstOrDefault(s => s.TipoSuite.ToUpper() == suiteEscolhida);
    while (true)//suite == null)
    {
        if(suite == null)
        {
            Console.WriteLine("Input inválido. Escolha um tipo de suíte válido entre as opções disponíveis:");
            suiteEscolhida = Console.ReadLine().ToUpper();
            suite = suites.FirstOrDefault(s => s.TipoSuite.ToUpper() == suiteEscolhida);
            continue;
        }
        if(hospedes.Count > suite.Capacidade)
        {
            Console.WriteLine($"A suíte escolhida não comporta a quantidade de hóspedes," +
                              $" escolha uma com Capacidade de ao menos {hospedes.Count}");
            suiteEscolhida = Console.ReadLine().ToUpper();
            suite = suites.FirstOrDefault(s => s.TipoSuite.ToUpper() == suiteEscolhida);
            continue;
        }
        break;
    }
    while (hospedes.Count > suite.Capacidade)
    {
        Console.WriteLine("suite escolhida não ");
        suiteEscolhida = Console.ReadLine().ToUpper();
        suite = suites.FirstOrDefault(s => s.TipoSuite.ToUpper() == suiteEscolhida);
    }
    Console.WriteLine("A suíte escolhida foi:");
    Console.WriteLine($"Suite tipo {suite.TipoSuite}. Capacidade: {suite.Capacidade}." + 
        $" Valor da diária: {suite.ValorDiaria.ToString("C")}.");
    
    Console.WriteLine("Pressione Enter para continuar");
    Console.ReadLine();

    // Definindo a duração da reserva
    Console.Clear();
    Console.WriteLine("Quantas noites desejam ficar hospedados? à partir de 10 noites há 10% de desconto.");
    int duracao = int.Parse(Console.ReadLine());
    Reserva novaReserva = new Reserva(hospedes, suite, duracao);
    decimal custo = novaReserva.CalcularValorDiaria();

    // Confirmando os detalhes da reserva
    Console.Clear();
    Console.Write($"Os detalhes da reserva são:" +
                      $"\nLista de hóspedes: ");
    ListaNomeDasPessoas(novaReserva.Hospedes);
    Console.WriteLine($"\nSuíte: {novaReserva.Suite.TipoSuite}");
    Console.WriteLine($"Custo total: {custo.ToString("C")}");

    Console.WriteLine("Pressione Enter para confirmar, ou digite 'cancela' para descartar essa reserva");
    string confirmacao = Console.ReadLine();
    if (confirmacao.ToLower() == "cancela")
    {
        Console.WriteLine("Reserva descartada.");
    }
    else
    {
        reservas.Add(novaReserva);
        Console.WriteLine("Reserva confirmada!");
    }
}

void RemoverReserva()
{
    Console.Clear();
    Console.WriteLine("Qual o número da reserva que você deseja remover?");
    int numeroReserva = int.Parse(Console.ReadLine()) - 1;
    try{reservas.RemoveAt(numeroReserva);}
    catch{Console.WriteLine("Invalid index selected");}

    Console.WriteLine("As reservas existentes são:");
    VerReservasExistentes();
}

void VerReservasExistentes()
{
    Console.Clear();
    Console.WriteLine("\n");
    for (int i = 0; i < reservas.Count; i++)
    {
        Console.Write($"Os detalhes da reserva #{i+1} são:" +
                      $"\nLista de hóspedes: ");
        ListaNomeDasPessoas(reservas[i].Hospedes);
        Console.WriteLine($"\nSuíte: {reservas[i].Suite.TipoSuite}");
        Console.WriteLine($"Custo total: {reservas[i].CalcularValorDiaria().ToString("C")}\n");
    }
}