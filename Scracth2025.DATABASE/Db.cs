using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Scracth2025.DATABASE.Entities;
using System.Reflection;

namespace Scracth2025.DATABASE;

public class Db : DbContext
{
	public Db(DbContextOptions<Db> options) : base(options)
	{
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Apply specific configurations from all IEntityTypeConfiguration<T> instances defined in Database folder
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		foreach (EntityEntry entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
		{
			BaseEntity? entity = entry.Entity as BaseEntity;
			if (entity != null)
			{
				if (entry.State == EntityState.Added)
				{
					entity.CreatedDate = DateTimeOffset.UtcNow;
				}
				else
				{
					// We should never modify the creation date
					if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test")
					{
						Entry(entity).Property(x => x.CreatedDate).IsModified = false;
					}
				}

				entity.LastModificationDate = DateTimeOffset.UtcNow;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}
}
