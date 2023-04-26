using Application.Schemas;

namespace Application.Interfaces
{
    public interface IQueryFormaEntrega
    {
        //2
        public Task<string> GetFormaEntrega(int id);
    }
}
