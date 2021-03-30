using OS.API.Models.Oversite;
using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface IOversiteManager
    {
        Task<List<OversiteModel>> GetAllAsync();
        Task<OversiteEntity> GetOneEntityAsync(int Id);

    }
}