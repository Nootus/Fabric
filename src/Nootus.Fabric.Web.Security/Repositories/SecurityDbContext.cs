//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityDbContext.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Entity Framework DB Context for security related tables
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Repositories
{
    using Nootus.Fabric.Web.Security.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    public class SecurityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options)
        {
        }

        public DbSet<IdentityClaimEntity> IdentityClaims { get; set; }

        public DbSet<IdentityPageEntity> IdentityPages { get; set; }

        public DbSet<IdentityMenuPageEntity> IdentityMenuPages { get; set; }

        public DbSet<IdentityPageClaimEntity> IdentityPageClaims { get; set; }

        public DbSet<IdentityRoleHierarchyEntity> IdentityRoleHierarchies { get; set; }

        public DbSet<IdentityCompanyClaimEntity> IdentityCompanyClaims { get; set; }

        public DbSet<UserProfileEntity> UserProfiles { get; set; }

        public DbSet<UserCompanyEntity> UserCompanies { get; set; }

        public DbSet<CompanyEntity> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRoleHierarchyEntity>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ChildRoleId });
            });

            builder.Entity<UserCompanyEntity>(entity =>
            {
                entity.HasKey(e => new { e.UserProfileId, e.CompanyId });
            });

            base.OnModelCreating(builder);

            // renaming identity tables
            builder.Entity<ApplicationUser>().ToTable("IdentityUser", "security");
            builder.Entity<ApplicationRole>().ToTable("IdentityRole", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("IdentityRoleClaim", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("IdentityUserClaim", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("IdentityUserLogin", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("IdentityUserRole", "security");
        }
    }
}
