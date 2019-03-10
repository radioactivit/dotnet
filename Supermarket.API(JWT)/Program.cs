﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Supermarket.API.Domain.Security.Hashing;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Security.Hashing;

namespace Supermarket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // If we don't use in memory DB
            // CreateWebHostBuilder(args).Build().Run();

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>())
            {
                var passwordHasher = scope.ServiceProvider.GetService<IPasswordHasher>();
                DatabaseSeed.Seed(context, passwordHasher);
            }


            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        // If we don't use in memory DB
        /* public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();*/
    }
}