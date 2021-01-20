using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.FluentConfig
{
    public class PlayerTrainingValueConfig : IEntityTypeConfiguration<PlayerTrainingValue>
    {
        public void Configure(EntityTypeBuilder<PlayerTrainingValue> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Type)
                .HasMaxLength(50);
            builder.Property(t => t.Notes)
                .HasMaxLength(500);

            builder.HasOne<BaseTrainingValue>(t => t.BaseTrainingValue);
        }
    }
}
