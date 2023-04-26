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
        //3
        public async Task InsertComanda(Comanda comanda, List<ComandaMercaderia> comandaMercaderias)
        {
            using var dbContextTransaction = _appDbContext.Database.BeginTransaction();
            try
            {
                await _appDbContext.ComandaDb.AddAsync(comanda);
                await _appDbContext.ComandaMercaderiaDb.AddRangeAsync(comandaMercaderias);
                await _appDbContext.SaveChangesAsync();
                dbContextTransaction.Commit();                   
            }
            catch 
            {
                dbContextTransaction.Rollback();
            }
        }
    }
}
