using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodBurger.Application.DTOs
{
    public class MenuResponse
    {
        public List<ItemDto> Sandwiches { get; set; }
        public List<ItemDto> Extras { get; set; }
    }
}
