
using Application.Interfaces;
using Application.Schemas;
using Application.UseCase;
using Domain.Entities;
using Infrastructure.cqrs_Query;
using Moq;

namespace UnitTest.Application.UseCase
{
    public class ServiceComandaTest
    {

        //public Task<ComandaResponse> InsertComanda(ComandaRequest body);
        [Fact]
        public async Task InsertComandaTest()
        {
            Domain.Entities.FormaEntrega formaEntrega = new()
            {
                FormaEntregaId = 1,
                Descripcion = "delivery"
            };
            //parametro del metodo
            ComandaRequest comandaRequest = new()
            {
                formaEntrega = 1,
                mercaderias = [1, 2] //new List<int> { 1, 2 }
            };


            //ARRANGE
            var commandMock = new Mock<ICommandComanda>();
            var queryMock = new Mock<IQueryComanda>();
            var queryMercaderiaMock = new Mock<IQueryMercaderia>();
            var queryFormaEntregaMock = new Mock<IQueryFormaEntrega>();
            var queryComandaMercaderiaMock = new Mock<IQueryComandaMercaderia>();
            var queryTipoMercaderiaMock = new Mock<IQueryTipoMercaderia>();

            ServiceComanda serviceComanda = new (
                commandMock.Object, 
                queryMock.Object,
                queryMercaderiaMock.Object,
                queryFormaEntregaMock.Object,
                queryComandaMercaderiaMock.Object,
                queryTipoMercaderiaMock.Object
                );

            List<MercaderiaComandaResponse> mercaderiaComandaResponses = new()
            {
                new MercaderiaComandaResponse()
                {
                    id = 1,
                    nombre = "name 1",
                    precio = 1,
                },
                new MercaderiaComandaResponse()
                {
                    id = 2,
                    nombre = "name 2",
                    precio = 2,
                },
            };
            int totalPrecio = 0;
            foreach (var item in mercaderiaComandaResponses)
            {
                totalPrecio += item.precio;
            }
            //Config de los mocks
            queryMercaderiaMock
                .Setup(q => q.ExistId(It.IsAny<int>()))
                .ReturnsAsync(true);
            queryMercaderiaMock
                .Setup(q => q.GetListByIds(It.IsAny<List<int>>()))
                .ReturnsAsync(mercaderiaComandaResponses);
            queryFormaEntregaMock
                .Setup(f => f.GetFormaEntrega(formaEntrega.FormaEntregaId))
                .ReturnsAsync(formaEntrega.Descripcion);

            ComandaResponse comandaResponse = new()
            {
                id = new Guid(),
                mercaderias = mercaderiaComandaResponses,
                formaEntrega = new()
                {
                    id = formaEntrega.FormaEntregaId,
                    descripcion = formaEntrega.Descripcion
                } ,
                total = totalPrecio,
                fecha = new DateTime()
            };

            //ACT
            var result = await serviceComanda.InsertComanda(comandaRequest);

            //ASSERT
            Assert.Equivalent(comandaResponse.id, result.id);
            //Assert.Equal(comandaResponse, result);
        }
        //public Task<List<ComandaResponse>> GetAll(DateTime? fecha);
        [Fact]
        public void GetAllTest()
        {

        }
        //public Task<ComandaGetResponse?> GetComandaById(Guid id);
        [Fact]
        public void GetComandaByIdTest()
        {

        }
    }
}
