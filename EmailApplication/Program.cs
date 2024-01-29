using System.Net.Mail;
using EmailApplication.Domain;
using EmailApplication.Interface;
using EmailApplication.Service;

namespace EmailApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.Configure<SMTPConfig>(builder.Configuration.GetSection("SMTPConfig"));
            builder.Services.AddTransient<SmtpClient>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email")); ; ;
            }
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
