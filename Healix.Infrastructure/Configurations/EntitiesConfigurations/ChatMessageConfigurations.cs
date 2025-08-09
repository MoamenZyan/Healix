using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healix.Infrastructure.Configurations.EntitiesConfigurations
{
    public class ChatMessageConfigurations : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.IsUser).IsRequired();

            // Relations
            builder
                .HasOne(x => x.Chat)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
