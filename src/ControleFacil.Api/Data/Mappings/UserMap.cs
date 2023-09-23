using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFacil.Api.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user")
            .HasKey(u => u.Id);
            
            builder.Property(u => u.Email)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(u => u.Password)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(u => u.RegisterDate)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(u => u.InactivationDate)
            .HasColumnType("timestamp");
        }
    }
}