using MassTransit;
using Messaging.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{
  // get-products endpoint gibi görünebilir fakat endpoint çağırmak yerine ilgili api daki IConsumer servisinden implemente olmuş bir request method tetikleyeceğiz. Mantık olarak RPC Remote Procedure Call işlemine benzer.
  opt.AddRequestClient<GetProductsRequest>(new Uri("exchange:get-products"));

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitMqConn"));
    config.ConfigureEndpoints(context); // ilgili consumerları register ettirmeyi unutmayalım.
  });

});

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
