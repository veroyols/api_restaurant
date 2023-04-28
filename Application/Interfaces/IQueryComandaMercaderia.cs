using Application.Schemas;

namespace Application.Interfaces
{
    public interface IQueryComandaMercaderia
    {
        //3
        //public Task<ComandaResponse> GetComandaResponse(Guid guid);

        public Task<List<ComandaResponse>> GetListByIds(List<Guid> guids);
    }
}
