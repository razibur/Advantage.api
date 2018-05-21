using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;

namespace Advantage.API.Controllers
{
    internal class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }

        public PaginationResponse(IEnumerable<T> data, int pageIndex, int len)
        {
            Data = data.Skip((pageIndex-1) * len).Take(len).ToList();
            Total = data.Count();
        }
    }
}