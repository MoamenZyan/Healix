using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healix.Infrastructure.Configurations.EntitiesConfigurations
{
    public class ChatBotConfigurations : IEntityTypeConfiguration<ChatBot>
    {
        public void Configure(EntityTypeBuilder<ChatBot> builder)
        {
            builder.ToTable("ChatBots");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            // Relations
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.ChatBots)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
