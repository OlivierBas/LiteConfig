using LiteConfiguration.Types;

namespace LiteConfiguration;

public class LiteConfigField
{
    private string _fieldName;
    private FieldValueType _fieldValueType;
    private string _fieldComment;
    private object _value;

    internal string GetName () => _fieldName;
    internal string GetComment () => _fieldComment;
    internal object GetValue () => _value;

    internal void SetValue (object value)
    {
        string strValue = value.ToString();
        try
        {
            switch (_fieldValueType)
            {
                case FieldValueType.Auto:
                    if (Char.IsLetter(strValue[0]))
                    {
                        _value = strValue;
                    }
                    else
                    {
                        if (strValue.Contains('.')
                         || strValue.Contains(','))
                        {
                            _value = Single.Parse(strValue);
                        }
                        else
                        {
                            _value = Int32.Parse(strValue);
                        }
                    }

                    break;
                case FieldValueType.String:
                    _value = strValue;
                    break;
                case FieldValueType.Number:
                    if (strValue.Contains('.')
                     || strValue.Contains(','))
                    {
                        _value = Single.Parse(strValue);
                    }
                    else
                    {
                        _value = Int32.Parse(strValue);
                    }

                    break;
                case FieldValueType.Guid:
                    _value = Guid.Parse(strValue);
                    break;
            }
        }
        catch (FormatException fex)
        {
            throw new FormatException(
                $"LiteConfig Field {_fieldName} has an invalid Value: ${_value}. Expected Type ${_fieldValueType}");
        }
    }

    internal LiteConfigField (string fieldName, FieldValueType fieldValueType, string fieldComment, object? defaultValue = null)
    {
        _fieldName = fieldName;
        _fieldValueType = fieldValueType;
        _fieldComment   = fieldComment;
        if (defaultValue is not null)
            SetValue(defaultValue);
    }
}