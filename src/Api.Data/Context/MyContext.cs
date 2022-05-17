using System;
using System.Threading.Tasks;
using api.Domain.Entitiers;
using Api.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } // control + . para incluir a referencia
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var usermap = new UserMap();
            usermap.Configure(modelBuilder.Entity<UserEntity>());

        }

        internal Task SaveChangeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
