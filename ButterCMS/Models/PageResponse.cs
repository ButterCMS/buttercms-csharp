using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButterCMS.Models
{
    public class PageResponse<T>
    {
        public Page<T> Data { get; set; }
    }
}
