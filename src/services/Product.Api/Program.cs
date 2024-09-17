using MassTransit;
using Product.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{

  // Consumer bulundu�u api da Add Consumer ile Consumer tan�m� yapmak zorunday�z yoksa async consumer serviceler tetiklenez.

  opt.AddConsumer<GetProductsConsumer>().Endpoint(x => x.Name = "get-products");

  opt.UsingRabbitMq((context, cfg) =>
  {
    cfg.Host(builder.Configuration.GetConnectionString("RabbitMqConn"));
    cfg.ConfigureEndpoints(context); // consumerlar� register et dinleyicileri a�.
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
