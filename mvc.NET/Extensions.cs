using mvc.NET.Dtos;
using mvc.NET.Models;

namespace mvc.NET
{

    public static class Extensions
    {
       // аргументом принимает item по контексту к которому вызван метод
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreateDate = item.CreateDate
            };
        }
    }
}