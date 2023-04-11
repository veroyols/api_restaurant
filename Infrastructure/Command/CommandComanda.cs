using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.cqrs_Command
{
    public class CommandComanda : ICommandComanda
    {
        private readonly AppDbContext _appDbContext;

        public CommandComanda(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task InsertComanda(Comanda comanda)
        {
            _appDbContext.Add(comanda);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
