namespace Customer.API.Abstraction.Installers
{
    public interface IInstaller
    {
        void RegisterService(IServiceCollection services, IConfiguration configuration);
    }
}
