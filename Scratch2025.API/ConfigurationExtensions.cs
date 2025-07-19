
namespace Scratch2025.API;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Bind a section of the configuration and returns the binded section
    /// </summary>
    /// <typeparam name="Options"></typeparam>
    /// <param name="config"></param>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public static Options BindOptions<Options>(this IConfiguration config, string sectionName) where Options : new()
    {
        var res = new Options();
        config.Bind(sectionName, res);
        return res;
    }

    /// <summary>
    /// Detects the local environment similar to IsDevelopment, IsStaging and IsProduction (see launchSettings.json)
    /// </summary>
    /// <param name="env"></param>
    /// <returns></returns>
    public static bool IsLocal(this IWebHostEnvironment env)
        => env.IsEnvironment("Local");

    /// <summary>
    /// Detects the test environment similar to IsDevelopment, IsStaging and IsProduction (see Tests project, MyWebApplicationFactory)
    /// </summary>
    /// <param name="env"></param>
    /// <returns></returns>
    public static bool IsTest(this IWebHostEnvironment env)
        => env.IsEnvironment("Test");
}