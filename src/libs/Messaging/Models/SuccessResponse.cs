using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Models
{
  // işlem başarılı olduğunda bir sonuç döndürmek için kullandığımız 
  public record SuccessResponse<TData>
  {
    public TData Data { get; set; }
  }
  
}
