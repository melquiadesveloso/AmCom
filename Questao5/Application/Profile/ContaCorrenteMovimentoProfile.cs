
namespace Questao5.Application.Profile
{
    using AutoMapper;
    using Questao5.Application.Commands.Requests;
    using Questao5.Application.Queries.Responses;
    using Questao5.Domain.Entities;
    using Questao5.Infrastructure.Database.QueryStore.Responses;

    public class ContaCorrenteMovimentoProfile : Profile
    {
        public ContaCorrenteMovimentoProfile()
        {
            //App
            CreateMap<CreateContaCorrenteMovimentoRequest, ContaCorrenteMovimento>()
                .ForMember(dest => dest.IdContaCorrente, opt => opt.MapFrom(src => src.IdContaCorrente))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
                .ForMember(dest => dest.TipoMovimento, opt => opt.MapFrom(src => src.TipoMovimento));

            //Repository
            CreateMap<GetContaCorrenteRepositoryResponse, GetSaldoContaCorrenteQueryResponse>();

            //CreateMap<ContaCorrenteMovimento, CreateContaCorrenteMovimentoRequest>();

        }
    }
}
