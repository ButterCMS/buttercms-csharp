using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButterCMS.Models
{
    public class PageMeta
    {
        public int Count { get; set; }
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }
    }
}
