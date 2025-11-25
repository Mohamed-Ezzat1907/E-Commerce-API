namespace E_Commerce.Domain.Contracts
{
    public interface IDbInititlaeizer
    {
        public Task InitializeAsync();
        public Task InitializeIdentityAsync();
    }
}
