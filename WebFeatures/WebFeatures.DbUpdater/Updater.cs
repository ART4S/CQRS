using Microsoft.Extensions.Logging;
using System;
using AppContext = WebFeatures.DataContext.AppContext;

namespace WebFeatures.DbUpdater
{
    /// <summary>
    /// Обновление БД
    /// </summary>
    internal class Updater
    {
        private readonly ILogger<Updater> _logger;
        private readonly AppContext _context;

        public Updater(
            ILogger<Updater> logger,
            AppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
