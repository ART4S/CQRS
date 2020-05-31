﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace WebFeatures.DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new CompositionRoot();
            var scripts = root.Services.GetRequiredService<ScriptsRunner>();
            var logger = root.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                scripts.Run();
                logger.LogInformation("Finished succesefully");

            }
            catch (Exception e)
            {
                logger.LogError(e, "Finished with exception");
            }
        }
    }
}
