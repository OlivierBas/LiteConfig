namespace LiteConfiguration.Types;

public enum ValueTypes
{
    /// <summary>
    /// Attempts to parse the configuration value to string if value starts with a letter, otherwise Number.
    /// </summary>
    Auto,
    String,
    Number,
    Guid
}