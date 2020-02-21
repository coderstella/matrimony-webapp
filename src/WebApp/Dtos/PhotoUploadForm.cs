using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Dtos
{
    public class PhotoUploadForm
    {
        public List<IFormFile> Files { get; set; }
        public Guid portfolioId { get; set; }
    }
}
