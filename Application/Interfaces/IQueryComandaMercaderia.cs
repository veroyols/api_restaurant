using Application.Schemas;

namespace Application.Interfaces
{
    public interface IQueryComandaMercaderia
    {
        //3
        public Task<List<ComandaResponse>> GetListByIds(List<Guid> guids);
    }
}
