using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class ReportDetailConfig : IEntityTypeConfiguration<ReportDetail>
{
    public void Configure(EntityTypeBuilder<ReportDetail> builder)
    {
        builder.ToTable("ReportDetails");
        builder.HasKey(x => new { x.ReportID, x.MaterialID });
        builder.HasOne(x => x.Report).WithMany(x => x.ReportDetails).HasForeignKey(x => x.ReportID);
        builder.HasOne(x => x.Material).WithMany(x => x.ReportDetails).HasForeignKey(x => x.MaterialID);
    }
}
