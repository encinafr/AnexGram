
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using System;
using System.Collections.Generic;

using System.Text;

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
    }
}
