using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebFeatures.DataContext.Sql
{
    public class SqlAppContext : AppContext
    {
        private readonly IConfiguration _configuration;

        public SqlAppContext(
            DbContextOptions<SqlAppContext> options, 
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Sql");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
