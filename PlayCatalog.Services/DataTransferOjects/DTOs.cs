using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayCatalog.Services.DataTransferOjects
{
    public class DTOs
    {
        public record ItemDTO (Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
       
        public record CreateItemDTO([Required(ErrorMessage ="Name is required")] string Name, [Required(ErrorMessage ="Provide a description")] string Description,
                [Required(ErrorMessage ="Add a price for Item")] [Range(2,1000,ErrorMessage ="Item Min. price is $2 and max is $1000")] decimal price);
       
        public record UpdateItemDTO([Required(ErrorMessage = "Name is required")] string Name, [Required(ErrorMessage = "Provide a description")] string Description,
                [Required(ErrorMessage = "Add a price for Item")][Range(2, 1000, ErrorMessage = "Item Min. price is $2 and max is $1000")] decimal price);
    }
}
