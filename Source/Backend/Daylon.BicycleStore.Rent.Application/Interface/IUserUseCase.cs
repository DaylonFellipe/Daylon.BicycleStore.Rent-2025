namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IUserUseCase
    {
        // POST
        public Task<Domain.Entity.User> ExecuteRegisterUserAsync(Communication.Request.User.RequestRegisterUserJson request);

        // PATCH
        public Task<Domain.Entity.User> ExecuteUpdateUserNameAsync(Guid id, string? firstName, string? LastName);
        public Task<Domain.Entity.User> ExecuteUpdateUserEmailAsync(Guid id, string newEmail, string password);
        public Task<Domain.Entity.User> ExecuteUpdateUserPasswordAsync(Guid id, string oldPassword, string newPassword);


        // PUT
        public Task<Domain.Entity.User> ExecuteUpdateUserStatusAsync(Domain.Entity.User user);
    }
}
