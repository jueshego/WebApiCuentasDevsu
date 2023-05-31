using Devsu.Cuentas.Dominio.Modelos;
using Devsu.Cuentas.Infraestructura.Datos.Contexto;
using Devsu.Cuentas.Infraestructura.Datos.Repositorios;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Xunit;

namespace Decvu.Cuentas.Tests.Integration_Tests
{
    public class ClienteRepositorioTest : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<CuentasDevsuDBContext> _contextOptions;

        public ClienteRepositorioTest()
        {
            _connection = new SqliteConnection("Data Source=test.sqlite");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<CuentasDevsuDBContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new CuentasDevsuDBContext(_contextOptions);

            if (context.Database.EnsureCreated())
            {
                using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
                                            CREATE VIEW AllResources AS
                                            SELECT Identificacion
                                            FROM Clientes;";
                viewCommand.ExecuteNonQuery();
            }

            context.AddRange(
                new Cliente
                {
                    Identificacion = "1234567890",
                    Nombre = "Cliente Pruebas 1",
                    Edad = 50,
                    Genero = "M",
                    Direccion = "Cra Av",
                    Telefono = "5555555555",
                    Contrasena = "pruebas1234"
                },
                new Cliente
                {
                    Identificacion = "0987654321",
                    Nombre = "Cliente Pruebas 2",
                    Edad = 30,
                    Genero = "F",
                    Direccion = "Cra Av",
                    Telefono = "5555555555",
                    Contrasena = "pruebas1234"
                }); ;
            context.SaveChanges();
        }

        CuentasDevsuDBContext CreateContext() => new CuentasDevsuDBContext(_contextOptions);

        public void Dispose() => _connection.Dispose();

        [Fact]
        public void RecuperarTodosLosClientes()
        {
            using var context = CreateContext();
            var repositorio = new Repositorio<Cliente>(context);

            var clientes = repositorio.ListarTodo().Result;

            Assert.Collection(
                clientes,
                c => Assert.Equal("1234567890", c.Identificacion),
                c => Assert.Equal("0987654321", c.Identificacion));
        }

    }
}
