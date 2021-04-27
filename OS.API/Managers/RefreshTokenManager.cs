using Microsoft.Extensions.Logging;
using OS.API.Managers.Interfaces;
using OS.API.Models.RefreshToken;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OS.API.Managers
{
    public class RefreshTokenManager : IRefreshTokenManager
    {
        private readonly ILogger<RefreshTokenManager> _Logger;
        private readonly IRefreshTokenRepository _RefreshTokenRepository;

        public RefreshTokenManager(ILogger<RefreshTokenManager> logger, IRefreshTokenRepository refreshTokenRepository)
        {
            _Logger = logger;
            _RefreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshTokenModel> GetOneByUserIdAsync(int userId)
        {
            var tokenEntity = await _RefreshTokenRepository.FindByUserId(userId);

            if (tokenEntity is null)
            {
                return null;
            }

            return ConvertEntityToModel(tokenEntity);
        }

        public async Task<RefreshTokenModel> GetOneByTokenAsync(string token)
        {
            var tokenEntity = await _RefreshTokenRepository.FindByToken(token);

            if (tokenEntity is null)
            {
                return null;
            }

            return ConvertEntityToModel(tokenEntity);
        }

        public async Task<RefreshTokenModel> CreateAsync(RefreshTokenModel token)
        {
            var tokenEntity = new RefreshTokenEntity
            {
                Token = token.Token,
                UserId = token.UserId
            };

            var createdToken = await _RefreshTokenRepository.AddAsync(tokenEntity);

            return ConvertEntityToModel(createdToken);
        }

        public async Task<RefreshTokenModel> UpdateAsync(string oldToken, RefreshTokenModel token)
        {
            var tokenEntity = await _RefreshTokenRepository.FindByToken(oldToken);

            if (tokenEntity is null)
            {
                return null;
            }

            tokenEntity.Id = tokenEntity.Id;
            tokenEntity.UserId = tokenEntity.UserId;
            tokenEntity.Token = token.Token;

            var updatedToken = await _RefreshTokenRepository.UpdateAsync(tokenEntity);

            return ConvertEntityToModel(updatedToken);
        }

        private Converter<RefreshTokenEntity, RefreshTokenModel> converter = ConvertEntityToModel;
        private static RefreshTokenModel ConvertEntityToModel(RefreshTokenEntity t)
        {
            return new RefreshTokenModel
            {
                Token = t.Token,
                UserId = t.UserId
            };
        }
    }
}
