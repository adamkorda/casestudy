using System;

using Microsoft.EntityFrameworkCore;

using Products.Api.Data;

namespace Products.Api.Tests.Helpers.Fixtures
{
    public class InMemoryApplicationDbContextFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; }

        public InMemoryApplicationDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "EmptyCaseStudy").Options;
            ApplicationDbContext = new ApplicationDbContext(options);
            ApplicationDbContext.Database.EnsureDeleted();
            ApplicationDbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            ApplicationDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}