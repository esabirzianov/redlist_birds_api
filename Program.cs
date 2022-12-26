using redlist_birds_api;
using redlist_birds_api.MethodsForRequests;
using redlist_birds_api.Interfaces;
using Npgsql;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IBirdInformationRequests, BirdInformationRequests>();
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