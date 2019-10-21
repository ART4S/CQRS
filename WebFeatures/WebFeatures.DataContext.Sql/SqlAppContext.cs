using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebFeatures.DataContext.Sql
{
    public class SqlAppContext : AppContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public SqlAppContext(
            DbContextOptions<SqlAppContext> options,
            IConfiguration configuration, 
            ILoggerFactory loggerFactory) : base(options)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(SqlAppContext)));
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}
