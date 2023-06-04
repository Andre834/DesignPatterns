using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.DataBase.DataBase
{
    public static class Extensions
    {
        public static void AddContextInMemoryDatabase<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddDbContextPool<TDbContext>(options => options.UseInMemoryDatabase(nameof(TDbContext)));

            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();

            services.BuildServiceProvider().GetRequiredService<TDbContext>().Database.EnsureDeleted();
        }

        public static void AddContextSqlServer<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContextPool<TDbContext>(options => options.UseSqlServer(connectionString));

            services.BuildServiceProvider().GetRequiredService<TDbContext>().Database.Migrate();
        }

        public static object[] PrimaryKey(this DbContext context, object entity)
        {
            return context.Model.FindEntityType(entity.GetType()).FindPrimaryKey().Properties.Select(property => entity.GetType().GetProperty(property.Name).GetValue(entity)).ToArray();
        }
    }

}
