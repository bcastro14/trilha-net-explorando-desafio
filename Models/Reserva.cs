namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            // Verificando se a capacidade é maior ou igual ao número de hóspedes sendo recebido
            int capacidadeSuite = Suite.Capacidade;
            int quantidadeHospedes = hospedes.Count;
            if (capacidadeSuite >= quantidadeHospedes)
            {
                Hospedes = hospedes;
            }
            else
            {
                // Retornando uma exception caso a capacidade seja menor que o número de hóspedes recebido
                throw new ArgumentException("A suite não comporta essa quantidade de hóspedes.");
            }
        }

        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            // Retorna a quantidade de hóspedes (propriedade Hospedes)
            return Hospedes.Count;
        }

        public decimal CalcularValorDiaria()
        {
            // Retorna o valor da diária. Cálculo: DiasReservados X Suite.ValorDiaria
            decimal valor = Suite.ValorDiaria * DiasReservados;

            // Regra: Caso os dias reservados forem maior ou igual a 10, conceder um desconto de 10%
            if (DiasReservados >= 10)
            {
                valor = valor * 0.90M;
            }

            return valor;
        }
    }
}