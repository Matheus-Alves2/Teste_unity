using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using Reservas.Api.Controllers;
using Reservas.Api.Interfaces;
using Reservas.Api.Models;
using Reservas_Teste.MockData;

namespace Reservas_Teste.Controllers
{
    public class ReservasControllerTeste
    {

        [Fact]
        public void GetTodasReservas_DeveRetornar200Status()
        {
            //Arrange - Organizar
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.Reservas).Returns(ReservasMockData.GetReservas());
            var sut = new ReservasController(reservaService.Object);

            //Act - Agir
            var result = (OkObjectResult)sut.Get();

            //Assert - Verificar
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public void
            GetReservaPorId_DeveRetornar200Status_QuandoEncontrar()
        {
            //Arrange - Organizar
            var reservaMock = ReservasMockData.GetReservas()[0];
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[reservaMock.ReservaId]).Returns(reservaMock);
            var sut = new ReservasController(reservaService.Object);

            //Asset - Verificar
            var result = sut.Get(reservaMock.ReservaId);

            //Act - Agir
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }
        [Fact]
        public void GetReservaPorId_DeveRetornar404Status_QuandoNaoEncontrar()
        {
            //Arrange - Organizar
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)null);
            var sut = new ReservasController(reservaService.Object);

            //Asset - Verificar
            var result = sut.Get(999);
            //Act - Agir
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void PostReserva_DeveRetornar201Status()
        {
            //Arrange - Organizar
            var novaReserva = new Reserva { Nome = "zezao" };
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.AddReserva(It.IsAny<Reserva>())).Returns(novaReserva);
            var sut = new ReservasController(reservaService.Object);
            //Asset - Verificar
            var result = sut.Post(novaReserva);
            //Act - Agir
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
        }
        [Fact]
        public void PutReserva_DeveRetornar200Status()
        {
            //Arrange - Organizar
            var reservaAtualizada = new Reserva { ReservaId = 1, Nome = "zezao" };
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.UpdateReserva(It.IsAny<Reserva>())).Returns(reservaAtualizada);
            var sut = new ReservasController(reservaService.Object);
            //Act - Agir
            var result = sut.Put(reservaAtualizada);

            //Assert - Verificar
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

          
            
        }
        [Fact]
        public void PatchReserva_DeveRetornar200Status_QuandoEncontrar()
        {
            //Arrange - Organizar
            var reserva = new Reserva { ReservaId = 1, Nome = "Sillas" };
            var patch = new JsonPatchDocument<Reserva>();
            patch.Replace(i => i.Nome, "Sillas Atualizado");
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[1]).Returns(reserva);
            var sut = new ReservasController(reservaService.Object);

            //Act - Agir
            var result = sut.Patch(1, patch);

            //Assert - Verificar
            result.Should().BeOfType<OkResult>();

        }
        [Fact]
        public void PatchReserva_DeveRetornar404_QuandoEncontrar()
        {
            //Arrange - Organizar
            var patch = new JsonPatchDocument<Reserva>();
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)
            null);
            var sut  = new ReservasController(reservaService.Object);

            //Act - Agir
            var result = sut.Patch(999, patch);

            //Assert - Verificar
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void DeleteReserva_DeveRetornar204Status()
        {
            //Arrange - Organizar
            var reservaService = new Mock<IReservaRepository>(); 
            var sut = new ReservasController(reservaService.Object);
            //Act - Agir
            var result = sut.Delete(1);
            //Assert - Verificar
            result.Should().BeOfType<NoContentResult>();

        }
    }
}
