
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Model.Domain.DbHelper;
using Persistence.DatabaseContext.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CommentsPerPhoto> CommentsPerPhoto { get; set; }
        public DbSet<LikesPerPhoto> LikesPerPhoto { get; set; }
        public DbSet<Photo> Photos { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //My custom filters
            AddMyFilters(ref builder);

            //My fluent api contraints
            new ApplicationUserConfig(builder.Entity<ApplicationUser>());
            new CommentsPerPhotoConfig(builder.Entity<CommentsPerPhoto>());
            new LikesPerPhotoConfig(builder.Entity<LikesPerPhoto>());
            new PhotoConfig(builder.Entity<Photo>());
        }

        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return base.SaveChangesAsync(cancellationToken);
        }



        private void MakeAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity
                    && (
                    x.State == EntityState.Added
                    || x.State == EntityState.Modified
                )
            );

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditEntity;

                if (entity != null)
                {
                    var date = DateTime.UtcNow;
                    string userId = null;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).Deleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;
                }
            }
        }

        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            #region SoftDeleted
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<CommentsPerPhoto>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<LikesPerPhoto>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<Photo>().HasQueryFilter(x => !x.Deleted);
            #endregion
        }
    }
}
