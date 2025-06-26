namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IBicycleService
    {
        Task<IList<Domain.Entity.Bicycle>> GetBicyclesAsync();

    }
}
