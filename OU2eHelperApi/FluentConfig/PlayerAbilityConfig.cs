using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.FluentConfig
{
    public class PlayerAbilityConfig : IEntityTypeConfiguration<PlayerAbility>
    {
        public void Configure(EntityTypeBuilder<PlayerAbility> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Type)
                .HasMaxLength(50);
            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            builder.HasOne<BaseAbility>(a => a.BaseAbility);
        }
    }
}
