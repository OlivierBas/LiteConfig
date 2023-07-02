using LiteConfiguration.Types;

namespace LiteConfiguration;

public class LiteConfigField
{
    private string _fieldName;
    private ValueTypes _valueType;
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
            switch (_valueType)
            {
                case ValueTypes.Auto:
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
                case ValueTypes.String:
                    _value = strValue;
                    break;
                case ValueTypes.Number:
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
                case ValueTypes.Guid:
                    _value = Guid.Parse(strValue);
                    break;
            }
        }
        catch (FormatException fex)
        {
            throw new FormatException(
                $"LiteConfig Field {_fieldName} has an invalid Value: ${_value}. Expected Type ${_valueType}");
        }
    }

    internal LiteConfigField (string fieldName, ValueTypes valueType, string fieldComment, object? defaultValue = null)
    {
        _fieldName = fieldName;
        _valueType = valueType;
        _fieldComment   = fieldComment;
        if (defaultValue is not null)
            SetValue(defaultValue);
    }
}