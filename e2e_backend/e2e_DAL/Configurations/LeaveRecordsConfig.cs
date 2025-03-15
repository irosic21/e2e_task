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
    public class LeaveRecordsConfig : IEntityTypeConfiguration<LeaveRecords>
    {
        public void Configure(EntityTypeBuilder<LeaveRecords> builder)
        {
            builder.ToTable("LeaveRecords");

            builder.HasKey(l => l.Id).HasName("PK_LeaveRecords");

            builder.Property(l => l.Id).HasColumnName("ID_leave");
            builder.Property(l => l.EmployeeId).HasColumnName("ID_employee");
            builder.Property(l => l.VacationDays).HasColumnName("vacation_days");
            builder.Property(l => l.FreeDays).HasColumnName("free_days");
            builder.Property(l => l.PaidLeaveDays).HasColumnName("paid_leave_days");
        }
    }
}
