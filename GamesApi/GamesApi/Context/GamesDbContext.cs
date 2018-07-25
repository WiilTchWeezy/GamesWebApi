using GamesApi.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Context
{
    internal class GamesDbContextInitializer : IDatabaseInitializer<GamesDbContext>
    {
        public void InitializeDatabase(GamesDbContext context)
        {

        }
    }

    public class GamesDbContext : DbContext
    {
        const string connectionString = "workstation id=GamesApi.mssql.somee.com;packet size=4096;user id=TarcisioVitor_SQLLogin_1;pwd=cj5rhnwpkq;data source=GamesApi.mssql.somee.com;persist security info=False;initial catalog=GamesApi";
        #region Infra
        public GamesDbContext() : base(connectionString)
        {
            Database.SetInitializer(new GamesDbContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();
        }


        public Task Add<TEntity>(TEntity entity) where TEntity : class
        {
            return Task.Run(() =>
            {
                Set<TEntity>().Attach(entity);
                Entry(entity).State = EntityState.Added;
            });
        }

        public Task Edit<TEntity>(TEntity entity) where TEntity : class
        {
            return Task.Run(() =>
            {
                Set<TEntity>().Attach(entity);
                Entry(entity).State = EntityState.Modified;
            });
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Attach(entity);
            Entry(entity).State = EntityState.Deleted;
        }
        #endregion

        public DbSet<Score> Score { get; set; }
        public DbSet<User> User { get; set; }
    }
}
