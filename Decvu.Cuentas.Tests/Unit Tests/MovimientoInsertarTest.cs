using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.Exceptions;
using Devsu.Cuentas.Aplicacion.Servicios;
using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Decvu.Cuentas.Tests.Unit_Tests
{
    public class MovimientoInsertarTest
    {
        private Mock<IConfiguration> _mockConfig;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRepositorio<Cuenta>> _mockRepoCuenta;
        private Mock<IRepositorio<Movimiento>> _mockRepoMovimiento;
        private Mock<IRepositorioRetiroDiarioPersona> _mockRepoRetirosDia;
        private Mock<ILogger> _mockLogger;

        public MovimientoInsertarTest()
        {
            _mockConfig = new Mock<IConfiguration>(MockBehavior.Strict);
            _mockUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _mockRepoCuenta = new Mock<IRepositorio<Cuenta>>(MockBehavior.Strict);
            _mockRepoMovimiento = new Mock<IRepositorio<Movimiento>>(MockBehavior.Strict);
            _mockRepoRetirosDia = new Mock<IRepositorioRetiroDiarioPersona>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger>(MockBehavior.Default);
        }

        [Fact]
        public async Task CuandoSeIngresaUnMovimientoSinSaldoSuficienteProduceError()
        {
            //Arrange
            Guid personaId = new Guid();
            const string msgExepcionSaldoInsuficiente = "Saldo insuficiente.";

            Cuenta cuentaCliente = new Cuenta
            {
                Estado = true,
                Numero = "0123456789",
                SaldoInicial = 500,
                Tipo = "Ahorros",
                PersonaId = personaId
            };

            DTOGuardarMovimiento movimientoGuardar = new DTOGuardarMovimiento
            {
                CuentaId = cuentaCliente.CuentaId,
                Valor = -900
            };

            _mockRepoCuenta.Setup(r => r.ListarPorId(movimientoGuardar.CuentaId))
                .Returns(Task.FromResult<Cuenta>(cuentaCliente));

            var movimientoServicio = new MovimientoServicio(_mockRepoMovimiento.Object, _mockRepoRetirosDia.Object,
                _mockRepoCuenta.Object, _mockConfig.Object, _mockUnitOfWork.Object, _mockLogger.Object);

            //Act
            var exception = await Record.ExceptionAsync(() => movimientoServicio.Insertar(movimientoGuardar));

            //Assert
            Assert.IsType<BusinessException>(exception);
            Assert.Equal(msgExepcionSaldoInsuficiente, exception.Message);
        }

        [Fact]
        public void CuandoSeListaMovimientosSeObtieneUnaListaDeMovimientosConDescripcionDeLaOperacion()
        {
            Cuenta cuenta = new Cuenta
            {
                Estado = true,
                Numero = "0123456789",
                SaldoInicial = 0,
                Tipo = "Corriente",
                PersonaId = new Guid()
            };

            Movimiento mov = new Movimiento
            {
                CuentaId = new Guid(),
                Fecha = DateTime.Now,
                Valor = -700,
                Saldo = 0,
                Tipo = "Corriente",
                Cuenta = cuenta
            };

            List<Movimiento> movimientos = new List<Movimiento>();
            movimientos.Add(mov);

            _mockRepoMovimiento.Setup(r => r.ListarTodo())
                .Returns(Task.FromResult<IList<Movimiento>>(movimientos));

            var movimientoServicio = new MovimientoServicio(_mockRepoMovimiento.Object, _mockRepoRetirosDia.Object,
                _mockRepoCuenta.Object, _mockConfig.Object, _mockUnitOfWork.Object, _mockLogger.Object);

            var dtoMovimientos = movimientoServicio.ListarTodo();

            Assert.Equal(dtoMovimientos.Result[0].Movimiento, $"Retiro de { Math.Abs(mov.Valor) }");
        }
    }
}
