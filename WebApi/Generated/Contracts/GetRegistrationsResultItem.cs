using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Contracts
{
    public partial class GetRegistrationsResultItem
    {
        public Guid Id { get; set; }
        public string WorkshopId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
