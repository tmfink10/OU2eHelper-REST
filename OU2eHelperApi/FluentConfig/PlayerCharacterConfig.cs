using Microsoft.EntityFrameworkCore;
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
            //Notes intentionally omitted to be nvarchar(max)

            builder.HasOne<PlayerAttribute>(c => c.Strength);
            builder.HasOne<PlayerAttribute>(c => c.Perception);
            builder.HasOne<PlayerAttribute>(c => c.Empathy);
            builder.HasOne<PlayerAttribute>(c => c.Willpower);
            builder.HasMany<PlayerSkill>(c => c.Skills);
            builder.HasMany<PlayerAbility>(c => c.Abilities);
            builder.HasMany<PlayerTrainingValue>(c => c.TrainingValues);

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
