using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChatMessageConfigurations : IEntityTypeConfiguration<ChatMessageFile>
{
    public void Configure(EntityTypeBuilder<ChatMessageFile> builder)
    {
        builder.ToTable("ChatMessageFiles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.FileUrl).IsRequired();

        builder.Property(x => x.MimeType).IsRequired();

        // Relations
        builder
            .HasOne(x => x.ChatMessage)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.ChatMessageId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
