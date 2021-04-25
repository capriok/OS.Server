using Microsoft.AspNetCore.Http;
using OS.API.Models.Oversite;
using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Managers.Interfaces
{
    public interface IOversiteManager
    {
        Task<List<OversiteModel>> GetAllAsync();
        Task<OversiteModel> GetEntityAsync(int id);
        Task<List<OversiteModel>> GetBySearchResultAsync(string searchResult);
        Task<OversiteModel> CreateAsync(OversiteFormData formData);
    }
}