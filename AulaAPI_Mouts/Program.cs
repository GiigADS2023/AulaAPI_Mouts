using AulaAPI_Mouts.Data;
using AulaAPI_Mouts.Interface;
using AulaAPI_Mouts.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace AulaAPI_Mouts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* 
             * * Adiciona o contexto de banco de dados ao contêiner de DI.
             * 
             * * Especifica que o contexto de banco de dados usará um banco de dados em memória.
             */
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("TodoContext")));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }        
    }
}
