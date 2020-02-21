using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Dtos;

namespace WebApp.Data.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAllPhotosAsync(string portfolioId);
        Task<IEnumerable<Photo>> SaveAllAsync(IEnumerable<Photo> photos);
        Task<string> SaveAsync(Photo photo);
        Task<bool> DeleteAsync(string photoId);
    }
}
