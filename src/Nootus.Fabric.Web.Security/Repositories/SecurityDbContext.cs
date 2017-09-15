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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Nootus.Fabric.Web.Security.Entities;

    public class SecurityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClaimEntity> Claims { get; set; }

        public DbSet<PageEntity> Pages { get; set; }

        public DbSet<MenuPageEntity> MenuPages { get; set; }

        public DbSet<PageClaimEntity> PageClaims { get; set; }

        public DbSet<RoleHierarchyEntity> RoleHierarchies { get; set; }

        public DbSet<CompanyClaimEntity> CompanyClaims { get; set; }

        public DbSet<UserProfileEntity> UserProfiles { get; set; }

        public DbSet<UserCompanyEntity> UserCompanies { get; set; }

        public DbSet<CompanyEntity> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RoleHierarchyEntity>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ChildRoleId });
            });

            builder.Entity<UserCompanyEntity>(entity =>
            {
                entity.HasKey(e => new { e.UserProfileId, e.CompanyId });
            });

            base.OnModelCreating(builder);

            // renaming identity tables
            builder.Entity<ApplicationUser>().ToTable("User", "security");
            builder.Entity<ApplicationRole>().ToTable("Role", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "security");
        }
    }
}
