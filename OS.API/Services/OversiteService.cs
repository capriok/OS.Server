﻿using Microsoft.EntityFrameworkCore;
using OS.API.Models.Oversite;
using OS.API.Services.Interfaces;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services
{
    public class OversiteService : IOversiteService
    {

        private readonly IOversiteRepository _oversiteRepository;

        public OversiteService(IOversiteRepository oversiteRepository)
        {
            _oversiteRepository = oversiteRepository;
        }

        public async Task<List<OversiteModel>> GetAllAsync()
        {
            IQueryable<OversiteEntity> query = _oversiteRepository.GetQueryable();

            return await query.Select(os => new OversiteModel
            {
                Id = os.Id,
                Title = os.Title,
                Website = os.Website,
                Category = os.Category,
                Severity = os.Severity
            })
            .ToListAsync();
        }

        public async Task<OversiteEntity> GetOneEntityAsync(int Id)
        {
            return await _oversiteRepository.FindByIdAsync(Id);
        }
    }
}
