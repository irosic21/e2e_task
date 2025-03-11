using e2e_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(e => e.Id).HasName("PK_Employee");

            builder.Property(e => e.Id)
                .HasColumnName("ID_employee");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Surname)
                .HasColumnName("surname")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasMaxLength(200);

            builder.Property(e => e.Gender)
                .HasColumnName("gender")
                .HasMaxLength(10);

            builder.Property(e => e.YearOfBirth)
                .HasColumnName("year_of_birth");
        }
    }
}
