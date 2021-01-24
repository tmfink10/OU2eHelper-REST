﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.FluentConfig
{
    public class PlayerCharacterConfig : IEntityTypeConfiguration<PlayerCharacter>
    {
        public void Configure(EntityTypeBuilder<PlayerCharacter> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .HasMaxLength(60);
            builder.Property(c => c.LastName)
                .HasMaxLength(60);
            builder.Property(c => c.Sex)
                .HasMaxLength(20);

            builder.Ignore(c => c.Attributes);
            //builder.Ignore(c => c.StrengthService);
            //builder.Ignore(c => c.PerceptionService);
            //builder.Ignore(c => c.EmpathyService);
            //builder.Ignore(c => c.WillpowerService);
            
            //Notes intentionally omitted to be nvarchar(max)

            builder.HasMany<PlayerSkill>(c => c.Skills)
                .WithOne(s => s.PlayerCharacter);
            builder.HasMany<PlayerAbility>(c => c.Abilities)
                .WithOne(a => a.PlayerCharacter);
            builder.HasMany<PlayerTrainingValue>(c => c.TrainingValues)
                .WithOne(t => t.PlayerCharacter);
            builder.HasMany<PlayerAttribute>(c => c.PlayerAttributes)
                .WithOne(a => a.PlayerCharacter)
                .HasForeignKey(a => a.PlayerCharacterId);

            builder.HasData(new PlayerCharacter
            {
                Id = 1,
                FirstName = "Trevor",
                LastName = "Fink",
                Age = 35
            });
        }
    }
}
