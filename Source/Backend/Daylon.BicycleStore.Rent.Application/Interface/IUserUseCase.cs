namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IUserUseCase
    {
        // POST
        public Task<Domain.Entity.User> ExecuteRegisterUserAsync(Communication.Request.User.RequestRegisterUserJson request);
    }
}
