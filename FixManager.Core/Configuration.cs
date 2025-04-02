namespace FixManager.Core;

public class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageSize = 10;
    public const int DefaultPageNumber = 1;
    public static string ConnectionString { get; set; } = string.Empty;
}