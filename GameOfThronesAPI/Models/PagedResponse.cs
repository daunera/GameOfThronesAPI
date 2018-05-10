using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesAPI.Models
{
    /// <summary>
    /// An object for paged api query response to replace the ugly Object[]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T>
    {
        public T result;
        public Linker links;

        public PagedResponse()
        {
            result = default(T);
            links = new Linker();
        }
    }
}
