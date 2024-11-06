using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityTypeConfigurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasMany(a => a.Applications)
                   .WithOne(b => b.Vacancy)
                   .HasForeignKey(c => c.VacancyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
