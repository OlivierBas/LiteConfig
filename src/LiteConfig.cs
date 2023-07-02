namespace LiteConfiguration;

public class LiteConfig
{
    private string _fileName;
    private LiteConfigField[] _fields;
    private int _lineSpacing;
    private string _lineSeperator;

    internal LiteConfig (string fileName, int lineSpacing, params LiteConfigField[] fields)
    {
        _fileName      = fileName + ".lc";
        _lineSpacing   = lineSpacing;
        _lineSeperator = "\r\n";
        _fields        = fields;

        using FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        if (fs.Length > 1)
        {
            using StreamReader sr = new StreamReader(fs);

            bool SkipLine (string line)
            {
                if (line.Length < 3) return true;
                if (line.StartsWith('#')) return true;
                return false;
            }

            while (!sr.EndOfStream)
            {
                string currentLine = sr.ReadLine();
                if (!SkipLine(currentLine))
                {
                    var kvp   = currentLine.Split('=', 2);
                    var key   = kvp[0];
                    var value = kvp[1];
                    var field = _fields.SingleOrDefault(f => f.GetName() == key);
                    if (field is not null)
                    {
                        field.SetValue(value);
                    }
                    else
                    {
                        // Something went horribly wrong?
                    }
                }
            }
        }
        else
        {
            using StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < _fields.Length; i++)
            {
                var field = _fields[i];
                sw.WriteLine(field.GetComment());
                sw.WriteLine(field.GetName() + "=" + field.GetValue());
                if (field != _fields.Last())
                {
                    for (int j = 0; j < _lineSpacing; j++)
                    {
                        sw.Write(_lineSeperator);
                    }
                }
                else
                {
                    sw.Write(_lineSeperator);
                }
            }
        }
    }

    private LiteConfigField GetField (string fieldName)
    {
        try
        {
            return _fields.Single(f => f.GetName() == fieldName);

        }
        catch (InvalidOperationException kex)
        {
            throw new InvalidOperationException(
                $"Configuration Field {fieldName} does not exist in LiteConfig file {_fileName}");
        }
    }

    public string GetStringValue (string field)
    {
        return GetField(field).GetValue().ToString();
    }

    public int GetIntValue (string field)
    {
        var value = GetField(field).GetValue();
        return Int32.Parse(value.ToString());
    }

    public float GetFloatValue (string field)
    {
        var value = GetField(field).GetValue();
        return Single.Parse(value.ToString());
    }

    public Guid GetGuidValue (string field)
    {
        var value = GetField(field).GetValue();
        return Guid.Parse(value.ToString());
    }

    public object GetValue (string field)
    {
        return GetField(field).GetValue();
    }
}