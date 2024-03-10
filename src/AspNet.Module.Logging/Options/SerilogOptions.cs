// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AspNet.Module.Logging.Options;

/// <summary>
///     Настройки логов из конфигурации приложения
/// </summary>
public class SerilogOptions
{
    /// <summary>
    ///     Включить внутреннее логирование Serilog
    /// </summary>
    public bool EnableSelfLog { get; internal init; }

    /// <summary>
    ///     Включить OpenSearch-формат логов
    /// </summary>
    public bool UseOpenSearchFormat { get; internal init; }

    /// <summary>
    ///     По умолчанию
    /// </summary>
    public static SerilogOptions Default => new();

    /// <summary>
    ///     Настройки Serilog
    /// </summary>
    public static string Key => "Serilog";
}