using LiteConfiguration.Exceptions;

namespace LiteConfiguration;

public class LiteConfigBuilder
{
    private string _name;
    private int _lineSpacing = 1;
    private List<LiteConfigField> _fields = new List<LiteConfigField>();

    /// <summary>
    /// Name of the Configuration File. Will strip the value of spaces.
    /// </summary>
    /// <param name="name">The given name for the file</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty.</exception>
    public LiteConfigBuilder UseConfig (string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"Argument {nameof(name)} cannot be empty");
        if (name[0] == '#') throw new ArgumentException($"Argument {nameof(name)} cannot start with '#'");
        _name = name;
        return this;
    }

    /// <summary>
    /// Line spacing to be used between configuration field.
    /// </summary>
    /// <param name="spacing">Amount of line spacing per line.</param>
    public LiteConfigBuilder WithSpacing (int spacing)
    {
        _lineSpacing = spacing;
        return this;
    }

    /// <summary>
    /// Add a Configuration Field to the Configuration File.
    /// </summary>
    /// <param name="field">The Field to be added</param>
    public LiteConfigBuilder AddField (LiteConfigField field)
    {
        if (_fields.Exists(f => f.GetName() == field.GetName()))
            throw new ArgumentException(
                $"Configuration Field {field.GetName()} already exists for configuration {_name}");
        _fields.Add(field);
        return this;
    }

    public LiteConfig Build ()
    {
        if (String.IsNullOrWhiteSpace(_name))
            throw new NotInitializedException(
                "LiteConfig requires a set Configuration File. Call UseConfig() on the builder");
        if (_fields.Count == 0)
            throw new NotInitializedException(
                "LiteConfig expects more than one initialized field. Call AddField() on the builder");
        return new LiteConfig(_name, _lineSpacing, _fields.ToArray());
    }
}