using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButterCMS.Models
{
    public class PagesResponse<T>
    {
        public PageMeta Meta { get; set; }
        public IEnumerable<Page<T>> Data { get; set; }
    }
}
