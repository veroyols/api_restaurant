using Application.Schemas;

namespace Application.Interfaces
{
    public interface IServiceComanda
    {
        //2
        public Task<ComandaResponse> InsertComanda(ComandaRequest body);
        //3
        public Task<List<ComandaResponse>> GetAll(string fecha);
        //8
        public Task<ComandaGetResponse?> GetComandaById(string id);

    }
}
