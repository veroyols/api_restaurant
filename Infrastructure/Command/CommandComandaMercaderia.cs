using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.cqrs_Command
{
    public class CommandComandaMercaderia : ICommandComandaMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public CommandComandaMercaderia(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task InsertComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            _appDbContext.Add(comandaMercaderia);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
