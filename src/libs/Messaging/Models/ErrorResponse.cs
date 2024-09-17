using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Models
{
  // istek sırasında oluşan hataya ait mesajı döndüren sınıf
  public record ErrorResponse(string errorMessage);
 
}
