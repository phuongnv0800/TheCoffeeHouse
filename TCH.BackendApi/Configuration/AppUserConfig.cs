﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration;

public class AppUserConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
        builder.HasMany(x => x.UserBranches).WithOne(x => x.User).HasForeignKey(x => x.UserId);
    }
}