using OS.API.Models.Oversite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Managers.Interfaces
{
    public interface IOversiteManager
    {
        Task<List<OversiteModel>> GetRecentAsync();
        Task<OversiteModel> GetModelAsync(int oversiteId);
        Task<List<OversiteModel>> GetBySearchResultAsync(string searchResult);
        Task<OversiteModel> CreateAsync(OversiteFormData formData);
    }
}
