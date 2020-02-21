using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Dtos;

namespace WebApp.Handlers
{
    public interface IFileHandler
    {
        IEnumerable<FileUploadDto> UploadFiles(List<IFormFile> files, string Email);
        bool RemoveFile(string name, string directory);
    }
}
