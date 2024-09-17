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
  // get-products endpoint gibi g�r�nebilir fakat endpoint �a��rmak yerine ilgili api daki IConsumer servisinden implemente olmu� bir request method tetikleyece�iz. Mant�k olarak RPC Remote Procedure Call i�lemine benzer.
  opt.AddRequestClient<GetProductsRequest>(new Uri("exchange:get-products"));

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitMqConn"));
    config.ConfigureEndpoints(context); // ilgili consumerlar� register ettirmeyi unutmayal�m.
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
