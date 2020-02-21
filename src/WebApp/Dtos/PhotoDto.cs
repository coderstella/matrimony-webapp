using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Dtos
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid PortfolioId { get; set; }
    }
}
