using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string DirectoryName { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid PortfolioId { get; set; }
        [ForeignKey("PortfolioId")]
        public Portfolio Portfolio { get; set; }
    }
}
