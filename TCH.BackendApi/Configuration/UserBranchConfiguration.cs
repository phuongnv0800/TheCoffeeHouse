using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            builder.ToTable("UserBranchs");
            builder.HasKey(x => new { x.UserId, x.BranchID});
            builder.HasOne(x => x.Branch).WithMany(x => x.UserBranches).HasForeignKey(x => x.BranchID);
            builder.HasOne(x => x.User).WithMany(x => x.UserBranches).HasForeignKey(x => x.UserId);
        }
    }
}
