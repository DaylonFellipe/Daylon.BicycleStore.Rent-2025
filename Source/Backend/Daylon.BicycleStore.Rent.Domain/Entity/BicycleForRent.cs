namespace Daylon.BicycleStore.Rent.Domain.Entity
{
    public class BicycleForRent : Bicycle
    {
        public Guid OrderId { get; set; }

        // valor por hora ou dia
        // status (alugado, atrasado, entregue)

        // dias de aluguel
        // Metodo de Pagamento
        // datetime do inicio do alugel
        // datetime do dia da entrega
        // Valor total

    }
}
