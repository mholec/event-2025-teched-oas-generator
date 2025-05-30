using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Contracts
{
    public partial class GetWorkshopsResult
    {
        public int TotalItems { get; set; }
        public List<GetWorkshopsResultItem> Items { get; set; }
    }
}
