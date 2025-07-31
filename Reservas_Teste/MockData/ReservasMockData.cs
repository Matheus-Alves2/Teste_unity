using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservas.Api.Models;

namespace Reservas_Teste.MockData
{
    public class ReservasMockData
    {
        public static List<Reserva> GetReservas()
        {
            return new List<Reserva>()
            {
                new Reserva { ReservaId = 1, Nome = "Herick", InicioLocacao = "São Paulo", FimLocacao = "São Caetano" }
                ,
                new Reserva { ReservaId = 2, Nome = "Sillas", InicioLocacao = "São Paulo", FimLocacao = "Mooca" }
                ,
                new Reserva { ReservaId = 3, Nome = "Ryan", InicioLocacao = "São Paulo", FimLocacao = "São Caetano" }
            };
        }
    }
}
