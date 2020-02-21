using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class PortfolioType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Portfolio> Portfolios { get; set; }
    }
}
