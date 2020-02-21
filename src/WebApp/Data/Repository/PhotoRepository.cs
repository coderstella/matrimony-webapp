using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;
using WebApp.Dtos;

namespace WebApp.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PhotoRepository> _logger;
        public PhotoRepository(ApplicationContext context, IMapper mapper, ILogger<PhotoRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Photo>> GetAllPhotosAsync(string portfolioId)
        {
            try
            {
                var uploadPhotos = await _context.Photos.Where(p => p.PortfolioId == Guid.Parse(portfolioId)).ToListAsync();
                if (uploadPhotos != null)
                    return uploadPhotos;

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Photo>> SaveAllAsync(IEnumerable<Photo> photos)
        {
            try
            {
                await _context.Photos.AddRangeAsync(photos);
                _context.SaveChanges();
                return photos;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in photo while getting all photos: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveAsync(Photo photo)
        {
            try
            {
                await _context.Photos.AddAsync(photo);
                _context.SaveChanges();
                return photo.Id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in photo while getting data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(string photoId)
        {
            try
            {
                var photo = _context.Photos.SingleOrDefault(p => p.Id == Guid.Parse(photoId));
                _context.Photos.Remove(photo);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
