
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rakais_EL3lag.Models;

namespace Rakais_EL3lag
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<RakaizContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Rakaiz"))
            );
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<RakaizContext>().AddDefaultTokenProviders();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
