using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Devsu.Cuentas.Aplicacion.Servicios;
using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using Devsu.Cuentas.Infraestructura.Datos.Contexto;
using Devsu.Cuentas.Infraestructura.Datos.Repositorios;
using Devsu.Cuentas.Infraestructura.Repositorios;
using Devsu.Cuentas.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devsu.Cuentas.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Devsu.Cuentas.WebAPI", Version = "v1" });
            });

            services.AddDbContext<DbContext, CuentasDevsuDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevsuCuentasDBConnection")));

            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepositorio<Cliente>, Repositorio<Cliente>>();
            services.AddScoped<IRepositorio<Cuenta>, Repositorio<Cuenta>>();
            services.AddScoped<IRepositorio<Movimiento>, Repositorio<Movimiento>>();
            services.AddScoped<IRepositorioReportes, RepositorioReportes>();
            services.AddScoped<IRepositorioRetiroDiarioPersona, RepositorioRetiroDiarioPersona>();

            services.AddScoped<IServicioListar<DTOClienteListado>, ClienteServicio>();
            services.AddScoped<IServicioCrear<DTOGuardarCliente>, ClienteServicio>();
            services.AddScoped<IServicioEditar<DTOGuardarCliente>, ClienteServicio>();
            services.AddScoped<IServicioBorrar, ClienteServicio>();

            services.AddScoped<IServicioListar<DTOCuentaListado>, CuentaServicio>();
            services.AddScoped<IServicioCrear<DTOGuardarCuenta>, CuentaServicio>();
            services.AddScoped<IServicioEditar<DTOGuardarCuenta>, CuentaServicio>();
            services.AddScoped<IServicioBorrar, CuentaServicio>();

            services.AddScoped<IServicioListar<DTOMovimientoListado>, MovimientoServicio>();
            services.AddScoped<IServicioCrear<DTOGuardarMovimiento>, MovimientoServicio>();

            services.AddScoped<IServicioReportes, ReporteServicio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Devsu.Cuentas.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseErrorHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
