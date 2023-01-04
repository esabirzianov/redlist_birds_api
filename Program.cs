using redlist_birds_api;
using Npgsql;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.MethodsForController;

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
        builder.Services.AddSingleton<IDbConnectionHelper, DbConnectionHelper> ();
        builder.Services.AddSingleton<IRedListRequests, RedListRequests> ();
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