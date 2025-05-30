using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Contracts
{
    public partial class GetRegistrationsResult
    {
        public int TotalItems { get; set; }
        public List<GetRegistrationsResultItem> Items { get; set; }
    }
}
