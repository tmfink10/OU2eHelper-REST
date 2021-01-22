using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public class PlayerCharacterRepository : IPlayerCharacterRepository
    {
        private readonly OU2eHelperContext _dbContext;

        public PlayerCharacterRepository(OU2eHelperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerCharacter>> SearchPlayerCharacters(string search)
        {
            IQueryable<PlayerCharacter> query = _dbContext.PlayerCharacters;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters()
        {
            return await _dbContext.PlayerCharacters.ToArrayAsync();
        }

        public async Task<PlayerCharacter> GetPlayerCharacter(int playerCharacterId)
        {
            return await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(p => p.Id == playerCharacterId);
        }

        public async Task<PlayerCharacter> AddPlayerCharacter(PlayerCharacter playerCharacter)
        {
            var result = await _dbContext.PlayerCharacters.AddAsync(playerCharacter);
            _dbContext.Entry(playerCharacter.Strength.BaseAttribute).State = EntityState.Unchanged;
            _dbContext.Entry(playerCharacter.Perception.BaseAttribute).State = EntityState.Unchanged;
            _dbContext.Entry(playerCharacter.Empathy.BaseAttribute).State = EntityState.Unchanged;
            _dbContext.Entry(playerCharacter.Willpower.BaseAttribute).State = EntityState.Unchanged;
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PlayerCharacter> UpdatePlayerCharacter(PlayerCharacter playerCharacter)
        {
            var result = await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(p => p.Id == playerCharacter.Id);

            if (result != null)
            {
                result.FirstName = playerCharacter.FirstName;
                result.LastName = playerCharacter.LastName;
                result.Age = playerCharacter.Age;
                result.Sex = playerCharacter.Sex;
                result.Attributes = playerCharacter.Attributes;
                result.Strength = playerCharacter.Strength;
                result.Perception = playerCharacter.Perception;
                result.Empathy = playerCharacter.Empathy;
                result.Willpower = playerCharacter.Willpower;
                result.Skills = playerCharacter.Skills;
                result.Abilities = playerCharacter.Abilities;
                result.TrainingValues = playerCharacter.TrainingValues;
                result.SurvivalPoints = playerCharacter.SurvivalPoints;
                result.GestaltLevel = playerCharacter.GestaltLevel;
                result.CargoCapacity = playerCharacter.CargoCapacity;
                result.CompetencePoints = playerCharacter.CompetencePoints;
                result.HealthPoints = playerCharacter.HealthPoints;
                result.DamageThreshold = playerCharacter.DamageThreshold;
                result.Morale = playerCharacter.Morale;
                result.Notes = playerCharacter.Notes;

                _dbContext.PlayerCharacters.Update(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }



        public async Task<PlayerCharacter> DeletePlayerCharacter(int playerCharacterId)
        {
            var result = await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(p => p.Id == playerCharacterId);

            if (result != null)
            {
                _dbContext.PlayerCharacters.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
