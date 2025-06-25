using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EduTrack.Data
{
    public class EduTrackDbContextFactory : IDesignTimeDbContextFactory<EduTrackDbContext>
    {
        public EduTrackDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EduTrackDbContext>();

            // appsettings.json'dan connection string okunabilir
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EduTrack.API"))

                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new EduTrackDbContext(optionsBuilder.Options);
        }
    }

}
