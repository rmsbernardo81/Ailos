using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Services.Controllers;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        private Mock<IMediator> _mediator;

        public UnitTest1()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void GetContaCorrente_Success_Result()
        {
            _mediator.Setup(a => a.Send(It.IsAny<ContaCorrenteQueryRequest>(), new CancellationToken()))
                .ReturnsAsync(new ContaCorrenteQueryResponse { Nome = "Teste", Valor = 10, DataHora = DateTime.Now, Numero = 123 });

            var postController = new ContaCorrenteController(_mediator.Object);

            //Action
            var result = postController.GetSaldoContaCorrente("B6BAFC09 -6967-ED11-A567-055DFA4A16C9");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}