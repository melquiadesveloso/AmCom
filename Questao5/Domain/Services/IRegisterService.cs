
namespace Questao5.Domain.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IRegisterService
    {
        IServiceCollection Register(IServiceCollection services);
    }
}
