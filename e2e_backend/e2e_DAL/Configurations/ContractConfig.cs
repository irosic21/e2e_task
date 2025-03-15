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
    public class ContractConfig : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract");

            builder.HasKey(c => c.Id).HasName("PK_Contract");

            builder.Property(c => c.Id).HasColumnName("ID_contract");
            builder.Property(c => c.EmployeeId).HasColumnName("ID_employee");
            builder.Property(c => c.DateOfEmployment).HasColumnName("date_of_employment");
            builder.Property(c => c.ContractType).HasColumnName("contract_type").HasMaxLength(50);
            builder.Property(c => c.DurationOfContract).HasColumnName("duration_of_contract");
            builder.Property(c => c.Department).HasColumnName("department").HasMaxLength(100);
        }
    }
}
