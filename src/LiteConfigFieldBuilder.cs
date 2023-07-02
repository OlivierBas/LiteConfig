using System.Text;
using LiteConfiguration.Exceptions;
using LiteConfiguration.Types;

namespace LiteConfiguration;

public class LiteConfigFieldBuilder
{
    private string _fieldName;
    private FieldValueType _fieldType = FieldValueType.Auto;
    private object? _defaultValue = null;
    private string _comment = string.Empty;

    /// <summary>
    /// Name of Configuration Field. Will strip the value of spaces.
    /// </summary>
    /// <param name="name">The given name for the field</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty.</exception>
    public LiteConfigFieldBuilder WithName (string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"Argument {nameof(name)} cannot be empty");
        _fieldName = name.Replace(' ', '_').Trim();
        return this;
    }

    /// <summary>
    /// The Value Type to parse from the Configuration Field's Value.
    /// </summary>
    /// <param name="fieldValueType">Type of Value to be parsed</param>
    public LiteConfigFieldBuilder WithFieldType (FieldValueType fieldValueType)
    {
        _fieldType = fieldValueType;
        return this;
    }

    /// <summary>
    /// [OPTIONAL] Set Default value for Configuration Field
    /// </summary>
    /// <param name="defaultValue">Default value to be used if no other value is found.</param>
    public LiteConfigFieldBuilder WithDefaultValue (object defaultValue)
    {
        _defaultValue = defaultValue;
        return this;
    }

    /// <summary>
    /// [OPTIONAL] Comment for the Configuration Field.
    /// </summary>
    /// <param name="comment">The comment to for the field</param>
    public LiteConfigFieldBuilder WithComment (string comment)
    {
        StringBuilder sb = new StringBuilder();
        using (StringReader reader = new StringReader(comment))
        {
            string line;
            while (( line = reader.ReadLine()) != null)
            {
                sb.AppendLine("#" + line.TrimStart());
            }
        }

        _comment = sb.ToString();
        return this;
    }

    /// <summary>
    /// Builds the LiteConfigField.
    /// </summary>
    /// <returns>a <see cref="LiteConfigField"/></returns>
    /// <exception cref="NotInitializedException">If Required fields were not set, this exception will be thrown.</exception>
    public LiteConfigField Build ()
    {
        if (String.IsNullOrWhiteSpace(_fieldName))
            throw new NotInitializedException("Field name was not set in LiteConfigFieldBuilder. Use WithName()");
        return new LiteConfigField(_fieldName, _fieldType, _comment, _defaultValue);
    }
}