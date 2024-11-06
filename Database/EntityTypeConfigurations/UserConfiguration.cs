using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(c => c.Resume)
                   .WithOne(e => e.Profile)
                   .HasForeignKey<Resume>(b => b.UserId)
                   .IsRequired();
        }
    }
}
