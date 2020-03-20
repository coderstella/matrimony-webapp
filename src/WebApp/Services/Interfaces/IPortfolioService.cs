using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Members.ViewModels.Portfolio.Forms;
using WebApp.Dtos;

namespace WebApp.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<IEnumerable<PortfolioDetailsDto>> GetAllAsync();
        Task<PortfolioDetailsDto> GetByUserId(string userId);
        Task<PortfolioDetailsDto> GetById(string id);
        Task<PortfolioFormViewModel> EditById(string id);
        Task<string> SavePortfolioAsync(PortfolioFormViewModel portfolioForm);
        Task<bool> UpdateAsync(PortfolioFormViewModel createForm);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<PortfolioTypeDto>> GetAllTypesAsync();
        Task<IEnumerable<FileUploadDto>> SavePhotos(IEnumerable<FileUploadDto> files, Guid portfolioId);
        Task<IEnumerable<FileUploadDto>> GetAllPhotosAsync(string portfolioId);
        Task<bool> DeletePhotoById(string photoId);
    }
}
