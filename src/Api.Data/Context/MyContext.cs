using System;
using System.Threading.Tasks;
using Api.Data.Mapping;
using Api.Domain.Entitiers;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

        }

        internal Task SaveChangeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
