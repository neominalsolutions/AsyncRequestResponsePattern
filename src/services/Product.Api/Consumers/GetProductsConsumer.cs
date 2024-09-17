using MassTransit;
using Messaging.Dtos;
using Messaging.Models;

namespace Product.Api.Consumers
{
  public class GetProductsConsumer : IConsumer<GetProductsRequest>
  {
    // burada bir request karşılık bir response dönceğimizden ilgili Consume methodudan Respond döneriz
    public async Task Consume(ConsumeContext<GetProductsRequest> context)
    {
      // kendi api veri tabanından dynamic veri çek ve response olarak gönder.
      //var response = new SuccessResponse<List<ProductDto>>();
      //response.Data = new List<ProductDto>();
      //response.Data.Add(new ProductDto(1, "Ürün-2"));
      //response.Data.Add(new ProductDto(2, "Ürün-2"));


      var errorResponse = new ErrorResponse("Hata Meydana geldi");
     

      await context.RespondAsync(errorResponse);

    }
  }
}
