using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Scracth2025.DATABASE;

public static class DbExtensions
{
    public static void AddDb(this IServiceCollection services, DbOptions options)
    {
        var connectionStringUri = new Uri(options.ConnectionString);
        var (user, pwd) = UrlExtractUserAndPwd(connectionStringUri);
        var dbName = connectionStringUri.AbsolutePath[1..]; //remove leading slash
        var connectionString = $"Host={connectionStringUri.Host};Port={connectionStringUri.Port};Username={user};Password={pwd};Database={dbName};Include Error Detail=true";

        services.AddDbContext<Db>(opt =>
        {
            opt.UseNpgsql(connectionString);
            opt.UseLazyLoadingProxies();
        });
    }

    private static (string user, string pwd) UrlExtractUserAndPwd(Uri uri)
    {
        var userInfo = uri.UserInfo.Split(':')
            .Select(s => s.Replace("%40", "@")) //strange c# hack ... (all chars are correctly decoded except %40...)
            .ToArray();
        return (userInfo[0], userInfo[1]);
    }

    public static async Task<bool> MigrateDb(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            using (var db = scope.ServiceProvider.GetRequiredService<Db>())
            {
                await db.Database.MigrateAsync();
                return true;
            }
        }
    }
}

