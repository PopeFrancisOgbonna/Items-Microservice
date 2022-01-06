using PlayCatalog.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PlayCatalog.Services.DataTransferOjects.DTOs;

namespace PlayCatalog.Services.DataTransferOjects
{
    public static class Extensions
    {
        public static ItemDTO AsDTO(this Item item)
        {
            return new ItemDTO(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}
