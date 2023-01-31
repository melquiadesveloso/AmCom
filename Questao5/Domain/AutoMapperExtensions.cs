using AutoMapper;
namespace Questao5.Domain
{
    public static class AutoMapperExtensions
    {
        public static IMapper Mapper { get; set; }

        public static TTo MapTo<TTo>(this object sourceObject)
        {
             //return (TTo)sourceObject;
            return (TTo)Mapper.Map(sourceObject, sourceObject.GetType(), typeof(TTo));
        }

    }
}
