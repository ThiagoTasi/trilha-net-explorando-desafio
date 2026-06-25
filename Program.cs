using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Seja bem-vindo ao sistema de reservas de hospedagem!");

Console.WriteLine("Por favor, informe os dados da reserva.");

Console.WriteLine("----------------------------------------------");

Console.WriteLine("Digite a quantidade de dias reservados:");
int diasReservados = int.Parse(Console.ReadLine());

Console.WriteLine("Quantas pessoas vão se hospedar?");
int quantidadeHospedes = int.Parse(Console.ReadLine());

if (quantidadeHospedes > 10)
{
    Console.WriteLine("\n❌ Desculpe, o hotel não possui suítes que acomodem mais de 10 pessoas.");
    Console.WriteLine("O processo de reserva foi cancelado.");
    return; // Esse 'return' faz o programa parar IMEDIATAMENTE aqui!
}

//Cria os modelos de hóspedes e cadastra na lista de hóspedes

List<Pessoa> hospedes = new List<Pessoa>();

for (int i = 1; i <= quantidadeHospedes; i++)
{
    Console.WriteLine($"Digite o nome do hóspede {i}:");
    string nomeHospede = Console.ReadLine();
    Pessoa hospede = new Pessoa(nome: nomeHospede);
    hospedes.Add(hospede);
}
// Mensagem de sucesso para testar no terminal
Console.WriteLine($"\n[Sucesso]: {hospedes.Count} hóspedes cadastrados para {diasReservados} dias!");


// --- DEFINIÇÃO AUTOMÁTICA DA SUÍTE E PREÇOS ---
string tipoSuite = "";
int capacitySuite = 0;
decimal valorPorPessoa = 0;

if (quantidadeHospedes <= 2)
{
    tipoSuite = "Simples";
    capacitySuite = 2;
    valorPorPessoa = 80;
}
else if (quantidadeHospedes <= 4)
{
    tipoSuite = "Clássico";
    capacitySuite = 4;
    valorPorPessoa = 160;
}
else if (quantidadeHospedes <= 6)
{
    tipoSuite = "Master";
    capacitySuite = 6;
    valorPorPessoa = 200;
}
else if (quantidadeHospedes <= 8)
{
    tipoSuite = "Família";
    capacitySuite = 8;
    valorPorPessoa = 250;
}
else if (quantidadeHospedes <= 10)
{
    tipoSuite = "Super";
    capacitySuite = 10;
    valorPorPessoa = 500;
}
else
{
    Console.WriteLine("\n❌ Desculpe, não temos suítes que acomodem mais de 10 pessoas.");
    return;
}
decimal valorDiariaDoGrupo = valorPorPessoa * quantidadeHospedes;

Suite suite = new Suite(tipoSuite, capacitySuite, valorDiariaDoGrupo);

// Cria a suíte com os dados automáticos

Console.WriteLine("\n----------------------------------------------");
Console.WriteLine("          PROCESSANDO CHECK-IN                ");
Console.WriteLine("----------------------------------------------");

try
{
    // Instancia a classe Reserva passando os seus dias do ReadLine
    Reserva reserva = new Reserva(diasReservados: diasReservados);
    
    // Vincula a suíte automática
    reserva.CadastrarSuite(suite);
    reserva.CadastrarHospedes(hospedes);

    decimal valorBrutoTotal = suite.ValorDiaria * diasReservados;
    decimal valorFinalComDesconto = reserva.CalcularValorDiaria();

    // Exibe os resultados na tela puxando os seus métodos criados no desafio
    Console.WriteLine("\n✨ Check-in concluído com sucesso! ✨");
    Console.WriteLine($"-> Suíte Selecionada: {suite.TipoSuite} (Capacidade máxima: {suite.Capacidade})");
    Console.WriteLine($"-> Total de Hóspedes: {reserva.ObterQuantidadeHospedes()}");
    Console.WriteLine($"-> Tempo de Estadia: {diasReservados} dias");
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine($"-> VALOR DE 1 DIÁRIA PARA O GRUPO: {suite.ValorDiaria:C}");
    Console.WriteLine($"-> VALOR TOTAL DAS DIÁRIAS: {reserva.CalcularValorDiaria():C}");

    if (diasReservados >= 10)
    {
        decimal valorEconomizado = valorBrutoTotal - valorFinalComDesconto;
        Console.WriteLine($"-> 🎉 DESCONTO DE ESTADIA LONGA (10%): - {valorEconomizado:C}");
        Console.WriteLine($"-> VALOR BRUTO SEM DESCONTO: {valorBrutoTotal:C}");
    }
    
    Console.WriteLine($"-> VALOR TOTAL A PAGAR: {valorFinalComDesconto:C}");
}

catch (Exception ex)
{
    // Se der qualquer erro de capacidade mapeado na sua classe, ele cai de pé aqui
    Console.WriteLine($"\n❌ Erro ao realizar check-in: {ex.Message}");
}

Console.WriteLine("\n==============================================");
Console.WriteLine("Pressione qualquer tecla para encerrar...");
Console.ReadKey();













