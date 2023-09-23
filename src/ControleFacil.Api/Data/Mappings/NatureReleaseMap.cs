using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFacil.Api.Data.Mappings
{
    public class NatureReleaseMap : IEntityTypeConfiguration<NatureRelease>
    {
        public void Configure(EntityTypeBuilder<NatureRelease> builder)
        {
            builder.ToTable("naturerelease")
            .HasKey(n => n.Id);

            builder.HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(fk => fk.UserId);

            builder.Property(n => n.Description)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(n => n.Details)
            .HasColumnType("VARCHAR");

            builder.Property(n => n.RegisterDate)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(n => n.InactivationDate)
            .HasColumnType("timestamp");
        }
    }
}