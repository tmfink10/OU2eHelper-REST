using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public class PlayerAbilityRepository : IPlayerAbilityRepository
    {
        private readonly OU2eHelperContext _dbContext;

        public PlayerAbilityRepository(OU2eHelperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerAbility>> SearchPlayerAbilities(string search)
        {
            IQueryable<PlayerAbility> query = _dbContext.PlayerAbilities;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(a => a.BaseAbility.Name.Contains(search) || a.BaseAbility.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PlayerAbility>> GetPlayerAbilities()
        {
            return await _dbContext.PlayerAbilities.ToArrayAsync();
        }

        public async Task<PlayerAbility> GetPlayerAbility(int playerAbilityId)
        {
            return await _dbContext.PlayerAbilities
                .FirstOrDefaultAsync(a => a.Id == playerAbilityId);
        }

        public async Task<PlayerAbility> AddPlayerAbility(PlayerAbility playerAbility)
        {
            var result = await _dbContext.PlayerAbilities.AddAsync(playerAbility);
            _dbContext.Entry(playerAbility.BaseAbility).State = EntityState.Unchanged;
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PlayerAbility> UpdatePlayerAbility(PlayerAbility playerAbility)
        {
            var result = await _dbContext.PlayerAbilities
                .FirstOrDefaultAsync(a => a.Id == playerAbility.Id);

            if (result != null)
            {
                result.BaseAbility = playerAbility.BaseAbility;
                result.Notes = playerAbility.Notes;
                result.Tier = playerAbility.Tier;
                result.Supports = playerAbility.Supports;
                result.Type = playerAbility.Type;

                _dbContext.PlayerAbilities.Update(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PlayerAbility> DeletePlayerAbility(int playerAbilityId)
        {
            var result = await _dbContext.PlayerAbilities
                .FirstOrDefaultAsync(a => a.Id == playerAbilityId);

            if (result != null)
            {
                _dbContext.PlayerAbilities.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
