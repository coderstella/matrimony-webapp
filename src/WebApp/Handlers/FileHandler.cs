using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Dtos;

namespace WebApp.Handlers
{
    public class FileHandler : IFileHandler
    {
        private readonly IWebHostEnvironment _hostingEnv;
        public FileHandler(IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public IEnumerable<FileUploadDto> UploadFiles(List<IFormFile> files, string Email)
        {
            if (files == null || string.IsNullOrWhiteSpace(Email))
                return null;

            try
            {
                List<FileUploadDto> uploadedFiles = new List<FileUploadDto>();

                if (files != null && files.Count > 0)
                {
                    var uploadFolder = $"{_hostingEnv.WebRootPath}{Path.DirectorySeparatorChar}media{Path.DirectorySeparatorChar}upload{Path.DirectorySeparatorChar}{MD5Hash(Email)}";
                    var userDirectory = Directory.CreateDirectory(uploadFolder);

                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileExtention = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var tempfileName = $"{Guid.NewGuid()}{fileExtention}";
                            var filePath = Path.Combine(userDirectory.FullName, tempfileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            FileInfo fi = new FileInfo(filePath);
                            if (fi == null)
                                return null;

                            var uploadedFile = new FileUploadDto
                            {
                                Type = file.ContentType,
                                CreatedDate = fi.CreationTime,
                                Name = tempfileName,
                                Title = Path.GetFileNameWithoutExtension(file.FileName),
                                DirectoryName = userDirectory.Name
                            };

                            uploadedFiles.Add(uploadedFile);
                        }
                    }
                }
                return uploadedFiles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFile(string name, string directory)
        {

            var folderPath = $"{_hostingEnv.WebRootPath}{Path.DirectorySeparatorChar}media{Path.DirectorySeparatorChar}upload{Path.DirectorySeparatorChar}{directory}";

            //string directoryFolder = Path.Combine(Directory.GetCurrentDirectory(), @"~/media/upload/", directory);
            string filePath = Path.Combine(folderPath, name);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);

                if (!File.Exists(filePath))
                    return true;
                else
                    return false;
            }
            return false;
        }

        private string RenameFile(string fileName)
        {
            return fileName.Replace(" ", "-").Replace("'", "-").Replace(".", "-").ToLower();
        }

        private static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
