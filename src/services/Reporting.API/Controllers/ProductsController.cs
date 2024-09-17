using MassTransit;
using Messaging.Dtos;
using Messaging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reporting.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    // Http Client gibi düşünelim
    private readonly IRequestClient<GetProductsRequest> client;

    public ProductsController(IRequestClient<GetProductsRequest> client)
    {
      this.client = client;
    }


    [HttpPost]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
      var request = new GetProductsRequest();

      var response = await  this.client.GetResponse<SuccessResponse<List<ProductDto>>,ErrorResponse>(request,cancellationToken);


      if(response.Is(out Response<SuccessResponse<List<ProductDto>>> successRes))
      {
        return Ok(successRes.Message.Data);
      }
      else if(response.Is(out  Response<ErrorResponse> errorRes))
      {
        return Ok(errorRes.Message);
      }

      return Ok();
    }
  }
}
