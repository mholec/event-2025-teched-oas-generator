using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Contracts
{
    public partial class UpdateWorkshopRequest
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
    }
}
