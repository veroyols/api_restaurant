using Application.Interfaces;

namespace Application.UseCase
{
    public class ServiceComanda : IServiceComanda
    {
        private readonly ICommandComanda _command;
        private readonly IQueryComanda _query;

        public ServiceComanda(ICommandComanda command, IQueryComanda query)
        {
            _command = command;
            _query = query;
        }
    }
}
