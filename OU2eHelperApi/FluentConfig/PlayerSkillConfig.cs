using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.FluentConfig
{
    public class PlayerSkillConfig : IEntityTypeConfiguration<PlayerSkill>
    {
        public void Configure(EntityTypeBuilder<PlayerSkill> builder)
        {

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Type)
                .HasMaxLength(50);
            builder.Property(s => s.Notes)
                .HasMaxLength(500);
            builder.Ignore(s => s.AdvancementsList);

            builder.HasOne<BaseSkill>(s => s.BaseSkill);

        }
    }
}
