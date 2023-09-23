using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFacil.Api.Data.Mappings
{
    public class ToReceiveMap : IEntityTypeConfiguration<ToReceive>
    {
        public void Configure(EntityTypeBuilder<ToReceive> builder)
        {
            builder.ToTable("toReceive")
            .HasKey(t => t.Id);

            builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(fk => fk.UserId);

            builder.HasOne(t => t.NatureRelease)
            .WithMany()
            .HasForeignKey(t => t.NatureReleaseId);

            builder.Property(t => t.Description)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(t => t.Details)
            .HasColumnType("VARCHAR");

            builder.Property(t => t.OriginalValue)
            .HasColumnType("decimal")
            .IsRequired();

            builder.Property(t => t.ReceivedValue)
            .HasColumnType("decimal");

            builder.Property(t => t.RegisterDate)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(t => t.ExpirationDate)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(t => t.ReferenceDate)
            .HasColumnType("timestamp");

            builder.Property(t => t.ReceivedDate)
            .HasColumnType("timestamp");

            builder.Property(t => t.InactivationDate)
            .HasColumnType("timestamp");
        }
    }
}