using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Members.ViewModels.Portfolio.Forms;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;
using WebApp.Data.Repository;
using WebApp.Dtos;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioTypeRepository _portfolioTypeRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PortfolioService> _logger;
        public PortfolioService(IPortfolioRepository portfolioRepository, IPortfolioTypeRepository portfolioTypeRepository, IPhotoRepository photoRepository, IMapper mapper, ILogger<PortfolioService> logger)
        {
            _portfolioRepository = portfolioRepository;
            _portfolioTypeRepository = portfolioTypeRepository;
            _photoRepository = photoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PortfolioDetailsDto>> GetAllAsync()
        {
            try
            {
                var portfolios = await _portfolioRepository.GetAllAsync();
                if (portfolios != null)
                    return _mapper.Map<IEnumerable<PortfolioDetailsDto>>(portfolios);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while getting all portfolios list: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<PortfolioDetailsDto> GetByUserId(string userId)
        {
            try
            {
                var portfolio = await _portfolioRepository.GetByUserId(userId);
                if (portfolio != null)
                {                   
                    return _mapper.Map<PortfolioDetailsDto>(portfolio);
                }                    

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while getting all portfolios list by user id: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<PortfolioDetailsDto> GetById(string id)
        {
            try
            {
                var portfolio = await _portfolioRepository.GetOneAsync(id);
                if (portfolio == null)
                    return null;

                return _mapper.Map<PortfolioDetailsDto>(portfolio);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PortfolioFormViewModel> EditById(string id)
        {
            try
            {
                var portfolio = await _portfolioRepository.GetOneAsync(id);
                if (portfolio == null)
                    return null;

                return _mapper.Map<PortfolioFormViewModel>(portfolio);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while getting single portfolio by id: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SavePortfolioAsync(PortfolioFormViewModel createForm)
        {
            try
            {
                var portfolio = _mapper.Map<Portfolio>(createForm);
                var portfolioId = await _portfolioRepository.SaveAsync(portfolio);
                if (string.IsNullOrEmpty(portfolioId))
                    return null;

                return portfolioId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while saving single portfolio: {0} at {1}", ex.Message, DateTime.Now.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(PortfolioFormViewModel createForm)
        {
            try
            {
                var portfolio = _mapper.Map<Portfolio>(createForm);
                var result = await _portfolioRepository.UpdateAsync(portfolio);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while updating single portfolio {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                return await _portfolioRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while deleting single portfolio: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        // Portfolio Type Services
        public async Task<IEnumerable<PortfolioTypeDto>> GetAllTypesAsync()
        {
            try
            {
                var portfolioTypes = await _portfolioTypeRepository.GetAllAsync();
                if (portfolioTypes != null)
                    return _mapper.Map<IEnumerable<PortfolioTypeDto>>(portfolioTypes);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Portfolio Service while getting all portfolio types: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        // Photo upload
        public async Task<IEnumerable<FileUploadDto>> SavePhotos(IEnumerable<FileUploadDto> files, Guid portfolioId)
        {
            try
            {
                if (files == null)
                    return null;

                files = files.Select(f => new FileUploadDto { Name = f.Name, Title = f.Title, Type = f.Type, DirectoryName = f.DirectoryName, CreatedDate = f.CreatedDate, PortfolioId = portfolioId });

                var photos = _mapper.Map<IEnumerable<Photo>>(files);
                var result = await _photoRepository.SaveAllAsync(photos);
                if (result == null)
                    return null;

                return _mapper.Map<IEnumerable<FileUploadDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Portfolio service while uploading photos: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<FileUploadDto>> GetAllPhotosAsync(string portfolioId)
        {
            try
            {
                var uploadPhotos = await _photoRepository.GetAllPhotosAsync(portfolioId);
                if (uploadPhotos == null)
                    return null;

                return _mapper.Map<IEnumerable<FileUploadDto>>(uploadPhotos);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePhotoById(string photoId)
        {
            try
            {
                return await _photoRepository.DeleteAsync(photoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while deleting single portfolio: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
